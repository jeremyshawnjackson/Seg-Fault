using UnityEngine;
using Redux;

public class GunController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float FireRate;
    public float ProjectileSpeed;
    [SerializeField] private GunTypes GunType;
    [SerializeField] public AudioClip ShootSound;
    [HideInInspector] public ObjectPooler ObjectPool;
    [HideInInspector] public float LastTimeFired;
    [HideInInspector] public string ProjectileTag;
    private ICommand Fire1;
    private AudioManagerController AudioManager;
    private enum GunTypes
    {
        Player,
        Enemy
    }

    void Start()
    {
        Time.timeScale = 1f;
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
        switch(GunType)
        {
            case GunTypes.Player:
                ProjectileTag = "PlayerProjectile";
                break;
            case GunTypes.Enemy:
                ProjectileTag = "EnemyProjectile";
                break;
            default:
                Debug.LogError("Gun type missing.");
                break;
        }
        ObjectPool = ObjectPooler.Instance;
        this.Fire1 = ScriptableObject.CreateInstance<CommandFireProjectile>();
    }

    void Update()
    {
        if (this.tag == "Player")
        {
            this.ReadWeaponInput();
        }
        else
        {
            this.Fire1.Execute(this.gameObject);
        }
    }

    void ReadWeaponInput()
    {
        if (Input.GetButton("Fire1"))
        {
            this.Fire1.Execute(this.gameObject);
        }
    }

    public void PlayShootSound()
    {
        AudioManager.PlayClip(ShootSound);
    }
}