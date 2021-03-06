using UnityEngine;
using System.Collections;
using Redux;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public static int EnemyCount = 0;
    [SerializeField] public float Health;
    public float MoveSpeed;
    public float TurnSpeed;
    [SerializeField] private EnemyTypes EnemyType;
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip DeathSound;

    [HideInInspector] public float VisionRadius;

    private Rigidbody Rigidbody;
    private ICommand MoveCommand;
    private Transform target;
    private AudioManagerController AudioManager;
    private enum EnemyTypes
    {
        Stationary,
        Spin,
        Chase
    }

    void Start()
    {
        EnemyCount++;
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.mass = 99999999;
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            target = null;
        }
        
        switch(EnemyType)
        {
            case EnemyTypes.Stationary:
                this.MoveCommand = ScriptableObject.CreateInstance<CommandStay>();
                Rigidbody.drag = 99999999;
                break;
            case EnemyTypes.Spin:
                this.MoveCommand = ScriptableObject.CreateInstance<CommandSpin>();
                Rigidbody.drag = 99999999;
                break;
            case EnemyTypes.Chase:
                this.MoveCommand = ScriptableObject.CreateInstance<CommandChasePlayer>();
                break;
        }
    }

    void FixedUpdate()
    {
        this.MoveCommand.Execute(this.gameObject);
    }

    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        EnemyCount--;
        AudioManager.PlayClip(DeathSound);
        Debug.Log("Enemy " + this.gameObject.name + " has died!");
        Destroy(this.gameObject);
    }

    public void TakeDamage()
    {
        // this.Audio.PlayOneShot(HitSound, 0.01f);
        AudioManager.PlayClip(HitSound);
        Health -= 1;
        Debug.Log("Enemy health reduced to " + Health);
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Enemy triggered by: " + other.tag);
        //if (this.tag != other.tag)
        if (other.tag == "PlayerProjectile")
        {
            TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, VisionRadius);
    }
}