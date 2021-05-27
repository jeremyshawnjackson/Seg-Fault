using UnityEngine;
using Redux;

[RequireComponent(typeof(GunController))]
public class CommandFireProjectile : ScriptableObject, ICommand
{
    public void Execute(GameObject gameObject)
    {
        GunController gun = gameObject.GetComponent<GunController>();
        if (gun == null)
        {
            // gun = gameObject.GetComponent<BossGunController>();
            this.ExecuteBoss(gameObject);
            return;
        }
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

    void ExecuteBoss(GameObject gameObject)
    {
        BossGunController gun = gameObject.GetComponent<BossGunController>();
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