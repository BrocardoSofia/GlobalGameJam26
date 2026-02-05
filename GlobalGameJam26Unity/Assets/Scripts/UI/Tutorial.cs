using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PolygraphController polygraphController;
    public GameObject tutorialCanvas;
    public GameObject[] tutoriales;
    public ButtonsCarpeta botonesCarpetas;
    private int indice = 0;

    public void IniciarTutorial()
    {
        polygraphController.SetPolygraphState("stop");
        tutorialCanvas.SetActive(true);
        tutoriales[0].SetActive(true);
    }

    public void PasarSigTutorial()
    {
        if(indice+1 != tutoriales.Length)
        {
            tutoriales[indice].SetActive(false);
            indice++;
            tutoriales[indice].SetActive(true);
        }
        else
        {
            tutoriales[indice].SetActive(false);
            tutorialCanvas.SetActive(false);
            botonesCarpetas.AbrirInfoPoligrafo();
        }
        
    }
}
