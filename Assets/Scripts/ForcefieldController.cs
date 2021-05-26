using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldController : MonoBehaviour
{
    private AudioManagerController AudioManager;
    
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip BreakSound;
    void Start()
    {
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PlayerProjectile")
        {
            AudioManager.PlayClip(HitSound);
        }
    }
}
