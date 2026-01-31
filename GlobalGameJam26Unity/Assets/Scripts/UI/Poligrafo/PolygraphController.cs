using UnityEngine;
using UnityEngine.InputSystem;

public class PolygraphController : MonoBehaviour
{
    [SerializeField] private PolygraphDrawer polygraph;
    /*
    void Update()
    {
        // Usar el nuevo Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                polygraph.SetState(PolygraphDrawer.PolygraphState.Normal);
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                polygraph.SetState(PolygraphDrawer.PolygraphState.Lying);
            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                polygraph.SetState(PolygraphDrawer.PolygraphState.Truth);
            }
            else if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                polygraph.ResetGraph();
            }
        }
    }

    // Método público que puedes llamar desde otros scripts
    public void SetPolygraphState(string state)
    {
        switch (state.ToLower())
        {
            case "normal":
                polygraph.SetState(PolygraphDrawer.PolygraphState.Normal);
                break;
            case "lying":
            case "mintio":
                polygraph.SetState(PolygraphDrawer.PolygraphState.Lying);
                break;
            case "truth":
            case "verdad":
                polygraph.SetState(PolygraphDrawer.PolygraphState.Truth);
                break;
        }
    }*/
}