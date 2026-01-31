using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SistemaCamarasMultiples : MonoBehaviour
{
    [System.Serializable]
    public class CamaraUI
    {
        public Camera camara;
        public RawImage displayUI;
        public Vector2Int resolucion = new Vector2Int(512, 512);
        [HideInInspector] public RenderTexture renderTexture;
    }

    public List<CamaraUI> camaras = new List<CamaraUI>();

    void Start()
    {
        ConfigurarTodasLasCamaras();
    }

    void ConfigurarTodasLasCamaras()
    {
        foreach (CamaraUI cam in camaras)
        {
            if (cam.camara != null && cam.displayUI != null)
            {
                // Crear RenderTexture para cada cámara
                cam.renderTexture = new RenderTexture(
                    cam.resolucion.x,
                    cam.resolucion.y,
                    16
                );
                cam.renderTexture.Create();

                // Configurar cámara
                cam.camara.targetTexture = cam.renderTexture;

                // Mostrar en UI
                cam.displayUI.texture = cam.renderTexture;

                Debug.Log($"Cámara {cam.camara.name} configurada en UI");
            }
        }
    }

    void OnDestroy()
    {
        // Limpiar todos los RenderTextures
        foreach (CamaraUI cam in camaras)
        {
            if (cam.renderTexture != null)
            {
                cam.renderTexture.Release();
            }
        }
    }

    // Función para cambiar la resolución en runtime
    public void CambiarResolucion(int indiceCamara, int ancho, int alto)
    {
        if (indiceCamara < 0 || indiceCamara >= camaras.Count) return;

        CamaraUI cam = camaras[indiceCamara];

        // Liberar el anterior
        if (cam.renderTexture != null)
        {
            cam.renderTexture.Release();
        }

        // Crear uno nuevo
        cam.renderTexture = new RenderTexture(ancho, alto, 16);
        cam.renderTexture.Create();

        cam.camara.targetTexture = cam.renderTexture;
        cam.displayUI.texture = cam.renderTexture;
    }

    // Activar/Desactivar cámara
    public void ToggleCamara(int indiceCamara, bool activa)
    {
        if (indiceCamara < 0 || indiceCamara >= camaras.Count) return;

        camaras[indiceCamara].camara.enabled = activa;
        camaras[indiceCamara].displayUI.gameObject.SetActive(activa);
    }
}