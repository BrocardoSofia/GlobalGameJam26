using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaPreguntas : MonoBehaviour
{
    public LectorPreguntas lectorPreguntas;
    public Button pregunta1;
    public Button pregunta2;
    public Button pregunta3;
    public Button pregunta4;

    private int ronda = 0;
    private List<Pregunta> preguntasdeRonda;

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
