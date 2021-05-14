using UnityEngine;
using Redux;

public class LaserController : MonoBehaviour
{
    // TODO: Add hitbox
    [SerializeField] private Transform FirePoint;
    [SerializeField] private LineRenderer Laser;
    [SerializeField] private float MaxDistance;

    void Update()
    {
        FireLaser();
    }

    void FireLaser()
    {
        RaycastHit hit;
        // Stop laser at nearest object, ignores projectiles
        if (Physics.Raycast(FirePoint.position, 
                            FirePoint.forward, 
                            out hit, 
                            MaxDistance, 
                            ~(LayerMask.GetMask("Projectile") | LayerMask.GetMask("PlayerProjectile"))))
        {
            Debug.DrawLine(FirePoint.position, hit.point, Color.red);
            DrawRay(FirePoint.position, FirePoint.position + FirePoint.forward * hit.distance);
            PlayerController player = hit.collider.gameObject.GetComponentInParent<PlayerController>();
            Debug.Log("HIT: " + hit.collider.tag);
            if (player)
            {
                player.TakeDamage();
            }
        }
        else
        {
            DrawRay(FirePoint.position, FirePoint.position + FirePoint.transform.forward * MaxDistance);
        }
    }

    void DrawRay(Vector3 start, Vector3 end)
    {
        Laser.SetPosition(0, start);
        Laser.SetPosition(1, end);
    }
}