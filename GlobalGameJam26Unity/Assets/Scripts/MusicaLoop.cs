using UnityEngine;

public class MusicaLoop : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cancion;
    [SerializeField] private float volumen = 0.5f;

    void Start()
    {
        // Si no asignaste el AudioSource en el Inspector, lo obtiene del mismo objeto
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Configura el AudioSource
        audioSource.clip = cancion;
        audioSource.loop = true;
        audioSource.volume = volumen;
        audioSource.playOnAwake = true;

        // Reproduce la canción
        audioSource.Play();
    }
}