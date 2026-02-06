using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SonidoBoton : MonoBehaviour
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
    }

    public void OnHover(Button boton)
    {
        if (sonidoHover != null && boton.interactable)
        {
            audioSource.PlayOneShot(sonidoHover, volumen);
        }
    }

    public void OnHover(TMP_Dropdown dropdown)
    {
        if (sonidoHover != null && dropdown.interactable)
        {
            audioSource.PlayOneShot(sonidoHover, volumen);
        }
    }

    public void OnHover()
    {
        if (sonidoHover != null && this.enabled)
        {
            audioSource.PlayOneShot(sonidoHover, volumen);
        }
    }

    public void OnClick()
    {
        if (sonidoClick != null)
        {
            audioSource.PlayOneShot(sonidoClick, volumen);
        }
    }
}