using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redux;

public class ShockwaveEmitterController : MonoBehaviour
{
    [SerializeField] public GameObject ShockwavePrefab;
    [SerializeField] public float FireRate;
    [HideInInspector] public float LastTimeFired;
    [HideInInspector] public ObjectPooler ObjectPool;
    private ICommand Emit;

    void Start()
    {
        ObjectPool = ObjectPooler.Instance;
        this.Emit = ScriptableObject.CreateInstance<CommandEmitShockwave>();
        LastTimeFired = 0;
    }

    void FixedUpdate()
    {
        this.Emit.Execute(this.gameObject);
    }
}
