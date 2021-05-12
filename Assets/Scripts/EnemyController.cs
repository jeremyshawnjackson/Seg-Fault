using UnityEngine;

namespace Redux
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] public float Health;
        [SerializeField] public float MoveSpeed;
        [SerializeField] public float TurnSpeed;
        [SerializeField] private EnemyTypes EnemyType;

        private ICommand MoveCommand;
        private enum EnemyTypes
        {
            Spin
        }

        void Start()
        {
            switch(EnemyType)
            {
                case EnemyTypes.Spin:
                    this.MoveCommand = ScriptableObject.CreateInstance<Spin>();
                    break;
            }
        }

        void FixedUpdate()
        {
            this.MoveCommand.Execute(this.gameObject);
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
            Debug.Log("Enemy " + this.gameObject.name + " has died!");
            Destroy(this.gameObject);
        }
    }
}