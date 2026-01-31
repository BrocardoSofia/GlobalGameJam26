using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Clase para cada pregunta individual
[System.Serializable]
public class Pregunta
{
    public int ronda;
    public string pregunta;
    public string respuesta;
    public string detector;
}

// Clase contenedora para el array de preguntas
[System.Serializable]
public class PreguntasData
{
    public List<Pregunta> preguntas;
}

public class LectorPreguntas : MonoBehaviour
{
    private PreguntasData preguntasData;
    public TextAsset archivoJSON;

    void Start()
    {
        Debug.Log("Cargando preguntas");
        CargarPreguntas(archivoJSON);
    }

    public void CargarPreguntas(TextAsset jsonFile)
    {
        if (jsonFile != null)
        {
            preguntasData = JsonUtility.FromJson<PreguntasData>(jsonFile.text);
            Debug.Log($"Se cargaron {preguntasData.preguntas.Count} preguntas");
        }
        else
        {
            Debug.LogError("No se asignó ningún archivo JSON");
        }
    }

    public List<Pregunta> ObtenerPreguntasPorRonda(int numeroRonda)
    {
        return preguntasData.preguntas.FindAll(p => p.ronda == numeroRonda);
    }

    public List<Pregunta> DesordenarPreguntas(List<Pregunta> listaPreguntasOriginal)
    {
        List<Pregunta> listaDesordenada = new List<Pregunta>(listaPreguntasOriginal);

        for (int i = listaDesordenada.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            Pregunta temp = listaDesordenada[i];
            listaDesordenada[i] = listaDesordenada[j];
            listaDesordenada[j] = temp;
        }

        return listaDesordenada;
    }
}