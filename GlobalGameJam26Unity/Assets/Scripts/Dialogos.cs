using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogos : MonoBehaviour
{
    public string[] dialogoInicioJuego;
    public TextMeshProUGUI texto;
    private int indice = 0;

    public void SiguienteDialogo()
    {
        if(indice != dialogoInicioJuego.Length)
        {
            texto.text = dialogoInicioJuego[indice];
            indice++;
        }
        else
        {
            SceneManager.LoadScene("2.LEVEL");
        }
    }
}
