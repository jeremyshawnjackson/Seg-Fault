using UnityEngine;
using System.Collections;
using Redux;

public class LaserController : MonoBehaviour
{
    // TODO: Turn off after firetime
    [SerializeField] private Transform FirePoint;
    [SerializeField] private LineRenderer Line;
    [SerializeField] private float MaxDistance;
    [SerializeField] private Shader ChargeShader;
    [SerializeField] private Shader FireShader;
    // private bool IsFiring;
    // private bool IsCharging;

    void Update()
    {
        FireLaser();
    }

    void FireLaser()
    {
        // IsFiring = true;
        // Line.enabled = true;
        RaycastHit hit;
        // Stop laser at nearest object, ignores projectiles
        if (Physics.Raycast(FirePoint.position, 
                            FirePoint.forward, 
                            out hit, 
                            MaxDistance, 
                            ~(LayerMask.GetMask("Projectile") | LayerMask.GetMask("Shockwave") | LayerMask.GetMask("PlayerProjectile") | LayerMask.GetMask("BossProjectile") | LayerMask.GetMask("AltBossProjectile"))))
        {
            Debug.DrawLine(FirePoint.position, hit.point, Color.red);
            DrawRay(FirePoint.position, FirePoint.position + FirePoint.forward * hit.distance);
            PlayerController player = hit.collider.gameObject.GetComponentInParent<PlayerController>();
            // Debug.Log("HIT: " + hit.collider.tag);
            if (player && player.GetIsVulnerable())
            {
                player.TakeDamage();
            }
        }
        else
        {
            DrawRay(FirePoint.position, FirePoint.position + FirePoint.transform.forward * MaxDistance);
        }
    }
    // None of this works
    /*public IEnumerator FireLaser(float fireTime)
    {
        IsFiring = true;
        Line.enabled = true;

        Line.material.shader = FireShader;
        // Line.material.shader = ChargeShader;

        // Line.material.SetVector("_EmissionColor", Color.red * .5f);
        // Line.material.shader = 
        // Stop laser at nearest object, ignores projectiles
        FireLaser();
        yield return new WaitForSeconds(fireTime);
        IsFiring = false;
        Line.enabled = false;
    }

    public IEnumerator ChargeLaser(float chargeTime)
    {
        Line.enabled = true;
        IsCharging = true;
        Line.material.shader = ChargeShader;
        FireLaser();
        yield return new WaitForSeconds(chargeTime);
        IsCharging = false;
    }

    void FireLaser(float chargeTime, float fireTime)
    {
        this.StartCoroutine(this.ChargeLaser(chargeTime));
        this.StartCoroutine(this.FireLaser(fireTime));
    }*/

    void DrawRay(Vector3 start, Vector3 end)
    {
        Line.SetPosition(0, start);
        Line.SetPosition(1, end);
    }
}