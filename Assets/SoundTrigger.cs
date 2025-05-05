using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;       
    public AudioClip soundEffect;         
    public float volume = 1f;             

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            audioSource.PlayOneShot(soundEffect, volume);
        }
    }
}
