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
            }/*
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                polygraph.SetState(PolygraphDrawer.PolygraphState.Truth);
            }
            else if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                polygraph.ResetGraph();
            }*/
        }
    }

    public void SetPolygraphState(string state)
    {
        switch (state.ToLower())
        {
            case "normal":
                polygraph.StartNormal();
                break;
            case "lying":
            case "mintio":
                polygraph.StartLying();
                break;/*
            case "truth":
            case "verdad":
                polygraph.SetState(PolygraphDrawer.PolygraphState.Truth);
                break;*/
        }
    }
}