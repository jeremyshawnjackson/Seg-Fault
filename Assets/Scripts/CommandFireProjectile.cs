using UnityEngine;
using Redux;

[RequireComponent(typeof(GunController))]
public class CommandFireProjectile : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        GunController gun = gameObject.GetComponent<GunController>();
        GameObject prefab = gun.ProjectilePrefab;
        

        if (gun.LastTimeFired + gun.FireRate <= Time.time)
        {
            gun.LastTimeFired = Time.time;
            gun.PlayShootSound();
            GameObject projectile = gun.ObjectPool.SpawnFromPool(gun.ProjectileTag, gun.transform.position, gun.transform.rotation);
            
            ProjectileController projectileController =  projectile.GetComponent<ProjectileController>();
            projectileController.Speed = gun.ProjectileSpeed;
        }
    }
}