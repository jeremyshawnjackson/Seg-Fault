using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    int segments = 360;
    public float Width;
    public float Radius;
    LineRenderer line;
    public Material Mat;

    int pointCount;
    Vector3[] points;

    

    void Start()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = segments + 1;
        pointCount = segments + 1;
        points = new Vector3[pointCount];
        line.material = Mat;
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle(Radius);
    }

    void DrawCircle(float radius)
    {
        line.startWidth = Width;
        line.endWidth = Width;
        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }
        line.SetPositions(points);
    }
}
