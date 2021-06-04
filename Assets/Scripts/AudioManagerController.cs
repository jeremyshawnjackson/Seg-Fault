using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerController : MonoBehaviour
{
    private AudioSource Audio;
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip != null && !PauseMenu.GameIsPaused)
        {
            Audio.PlayOneShot(clip);
        }
    }
}
