using UnityEngine;
using System.Collections.Generic;

public class PolygraphDrawer : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float drawSpeed = 30f; // Puntos por segundo
    [SerializeField] private int maxPoints = 100; // Máximo de puntos antes de desaparecer

    [Header("Posición Inicial")]
    [SerializeField] private float startX = 5f; // Donde empieza la línea (derecha) - FIJO
    [SerializeField] private float yPosition = 0f; // Altura de la línea base

    [Header("Picos Aleatorios")]
    [SerializeField] private float minPeakUp = 0.3f; // Mínimo pico hacia arriba
    [SerializeField] private float maxPeakUp = 1f; // Máximo pico hacia arriba
    [SerializeField] private float minPeakDown = 0.3f; // Mínimo pico hacia abajo
    [SerializeField] private float maxPeakDown = 1f; // Máximo pico hacia abajo
    [SerializeField] private float peakSpacing = 0.2f; // Distancia entre picos (controla velocidad)

    private List<Vector3> points = new List<Vector3>();
    private float timeSinceLastPoint = 0f;
    private bool nextPeakUp = true; // Alterna entre arriba y abajo

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        // Generar nuevos puntos
        timeSinceLastPoint += Time.deltaTime;

        if (timeSinceLastPoint >= 1f / drawSpeed)
        {
            // Calcular altura aleatoria del pico
            float peakHeight;
            if (nextPeakUp)
            {
                // Pico hacia arriba: altura aleatoria entre min y max
                peakHeight = Random.Range(minPeakUp, maxPeakUp);
            }
            else
            {
                // Pico hacia abajo: altura aleatoria entre min y max (negativo)
                peakHeight = -Random.Range(minPeakDown, maxPeakDown);
            }

            // Agregar nuevo punto SIEMPRE en startX (posición fija)
            Vector3 newPoint = new Vector3(startX, yPosition + peakHeight, 0);
            points.Add(newPoint);

            // Alternar para el siguiente pico
            nextPeakUp = !nextPeakUp;

            timeSinceLastPoint = 0f;
        }

        // Calcular velocidad de movimiento basada en peakSpacing y drawSpeed
        float moveSpeed = peakSpacing * drawSpeed;

        // Mover todos los puntos hacia la izquierda
        for (int i = 0; i < points.Count; i++)
        {
            points[i] += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // Eliminar puntos que salieron de la pantalla (muy a la izquierda)
        if (points.Count > maxPoints)
        {
            points.RemoveAt(0);
        }

        // Actualizar el LineRenderer
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}