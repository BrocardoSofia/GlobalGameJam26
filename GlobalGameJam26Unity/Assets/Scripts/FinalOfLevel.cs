using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalOfLevel : MonoBehaviour
{
    public GameObject[] canvasCerrar;
    public GameObject canvasFinal;
    public string[] culpableTxts;
    public string[] inocenteTxts;
    public TextMeshProUGUI textoCanvas;
    [SerializeField] private float duracionTransicion = 0.5f;

    public void IniciarFinal(string estado)
    {
        Color color = textoCanvas.color;
        color.a = 0f;
        textoCanvas.color = color;

        canvasFinal.SetActive(true);
        foreach (var canvas in canvasCerrar)
        {
            canvas.SetActive(false);
        }

        if(estado == "culpable")
        {
            StartCoroutine(AnimarTextos(culpableTxts));
        }
        else
        {
            StartCoroutine(AnimarTextos(inocenteTxts));
        }
    }

    IEnumerator AnimarTextos(string[] textos)
    {
        yield return new WaitForSeconds(1f);

        foreach (string texto in textos)
        {
            textoCanvas.text = texto;

            yield return StartCoroutine(CambiarAlpha(0f, 1f, duracionTransicion));

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(CambiarAlpha(1f, 0f, duracionTransicion));
        }

        SceneManager.LoadScene("99.CREDITOS");
    }

    IEnumerator CambiarAlpha(float alphaInicial, float alphaFinal, float duracion)
    {
        float tiempoTranscurrido = 0f;
        Color color = textoCanvas.color;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float porcentaje = tiempoTranscurrido / duracion;

            color.a = Mathf.Lerp(alphaInicial, alphaFinal, porcentaje);
            textoCanvas.color = color;

            yield return null;
        }

        color.a = alphaFinal;
        textoCanvas.color = color;
    }
}
