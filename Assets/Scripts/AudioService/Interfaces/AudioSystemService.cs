using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystemService : MonoBehaviour
{
    [SerializeField] private AudioSource effectsSource; 
    [SerializeField] private AudioClip attackClip; 
    [SerializeField] private AudioClip deathClip; 

    public void Initialize()
    {
        
        if (effectsSource == null)
        {
            effectsSource = gameObject.AddComponent<AudioSource>();
            effectsSource.playOnAwake = false; 
        }
    }

    
    public void PlayAttackSound()
    {
        if (attackClip != null)
        {
            effectsSource.PlayOneShot(attackClip);
        }
    }

   
    public void PlayDeathSound()
    {
        if (deathClip != null)
        {
            effectsSource.PlayOneShot(deathClip);
        }
    }
}