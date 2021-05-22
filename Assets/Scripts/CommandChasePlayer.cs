using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redux;

public class CommandChasePlayer : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            return;
        }
        Transform target = player.transform;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - gameObject.transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        // Rotate towards target
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, enemy.TurnSpeed * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, target.position) > 7f)
        {
            // Approach target
            rb.velocity = gameObject.transform.forward * enemy.MoveSpeed;
        }
        else
        {
            // Slow to stop
            rb.velocity = Vector3.Slerp(rb.velocity, Vector3.zero, 10f * Time.deltaTime);
        }

    }
}
