using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redux
{
    [RequireComponent(typeof(GunController))]
    public class FireProjectile : ScriptableObject, ICommand
    {
        public void Execute(GameObject gameObject)
        {
            GunController gun = gameObject.GetComponent<GunController>();
            GameObject projectile = gun.ProjectilePrefab;

            if (gun.LastTimeFired + gun.FireRate <= Time.time)
            {
                gun.LastTimeFired = Time.time;
                gun.ObjectPool.SpawnFromPool(gun.ProjectileTag, gun.transform.position, gun.transform.rotation);
            }
        }
    }
}