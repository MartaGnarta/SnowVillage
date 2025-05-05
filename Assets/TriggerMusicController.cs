using UnityEngine;
using System.Collections;

public class TrigerMusicController : MonoBehaviour
{
    public AudioSource musicSource;  
    public float reducedVolume = 0.2f; 
    public float normalVolume = 1f;    
    public float fadeSpeed = 2f;       

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeVolume(reducedVolume));  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeVolume(normalVolume));  
        }
    }

   
    private IEnumerator FadeVolume(float targetVolume)
    {
        float startVolume = musicSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = targetVolume; 
    }
}
