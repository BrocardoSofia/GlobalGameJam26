using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonsVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referencias")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Colores del Texto")]
    [SerializeField] private Color colorNormal = Color.white;
    [SerializeField] private Color colorOnHover = Color.yellow;
    [SerializeField] private Color colorDisabled = Color.gray;

    [Header("Configuración")]
    [SerializeField] private float transitionDuration = 0.2f;

    private Color currentColor;
    private bool isHovering = false;

    private void Start()
    {
        // Si no se asignó el botón manualmente, intentar obtenerlo del mismo GameObject
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        // Si no se asignó el texto, buscar en los hijos
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

            // Si no hay TextMeshPro, intentar con Text legacy
            if (buttonText == null)
            {
                Text legacyText = GetComponentInChildren<Text>();
                if (legacyText != null)
                {
                    Debug.LogWarning($"El botón {gameObject.name} usa Text legacy. Se recomienda usar TextMeshPro.");
                }
            }
        }

        // Establecer el color inicial
        if (buttonText != null)
        {
            currentColor = colorNormal;
            buttonText.color = currentColor;
        }

    }

    private void Update()
    {
        // Verificar si el botón está deshabilitado
        if (button != null && !button.interactable)
        {
            if (buttonText != null && buttonText.color != colorDisabled)
            {
                SetColor(colorDisabled);
            }
        }
        else if (!isHovering && buttonText != null && buttonText.color != colorNormal)
        {
            // Volver al color normal si no está en hover y no está deshabilitado
            SetColor(colorNormal);
        }
    }

    // Se llama cuando el cursor entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            isHovering = true;
            SetColor(colorOnHover);
        }
    }

    // Se llama cuando el cursor sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (button != null && button.interactable)
        {
            SetColor(colorNormal);
        }
    }

    // Método para cambiar el color con o sin transición
    private void SetColor(Color targetColor)
    {
        if (buttonText == null) return;

        currentColor = targetColor;

        if (transitionDuration > 0)
        {
            StopAllCoroutines();
            StartCoroutine(TransitionToColor(targetColor));
        }
        else
        {
            buttonText.color = targetColor;
        }
    }

    // Corrutina para transición suave de color
    private System.Collections.IEnumerator TransitionToColor(Color targetColor)
    {
        Color startColor = buttonText.color;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            buttonText.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        buttonText.color = targetColor;
    }

    // Métodos públicos para cambiar colores en tiempo de ejecución
    public void SetNormalColor(Color color)
    {
        colorNormal = color;
        if (!isHovering && button != null && button.interactable)
        {
            SetColor(colorNormal);
        }
    }

    public void SetHoverColor(Color color)
    {
        colorOnHover = color;
    }

    public void SetDisabledColor(Color color)
    {
        colorDisabled = color;
    }

}