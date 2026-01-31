using UnityEngine;
using UnityEngine.InputSystem;

public class PolygraphController : MonoBehaviour
{
    [SerializeField] private PolygraphDrawer polygraph;

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Debug.Log("Normal");
                polygraph.StartNormal();
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                Debug.Log("Mintiendo");
                polygraph.StartLying();
            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                Debug.Log("Verdad");
                polygraph.StartTruth();
            }
        }
    }

    public void SetPolygraphState(string state)
    {
        switch (state.ToLower())
        {
            case "normal":
                Debug.Log("Normal");
                polygraph.StartNormal();
                break;
            case "lying":
            case "mintio":
                Debug.Log("Mintiendo");
                polygraph.StartLying();
                break;
            case "truth":
            case "verdad":
                Debug.Log("Verdad");
                polygraph.StartTruth();
                break;
        }
    }
}