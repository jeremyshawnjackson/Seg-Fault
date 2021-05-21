using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redux;

[RequireComponent(typeof(CircleRenderer))]
public class ShockwaveController : MonoBehaviour, IPooledObject
{
    [SerializeField] private SphereCollider Hitbox;  
    [SerializeField] private float MaxRadius;
    [SerializeField] private float GrowthSpeed;
    [SerializeField] private CircleRenderer CircleRenderer;
    [SerializeField] private float Width;

    public void OnObjectSpawn()
    {
        // this.gameObject.SetActive(true);
        CircleRenderer.Radius = 0;
        CircleRenderer.Width = Width;
        Hitbox.radius = CircleRenderer.Radius + Width / 2;
    }

    void FixedUpdate()
    {
        if (CircleRenderer.Radius < MaxRadius)
        {
            CircleRenderer.Radius += GrowthSpeed;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        Hitbox.radius = CircleRenderer.Radius + CircleRenderer.Width / 2;
    }

    // void OntriggerEnter(Collider other)
    // {
    //     StartCoroutine(Reset());
    // }

    // public IEnumerator Reset()
    // {
    //     Debug.Log("shockwave reset");
    //     Hitbox.enabled = false;
    //     yield return new WaitForSeconds(.3f);
    //     Hitbox.enabled = true;
    // }
}
