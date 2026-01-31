using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class PolygraphDrawer : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float drawSpeed = 30f; // Puntos por segundo
    [SerializeField] private int maxPoints = 100; // Máximo de puntos antes de desaparecer

    [Header("Posición Inicial")]
    [SerializeField] private float startX = 5f; // Donde empieza la línea (derecha) - FIJO
    [SerializeField] private float yPosition = 0f; // Altura de la línea base

    [Header("Picos Aleatorios pulso Base")]
    [SerializeField] private float minPeakUp = 0.05f; // Mínimo pico hacia arriba
    [SerializeField] private float maxPeakUp = 0.8f; // Máximo pico hacia arriba
    [SerializeField] private float minPeakDown = 0.05f; // Mínimo pico hacia abajo
    [SerializeField] private float maxPeakDown = 0.8f; // Máximo pico hacia abajo
    [SerializeField] private float peakSpacing = 0.3f; // Distancia entre picos (controla velocidad)

    [Header("Picos Aleatorios pulso Normal")]
    [SerializeField] private float minPeakUpNormal = 0.05f;
    [SerializeField] private float maxPeakUpNormal = 0.8f;
    [SerializeField] private float minPeakDownNormal = 0.05f;
    [SerializeField] private float maxPeakDownNormal = 0.8f;
    [SerializeField] private float peakSpacingNormal = 0.3f;

    [Header("Picos Aleatorios pulso Mentira")]
    [SerializeField] private float minPeakUpLie = 0.3f;
    [SerializeField] private float maxPeakUpLie = 1f;
    [SerializeField] private float minPeakDownLie = 0.3f;
    [SerializeField] private float maxPeakDownLie = 1f;
    [SerializeField] private float peakSpacingLie = 0.15f;

    private List<Vector3> points = new List<Vector3>();
    private float timeSinceLastPoint = 0f;
    private bool nextPeakUp = true;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f; // Aumentado de 0.1f
        lineRenderer.endWidth = 0.1f;   // Aumentado de 0.1f
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.useWorldSpace = true;

        // Estos parámetros ayudan a que la línea se vea más gruesa y suave
        lineRenderer.numCornerVertices = 2;
        lineRenderer.numCapVertices = 2;
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

    public void StartLying()
    {
        Debug.Log("-");
        minPeakUp = minPeakUpLie;
        maxPeakUp = maxPeakUpLie;
        minPeakDown = minPeakDownLie;
        maxPeakDown = maxPeakDownLie;
        peakSpacing = peakSpacingLie;
    }

    public void StartNormal()
    {
        Debug.Log("-");
        minPeakUp = minPeakUpNormal;
        maxPeakUp = maxPeakUpNormal;
        minPeakDown = minPeakDownNormal;
        maxPeakDown = maxPeakDownNormal;
        peakSpacing = peakSpacingNormal;
    }
}