using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaPreguntas : MonoBehaviour
{
    public PolygraphController polygraphController;
    public LectorPreguntas lectorPreguntas;
    public Button pregunta1;
    public Button pregunta2;
    public Button pregunta3;
    public Button pregunta4;

    public GameObject canvasFinal;
    public GameObject[] historial;

    private int ronda = 1;
    private List<Pregunta> preguntasdeRonda;
    private bool pausado = false;

    public void Iniciar()
    {
        preguntasdeRonda = lectorPreguntas.ObtenerPreguntasPorRonda(1);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        Debug.Log("Cantidad de preguntas: " + preguntasdeRonda.Count);

        pregunta1.GetComponentInChildren<TextMeshProUGUI>().text = preguntasdeRonda[0].pregunta;
        pregunta2.GetComponentInChildren<TextMeshProUGUI>().text = preguntasdeRonda[1].pregunta;
        pregunta3.GetComponentInChildren<TextMeshProUGUI>().text = preguntasdeRonda[2].pregunta;

        if (preguntasdeRonda.Count == 4)
        {
            pregunta4.gameObject.SetActive(true);
            pregunta4.GetComponentInChildren<TextMeshProUGUI>().text = preguntasdeRonda[3].pregunta;
        }
        else
        {
            pregunta4.gameObject.SetActive(false);
        }

        ActiveButtons();
    }

    public void ButtonClicked(int indice)
    {
        Debug.Log(indice);
        DisableButtons();
        TextMeshProUGUI[] todosLosTextos = historial[ronda - 1].GetComponentsInChildren<TextMeshProUGUI>();
        string detector = "Inconcluso";
        TMP_Dropdown dopdownPregunta = historial[ronda - 1].GetComponentInChildren<TMP_Dropdown>();

        foreach (TextMeshProUGUI texto in todosLosTextos)
        {
            if (texto.gameObject.CompareTag("Pregunta"))
            {
                texto.text = preguntasdeRonda[indice].pregunta;
            }
            else if (texto.gameObject.CompareTag("Respuesta"))
            {
                texto.text = preguntasdeRonda[indice].respuesta;
            }
        }

        detector = preguntasdeRonda[indice].detector;
        Debug.Log(detector);
        historial[ronda - 1].SetActive(true);

        StartCoroutine(EjecutarPoligrafo(detector, dopdownPregunta));
    }

    IEnumerator EjecutarPoligrafo(string detector, TMP_Dropdown dopdownPregunta)
    {
        dopdownPregunta.interactable = false;
        polygraphController.SetPolygraphState(detector);

        while (true)
        {
            while (pausado)
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(4f);
            dopdownPregunta.interactable = true;
            polygraphController.SetPolygraphState("Inconcluso");
        }
        
    }

    public void Pausar()
    {
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
    }

    public void NuevasPreguntas()
    {
        if(ronda != 8)
        {
            ronda++;
            preguntasdeRonda = lectorPreguntas.ObtenerPreguntasPorRonda(ronda);
            UpdateButtons();
        }
        else
        {
            polygraphController.SetPolygraphState("stop");
            canvasFinal.SetActive(true);
        }
        
    }

    private void DisableButtons()
    {
        pregunta1.interactable = false;
        pregunta2.interactable = false;
        pregunta3.interactable = false;

        if (preguntasdeRonda.Count == 4)
        {
            pregunta4.interactable = false;
        }
    }

    private void ActiveButtons()
    {
        pregunta1.interactable = true;
        pregunta2.interactable = true;
        pregunta3.interactable = true;

        if (preguntasdeRonda.Count == 4)
        {
            pregunta4.interactable = true;
        }
    }
}
