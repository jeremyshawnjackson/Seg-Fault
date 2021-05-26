using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiTargetController : MonoBehaviour
{
    public Transform Player;
    private List<Transform> Targets;
    public Vector3 Offset;
    public float TargetRadius;

    void Start()
    {
        Targets = new List<Transform>();
    }

    void Update()
    {
        AquireTargets();
    }

    void LateUpdate()
    {
        if (Targets.Count == 0)
        {
            return;
        }
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + Offset;
        transform.position = newPosition;
    }

    Vector3 GetCenterPoint()
    {
        if (Targets.Count == 1)
        {
            return Targets[0].position;
        }

        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        // for (int i = 0; i < targets.Count; i++)
        foreach (var target in Targets)
        {
            if (target != null)
            {
                bounds.Encapsulate(target.position);
            }
        }
        return bounds.center;
    }

    void AquireTargets()
    {
        Targets.Clear();
        Targets.Add(Player);

        Collider[] hitColliders = Physics.OverlapSphere(Player.position, TargetRadius);
        foreach (var hitCollider in hitColliders)
        {
            GameObject hitObject = hitCollider.gameObject;
            if (hitObject.tag == "Enemy" && !Targets.Contains(hitObject.transform))
            {
                Targets.Add(hitObject.transform);
            }
        }
    }
}
