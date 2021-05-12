using UnityEngine;
using Redux;

public class MovePlayer : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float speed = gameObject.GetComponent<PlayerController>().MoveSpeed;
        rb.velocity = movement * speed;
    }
}