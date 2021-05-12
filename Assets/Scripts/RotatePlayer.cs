using UnityEngine;
using Redux;

public class RotatePlayer : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        float turnSpeed = gameObject.GetComponent<PlayerController>().TurnSpeed;
        Plane playerPlane = new Plane(Vector3.up, gameObject.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Debug.DrawLine(ray.origin, targetPoint, Color.blue);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - gameObject.transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}