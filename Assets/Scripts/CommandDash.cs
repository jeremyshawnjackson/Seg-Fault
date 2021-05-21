using UnityEngine;
using System.Collections;
using Redux;

public class CommandDash : ScriptableObject, ICommand
{
    private int RemainingCharges;
    private float CooldownTimer;
    public bool Dashing;
    private Vector3 Destination;
    private ParticleSystem Trail;
    private PlayerController Player;
    private float LastTimeDashed;

    public void Execute(GameObject gameObject)
    {
        // Initialize
        if (Player == null)
        {
            Player = gameObject.GetComponent<PlayerController>();
            Trail = gameObject.transform.Find("Trail").GetComponent<ParticleSystem>();
            RemainingCharges = Player.DashCharges;
            CooldownTimer = Player.DashCooldown;
            LastTimeDashed = 0;
            Dashing = false;
            // UIText.text = RemainingCharges.ToString();
        }

        if (Input.GetKey(KeyCode.LeftShift) && RemainingCharges > 0 && (LastTimeDashed + Player.DashCooldown) < Time.time)
        {
            LastTimeDashed = Time.time;
            Player.StartCoroutine(Player.MakeTemporaryInvulnerable(0.25f));
            Player.AudioManager.PlayClip(Player.DashSound);
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