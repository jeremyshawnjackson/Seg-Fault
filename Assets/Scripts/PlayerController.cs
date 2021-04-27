using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redux
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float MoveSpeed;
        [SerializeField] public float TurnSpeed;
        private ICommand MoveCommand;
        private ICommand RotateCommand;

        void Start()
        {
            this.MoveCommand = ScriptableObject.CreateInstance<MovePlayer>();
            this.RotateCommand = ScriptableObject.CreateInstance<RotatePlayer>();
        }

        void FixedUpdate()
        {
            this.MoveCommand.Execute(this.gameObject);
            this.RotateCommand.Execute(this.gameObject);
        }
    }
}