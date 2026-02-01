using UnityEngine;

public class ButtonsCarpeta : MonoBehaviour
{
    public PolygraphController polygraphController;
    public LogicaPreguntas logicaPreguntas;
    public GameObject canvasCarpeta;

    public void AbrirCarpeta()
    {
        polygraphController.SetPolygraphState("stop");
        canvasCarpeta.SetActive(true);
        logicaPreguntas.Pausar();
    }

    public void CerrarCarpeta()
    {
        polygraphController.SetPolygraphState("start");
        canvasCarpeta.SetActive(false);
        logicaPreguntas.Reanudar();
    }
}
