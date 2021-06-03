using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 targetOffset;
    public float movementSpeed;

    void Start()
    {
        targetOffset = this.transform.position - target.position;
    }

    private void FixedUpdate()
    {
        moveCamera();
    }

    void moveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime); 
    }
}
