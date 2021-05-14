using UnityEngine;

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

        void Start()
        {
            this.MoveCommand = ScriptableObject.CreateInstance<MovePlayer>();
            this.RotateCommand = ScriptableObject.CreateInstance<RotatePlayer>();
            this.DashCommand = ScriptableObject.CreateInstance<DashMove>();

            DamageCooldown = 0.5f;
            TimeSinceDamaged = DamageCooldown;
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
            // if (this.tag != other.tag)
            // {
            //     Debug.Log("Enemy triggered by: " + other.tag);
            //     TakeDamage();
            // }
            switch(other.tag)
            {
                case "Enemy":
                    TakeDamage();
                    break;
            }
        }
    }
}