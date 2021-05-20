using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Redux
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Basic Settings")]
        [SerializeField] private int Health;
        [SerializeField] public float MoveSpeed;
        [SerializeField] public float TurnSpeed;
        
        [Header("Dash Settings")]
        [SerializeField] public int DashCharges;
        [SerializeField] public float DashCooldown;
        [SerializeField] public float DashDistance;
        [SerializeField] public float DashSpeed;


        private ICommand MoveCommand;
        private ICommand RotateCommand;
        private ICommand DashCommand;

        private float DamageCooldown;
        private float TimeSinceDamaged;
        private string LastTrigger;
        private List<Collider> TriggerList;

        void Start()
        {
            this.MoveCommand = ScriptableObject.CreateInstance<CommandMovePlayer>();
            this.RotateCommand = ScriptableObject.CreateInstance<CommandRotatePlayer>();
            this.DashCommand = ScriptableObject.CreateInstance<CommandDash>();

            DamageCooldown = 0.5f;
            TimeSinceDamaged = DamageCooldown;

            TriggerList = new List<Collider>();
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
            this.gameObject.SetActive(false);
        }

        public void TakeDamage()
        {
            if (TimeSinceDamaged >= DamageCooldown)
            {
                Health -= 1;
                Debug.Log("Player health reduced to " + Health);
                TimeSinceDamaged = 0;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("player triggered on " + other.tag);
            switch(other.tag)
            {
                case "Enemy":
                case "Shockwave":
                    TakeDamage();
                    break;
                // fix dashing overshockwaves
            }
            LastTrigger = other.tag;
        }

        void OnTriggerExit(Collider other)
        {

        }

        public void MakeInvulnerable()
        {
            TimeSinceDamaged = 0;
            Debug.Log("Player invulnerable!");
            // List<Collider> colliders = new List<Collider>(this.GetComponentsInChildren<BoxCollider>());
            // foreach (var collider in colliders)
            // {
            //     collider.enabled = false;
            // }
        }

        void MakeVulnerable()
        {
            Debug.Log("Player vulnerable!");
            // List<Collider> colliders = new List<Collider>(this.GetComponentsInChildren<BoxCollider>());
            // foreach (var collider in colliders)
            // {
            //     collider.enabled = true;
            // }
        }

        public IEnumerator MakeTemporaryInvulnerable(float invulnTime)
        {
            // MakeInvulnerable();
            yield return new WaitForSeconds(invulnTime);
            // MakeVulnerable();
        }
    }
}