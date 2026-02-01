using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SonidoBoton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip sonidoHover;
    [SerializeField] private AudioClip sonidoClick;
    [SerializeField] private float volumen = 1f;

    private AudioSource audioSource;
    private Button boton;

    void Start()
    {
        // Obtiene o crea el AudioSource
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Añade el sonido al click del botón
        boton = GetComponent<Button>();
        if (boton != null && sonidoClick != null)
        {
            boton.onClick.AddListener(ReproducirSonidoClick);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Reproduce el sonido una sola vez al hacer hover
        if (sonidoHover != null)
        {
            audioSource.PlayOneShot(sonidoHover, volumen);
        }
    }

    void ReproducirSonidoClick()
    {
        if (sonidoClick != null)
        {
            audioSource.PlayOneShot(sonidoClick, volumen);
        }
    }
}