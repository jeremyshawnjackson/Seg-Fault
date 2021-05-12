using UnityEngine;

namespace Redux
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Basic Settings")]
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

        void Start()
        {
            this.MoveCommand = ScriptableObject.CreateInstance<MovePlayer>();
            this.RotateCommand = ScriptableObject.CreateInstance<RotatePlayer>();
            this.DashCommand = ScriptableObject.CreateInstance<DashMove>();
        }

        void FixedUpdate()
        {
            this.MoveCommand.Execute(this.gameObject);
            this.RotateCommand.Execute(this.gameObject);
            this.DashCommand.Execute(this.gameObject);
        }
    }
}