using UnityEngine;

namespace Redux
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] public GameObject ProjectilePrefab;
        [SerializeField] public float FireRate;
        [SerializeField] private GunTypes GunType;
        [HideInInspector] public ObjectPooler ObjectPool;
        [HideInInspector] public float LastTimeFired;
        [HideInInspector] public string ProjectileTag;
        private ICommand Fire1;
        private enum GunTypes
        {
            Player,
            Enemy
        }

        void Start()
        {
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
    }
}