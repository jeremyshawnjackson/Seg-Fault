using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleRenderer))]
public class ShockwaveController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SphereCollider OuterRim;
    [SerializeField] private SphereCollider InnerRim;    
    [SerializeField] private float MaxRadius;
    [SerializeField] private float GrowthSpeed;
    [SerializeField] private CircleRenderer CircleRender;
    [SerializeField] private float Width;

    void Start()
    {
        CircleRender.Radius = 0;
        CircleRender.Width = Width;
        OuterRim.radius = CircleRender.Radius + Width / 2;
        InnerRim.radius = CircleRender.Radius - Width / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CircleRender.Radius < MaxRadius)
        {
            CircleRender.Radius += GrowthSpeed;
        }
        else
        {
            CircleRender.Radius = 0;
        }
        OuterRim.radius = CircleRender.Radius + CircleRender.Width / 2;
        InnerRim.radius = CircleRender.Radius - CircleRender.Width / 2;
    }
}
