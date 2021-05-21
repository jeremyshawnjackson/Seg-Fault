using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Redux
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Basic Settings")]
        [SerializeField] private int Health;
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


        private ICommand MoveCommand;
        private ICommand RotateCommand;
        private ICommand DashCommand;

        private float DamageCooldown;   // bandaid solution to multiple colliders triggering on the same event
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

            AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
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
        }
        public void Die()
        {
            // TODO: GAME OVER, effects
            Debug.Log("Player has died!");
            AudioManager.PlayClip(DeathSound);
            this.gameObject.SetActive(false);
        }

        public void TakeDamage()
        {
            if (TimeSinceDamaged >= DamageCooldown)
            {
                AudioManager.PlayClip(HitSound);
                Health -= 1;
                Debug.Log("Player health reduced to " + Health);
                TimeSinceDamaged = 0;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("player triggered on " + other.tag);
            if (this.IsVulnerable)
            {
                switch(other.tag)
                {
                    case "Enemy":
                    case "Shockwave":
                        TakeDamage();
                        break;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {

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