using UnityEngine;

namespace Redux
{
    public class ProjectileController : MonoBehaviour, IPooledObject
    {
        [SerializeField] private float Speed;
        [SerializeField] private float MaxDistance;
        [SerializeField] private float Damage;
        private Vector3 FiringPoint;
        private bool Active = false;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
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
                rb.velocity = transform.forward * Speed * 50 * Time.deltaTime;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (this.tag == other.tag || (this.tag == "PlayerProjectile" && other.tag == "Player") || other.tag == "Shockwave")
            {
                return;
            }
            Debug.Log("trigger: " + other.tag);
            this.gameObject.SetActive(false);
        }

        public void OnCollisionEnter(Collision collision)
        {
            // Debug.Log(this.tag + " collided with " + collision.collider.tag);
            if (this.tag == collision.collider.tag || this.tag == "PlayerProjectile" && collision.collider.tag == "Player")
            {
                return;
            }
            this.gameObject.SetActive(false);
        }
    }
}