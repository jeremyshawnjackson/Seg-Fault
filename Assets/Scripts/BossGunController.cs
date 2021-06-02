using UnityEngine;
using Redux;
using System.Collections;

public class BossGunController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject AltProjectilePrefab;
    public float FireRate;
    public float ProjectileSpeed;
    public AudioClip ShootSound;
    [HideInInspector] public ObjectPooler ObjectPool;
    [HideInInspector] public float LastTimeFired;
    [HideInInspector] public string ProjectileTag;
    [HideInInspector] public string AltProjectileTag;
    private ICommand Fire1;
    public float InitialDelay;
    public float ToggleDelay;
    private AudioManagerController AudioManager;

    void Start()
    {
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
        
        ProjectileTag = "BossProjectile";
        AltProjectileTag = "AltBossProjectile";
        ObjectPool = ObjectPooler.Instance;
        this.Fire1 = ScriptableObject.CreateInstance<CommandFireProjectile>();
        
        this.StartCoroutine(AlternateFire());
    }

    void Update()
    {
        this.Fire1.Execute(this.gameObject);
    }

    void SwitchProjectiles()
    {
        string tempStr = ProjectileTag;
        ProjectileTag = AltProjectileTag;
        AltProjectileTag = tempStr;

        GameObject tempObj = ProjectilePrefab;
        ProjectilePrefab = AltProjectilePrefab;
        AltProjectilePrefab = tempObj;
    }

    IEnumerator AlternateFire()
    {
        yield return new WaitForSeconds(InitialDelay);
        while(true)
        {
            yield return new WaitForSeconds(ToggleDelay);
            SwitchProjectiles();
        }
    }

    public void PlayShootSound()
    {
        AudioManager.PlayClip(ShootSound);
    }
}