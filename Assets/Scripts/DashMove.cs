using UnityEngine;
using Redux;

// TODO: IFrames, fix dashing over walls
public class DashMove : ScriptableObject, ICommand
{
    public int RemainingCharges;
    private float CooldownTimer;
    public bool Dashing;
    private Vector3 Destination;
    private ParticleSystem Trail;
    private PlayerController Player;

    public void Execute(GameObject gameObject)
    {
        // Initialize
        if (Player == null)
        {
            Player = gameObject.GetComponent<PlayerController>();
            Trail = gameObject.transform.Find("Trail").GetComponent<ParticleSystem>();
            RemainingCharges = Player.DashCharges;
            CooldownTimer = Player.DashCooldown;
            Dashing = false;
            // UIText.text = RemainingCharges.ToString();
        }

        if (Input.GetKey(KeyCode.LeftShift) && RemainingCharges > 0)
        {
            RemainingCharges -= 1;
            // UIText.text = RemainingCharges.ToString();
            Trail.Play();
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            if (movement == Vector3.zero)
            {
                movement = gameObject.transform.forward;
            }
            Destination = gameObject.transform.position + Player.DashDistance * movement.normalized;

            // Buggy and maybe unecessary. The intent is to avoid dashing into terrain
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, Player.DashDistance, LayerMask.GetMask("Terrain")))
            {
                Debug.Log("hit");
                Vector3 closestPoint = hit.collider.ClosestPointOnBounds(Destination);
                if (Vector3.Distance(Destination, hit.point) < Vector3.Distance(Destination, closestPoint))
                {
                    Debug.Log("blink shortened by terrain");
                    Destination = hit.point - gameObject.transform.forward * 6f;
                }
                else
                {
                    Debug.Log(Vector3.Distance(gameObject.transform.position, closestPoint) + " >= " + Player.DashDistance + "?");
                    if (Vector3.Distance(gameObject.transform.position, closestPoint) >= Player.DashDistance)
                    {
                        Debug.Log("blink extended by terrain");
                        Destination = closestPoint + gameObject.transform.forward * 6f;
                    }
                }
            }

            Dashing = true;
        }

        if (RemainingCharges < Player.DashCharges)
        {
            if (CooldownTimer > 0)
            {
                CooldownTimer -= Time.deltaTime;
            }
            else
            {
                RemainingCharges += 1;
                CooldownTimer = Player.DashCooldown;
                // UIText.text = RemainingCharges.ToString();
            }
        }
        
        if (Dashing)
        {
            float distance = Vector3.Distance(gameObject.transform.position, Destination);
            if (distance > .5f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, Player.DashSpeed * Time.deltaTime);
            }
            else
            {
                Dashing = false;
            }
        }
    }
}