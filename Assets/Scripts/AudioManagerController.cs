using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerController : MonoBehaviour
{
    private AudioSource Audio;
    private float Volume;
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        Volume = 0.01f;
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            Audio.PlayOneShot(clip, Volume);
        }
    }
}
