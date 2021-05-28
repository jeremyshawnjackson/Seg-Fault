using UnityEngine;
using System.Collections;

namespace Redux
{
    public class ProjectileController : MonoBehaviour, IPooledObject
    {
        [HideInInspector] public float Speed;
        [SerializeField] private float MaxDistance;
        [SerializeField] private float Damage;
        private Vector3 FiringPoint;
        private bool Active = false;
        private Rigidbody RigidBody;
        private SphereCollider Collider;

        void Start()
        {
            RigidBody = this.GetComponent<Rigidbody>();
            Collider = this.GetComponent<SphereCollider>();
        }

        public void OnObjectSpawn()
        {
            Active = true;
            FiringPoint = transform.position;
        }

        private void FixedUpdate()
        {
            if (Active)
            {
                MoveProjectile();
            }
        }

        private void MoveProjectile()
        {
            if (Vector3.Distance(FiringPoint, transform.position) > MaxDistance)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                RigidBody.velocity = transform.forward * Speed * 50 * Time.deltaTime;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (this.tag == other.tag || 
                this.tag == "PlayerProjectile" && other.tag == "Player" || 
                other.tag == "Shockwave" || 
                this.tag == "EnemyProjectile" && other.tag == "Forcefeild" ||
                this.tag == "AltBossProjectile" && other.tag == "PlayerProjectile")
            {
                return;
            }
            if (other.tag == "Player")
            {
                PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();
                if (player.GetIsVulnerable() == false)
                {
                    return;
                }
            }
            // Debug.Log("trigger: " + other.tag);
            this.gameObject.SetActive(false);
        }

        public void OnCollisionEnter(Collision collision)
        {
            // Debug.Log(this.tag + " collided with " + collision.collider.tag);
            if (this.tag == collision.collider.tag || 
                this.tag == "PlayerProjectile" && collision.collider.tag == "Player" || 
                this.tag == "EnemyProjectile" && collision.collider.tag == "Forcefield" ||
                this.tag == "AltBossProjectile" && collision.collider.tag == "PlayerProjectile")
            {
                return;
            }
            if (collision.collider.tag == "Player")
            {
                PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
                if (player.GetIsVulnerable() == false)
                {
                    // Debug.Log("Projectile should live here");
                    Physics.IgnoreCollision(this.Collider, collision.collider);
                    return;
                }
            }
            this.gameObject.SetActive(false);
        }
    }
}