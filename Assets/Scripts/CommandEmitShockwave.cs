using UnityEngine;
using Redux;

[RequireComponent(typeof(ShockwaveEmitterController))]
public class CommandEmitShockwave : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        ShockwaveEmitterController shockwave = gameObject.GetComponent<ShockwaveEmitterController>();
        GameObject projectile = shockwave.ShockwavePrefab;

        if (shockwave.LastTimeFired + shockwave.FireRate <= Time.time)
        {
            shockwave.LastTimeFired = Time.time;
            shockwave.ObjectPool.SpawnFromPool("EnemyShockwave", shockwave.transform.position, shockwave.transform.rotation);
        }
    }
}