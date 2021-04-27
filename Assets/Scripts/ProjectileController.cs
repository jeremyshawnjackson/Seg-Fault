using System;
using System.Collections;
using System.Collections.Generic;
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
            // Debug.Log(tag + " projectile triggerred on " + other.tag);
            if (this.tag == "Player" && other.tag == "Enemy")
            {
                GameObject trigger = other.gameObject;
                EnemyController enemy = trigger.GetComponent<EnemyController>();
                ProjectileController enemyProjectile = trigger.GetComponent<ProjectileController>();
                if (enemy)
                {
                    enemy.Health -= Damage;
                }
                else if (enemyProjectile)
                {
                    // Return projectiles to their object pool
                    enemyProjectile.gameObject.SetActive(false);
                }
                this.gameObject.SetActive(false);
            }
            else if (this.tag == "Enemy" && other.tag == "Player")
            {
                Debug.Log(tag + " projectile triggerred on " + other.tag);
                // TODO: Damage player
                this.gameObject.SetActive(false);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log(tag + "projectile collided with " + collision.gameObject.tag);
            //Debug.Log(collision.gameObject.tag);
            // if (collision.gameObject.tag == "Terrain")
            // {
                // Debug.Log(tag + " collided with terrain.");
                // this.gameObject.SetActive(false);
            // }
        }
    }
}