using UnityEngine;
using System.Collections;
using Redux;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public float Health;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float TurnSpeed;
    [SerializeField] private EnemyTypes EnemyType;
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip DeathSound;

    private ICommand MoveCommand;
    private Transform target;
    private AudioManagerController AudioManager;
    private enum EnemyTypes
    {
        Stationary,
        Spin
    }

    void Start()
    {
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
        target = GameObject.Find("Player").transform;
        switch(EnemyType)
        {
            case EnemyTypes.Stationary:
                this.MoveCommand = ScriptableObject.CreateInstance<CommandStay>();
                break;
            case EnemyTypes.Spin:
                this.MoveCommand = ScriptableObject.CreateInstance<CommandSpin>();
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
        if (this.tag != other.tag)
        {
            // Debug.Log("Enemy triggered by: " + other.tag);
            TakeDamage();
        }
    }
}