using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    int Segments = 360;
    public float Width;
    public float Radius;
    LineRenderer Line;
    public Material Mat;

    int PointCount;
    Vector3[] Points;

    

    void Start()
    {
        Line = this.gameObject.AddComponent<LineRenderer>();
        Line.useWorldSpace = false;
        Line.positionCount = Segments + 1;
        PointCount = Segments + 1;
        Points = new Vector3[PointCount];
        Line.material = Mat;
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle(Radius);
    }

    void DrawCircle(float radius)
    {
        Line.startWidth = Width;
        Line.endWidth = Width;
        for (int i = 0; i < PointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / Segments);
            Points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }
        Line.SetPositions(Points);
    }
}
