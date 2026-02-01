using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonsCarpeta : MonoBehaviour
{
    public PolygraphController polygraphController;
    public LogicaPreguntas logicaPreguntas;
    public GameObject canvasCarpeta;
    public GameObject botonIzquierdo;
    public GameObject botonDerecho;
    public UnityEngine.UI.Image fotoPlace;
    public Sprite[] imagenes;
    private int indice = 0;

    public void AbrirCarpeta()
    {
        polygraphController.SetPolygraphState("stop");
        canvasCarpeta.SetActive(true);
        logicaPreguntas.Pausar();
        fotoPlace.sprite = imagenes[0];
        botonIzquierdo.SetActive(false);
        botonDerecho.SetActive(true);
    }

    public void CerrarCarpeta()
    {
        polygraphController.SetPolygraphState("start");
        canvasCarpeta.SetActive(false);
        logicaPreguntas.Reanudar();
    }

    public void PasarImagen(string hacia)
    {
        if(hacia == "Siguiente")
        {
            fotoPlace.sprite = imagenes[indice + 1];
            indice++;
            if(indice == imagenes.Length)
            {
                botonDerecho.SetActive(false);
            }
            botonIzquierdo.SetActive(true);
        }
        else
        {
            fotoPlace.sprite = imagenes[indice -1];
            indice--;
            if(indice == 0)
            {
                botonIzquierdo.SetActive(false);
            }
            botonDerecho.SetActive(true);
        }
    }
}
