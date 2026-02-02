using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownLogic : MonoBehaviour
{
    public LogicaPreguntas logicaPreguntas;
    public TMP_Dropdown dropdown;
    private bool primeraVezSeleccionado = true;

    public void Seleccion()
    {
        if (primeraVezSeleccionado)
        {
            dropdown.options.RemoveAt(0);
            primeraVezSeleccionado = false;
            logicaPreguntas.NuevasPreguntas();
            dropdown.value--;
        }
    }
}
