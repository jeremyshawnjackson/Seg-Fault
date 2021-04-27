using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redux
{
    public class Spin : ScriptableObject, ICommand
    {
        public void Execute(GameObject gameObject)
        {
            EnemyController enemy = gameObject.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.transform.Rotate(0, enemy.TurnSpeed * Time.deltaTime, 0);
            }
            else{Debug.LogError("not enemy");}
        }
    }
}