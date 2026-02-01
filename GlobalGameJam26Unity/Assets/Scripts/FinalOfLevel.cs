using UnityEngine;

public class FinalOfLevel : MonoBehaviour
{
    public GameObject[] canvasCerrar;
    public GameObject canvasFinal;
    public string culpable;
    public string inocente;

    public void IniciarFinal(bool culpable)
    {
        canvasFinal.SetActive(true);
        foreach (var canvas in canvasCerrar)
        {
            canvas.SetActive(false);
        }

        if(culpable)
        {

        }
        else
        {

        }
    }
}
