using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLogic : MonoBehaviour
{
    public void goHome()
    {
        SceneManager.LoadScene("0.MENU");
    }

    public void startGame()
    {
        SceneManager.LoadScene("1.LEVEL");
    }

    public void go2Credits()
    {
        SceneManager.LoadScene("99.CREDITOS");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
