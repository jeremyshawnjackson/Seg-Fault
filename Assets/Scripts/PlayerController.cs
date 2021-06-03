using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Redux
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Basic Settings")]
        [HideInInspector] public int ExtraLives = 3;
        public int MaxHealth;
        private int Health;
        public float MoveSpeed;
        public float TurnSpeed;
        public AudioClip HitSound;
        public AudioClip DashSound;
        public AudioClip DeathSound;
        
        
        [Header("Dash Settings")]
        public int DashCharges;
        public float DashCooldown;
        public float DashDistance;
        public float DashSpeed;


        [HideInInspector] public AudioManagerController AudioManager;
        [HideInInspector] public Collider Playfield;


        private ICommand MoveCommand;
        private ICommand RotateCommand;
        private ICommand DashCommand;

        private float DamageCooldown;
        private float TimeSinceDamaged;
        private bool IsVulnerable;

        void Start()
        {
            this.MoveCommand = ScriptableObject.CreateInstance<CommandMovePlayer>();
            this.RotateCommand = ScriptableObject.CreateInstance<CommandRotatePlayer>();
            this.DashCommand = ScriptableObject.CreateInstance<CommandDash>();

            DamageCooldown = 0.3f;
            IsVulnerable = true;
            TimeSinceDamaged = DamageCooldown;

            Health = MaxHealth;

            AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
            Playfield = GameObject.Find("Playfield").GetComponent<Collider>();
        }

        void FixedUpdate()
        {
            this.MoveCommand.Execute(this.gameObject);
            this.RotateCommand.Execute(this.gameObject);
            this.DashCommand.Execute(this.gameObject);
            
            TimeSinceDamaged += Time.deltaTime;
        }
        void Update()
        {
            if (Health <= 0)
            {
                Die();
            }
            if (!Playfield.bounds.Contains(this.transform.position))
            {
                this.TakeDamage();
            }
        }
        public void Die()
        {
            // TODO: GAME OVER, effects
            Debug.Log("Player has died!");
            AudioManager.PlayClip(DeathSound);
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().EndGame();
        }

        public void TakeDamage()
        {
            if (TimeSinceDamaged >= DamageCooldown)
            {
                if (Health == (int)(MaxHealth * (2f/3f) + 1))
                {
                    GameObject.Find("ship_wing_left").gameObject.SetActive(false);
                }
                if (Health == (int)(MaxHealth * (1f/3f) + 1))
                {
                    GameObject.Find("ship_wing_right").gameObject.SetActive(false);
                }
                AudioManager.PlayClip(HitSound);
                Health -= 1;
                Debug.Log("Player health reduced to " + Health);
                TimeSinceDamaged = 0;
            }
        }

        public int GetHealth()
        {
            return Health;
        }

        void OnTriggerEnter(Collider other)
        {
            // Debug.Log("player triggered on " + other.tag);
            if (this.IsVulnerable)
            {
                switch(other.tag)
                {
                    case "Enemy":
                    case "BossProjectile":
                    case "AltBossProjectile":
                    case "EnemyProjectile":
                    case "Shockwave":
                        TakeDamage();
                        break;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            // Debug.Log("player triggered on " + other.tag);
            if (this.IsVulnerable)
            {
                switch(collision.collider.tag)
                {
                    case "Enemy":
                    case "BossProjectile":
                    case "AltBossProjectile":
                    case "EnemyProjectile":
                    case "Shockwave":
                        TakeDamage();
                        break;
                }
            }
        }

        public void FinishedLevel()
        {
            this.StartCoroutine(MakeTemporaryInvulnerable(3f));
        }

        public void MakeInvulnerable()
        {
            this.IsVulnerable = false;
            Debug.Log("Player invulnerable!");
        }

        void MakeVulnerable()
        {
            this.IsVulnerable = true;
            Debug.Log("Player vulnerable!");
        }

        public IEnumerator MakeTemporaryInvulnerable(float invulnTime)
        {
            MakeInvulnerable();
            yield return new WaitForSeconds(invulnTime);
            MakeVulnerable();
        }

        public bool GetIsVulnerable()
        {
            return IsVulnerable;
        }
    }
}