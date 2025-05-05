using UnityEngine;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class SurfaceAudio
    {
        public string surfaceTag;
        public AudioClip[] audioClips;
    }

    public SurfaceAudio[] surfaceAudios;
    public AudioSource audioSource;
    public LayerMask surfaceLayer;
    public float raycastDistance = 1.5f;

    public float stepInterval = 0.5f; 
    public float movementThreshold = 0.1f; 

    private string currentSurfaceTag = "";
    private float stepTimer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        stepTimer += Time.deltaTime;

        if (IsMoving() && stepTimer >= stepInterval)
        {
            DetectSurfaceAndPlayAudio();
            stepTimer = 0f;
        }
    }

    private bool IsMoving()
    {
        if (rb == null) return false;
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        return horizontalVelocity.magnitude > movementThreshold;
    }

    private void DetectSurfaceAndPlayAudio()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, raycastDistance, surfaceLayer))
        {
            string surfaceTag = hit.collider.tag;
            currentSurfaceTag = surfaceTag;
            PlaySurfaceAudio(surfaceTag);
        }
    }

    private void PlaySurfaceAudio(string surfaceTag)
    {
        foreach (var surfaceAudio in surfaceAudios)
        {
            if (surfaceAudio.surfaceTag == surfaceTag && surfaceAudio.audioClips.Length > 0)
            {
                int randomIndex = Random.Range(0, surfaceAudio.audioClips.Length);
                audioSource.pitch = Random.Range(0.95f, 1.05f); 
                audioSource.PlayOneShot(surfaceAudio.audioClips[randomIndex]);
                return;
            }
        }

        audioSource.Stop();
    }
}
