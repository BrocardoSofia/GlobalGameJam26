using UnityEngine;
using System.Collections.Generic;

public class PolygraphDrawer : MonoBehaviour
{
    [Header("Configuración Visual")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float drawSpeed = 30f;
    [SerializeField] private int maxPoints = 500;
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color lyingColor = Color.red;
    [SerializeField] private Color truthColor = Color.blue;

    [Header("Escala del Gráfico")]
    [SerializeField] private float horizontalScale = 0.1f;
    [SerializeField] private float verticalScale = 2f;

    [Header("Patrón Normal - Líneas Rectas")]
    [SerializeField] private float normalLineHeight = 0f;
    [SerializeField] private float normalPeakHeight = 0.5f;
    [SerializeField] private float normalValleyDepth = -0.3f;
    [SerializeField] private int normalFlatPoints = 20;      // Puntos en línea recta
    [SerializeField] private int normalRisePoints = 5;       // Puntos para subir
    [SerializeField] private int normalPeakPoints = 3;       // Puntos en el pico
    [SerializeField] private int normalFallPoints = 5;       // Puntos para bajar
    [SerializeField] private int normalValleyPoints = 3;     // Puntos en el valle

    [Header("Patrón Mintiendo")]
    [SerializeField] private float lyingPeakHeight = 1.0f;
    [SerializeField] private float lyingValleyDepth = -0.6f;
    [SerializeField] private int lyingFlatPoints = 8;
    [SerializeField] private int lyingRisePoints = 3;
    [SerializeField] private int lyingPeakPoints = 2;
    [SerializeField] private int lyingFallPoints = 3;
    [SerializeField] private int lyingValleyPoints = 2;
    [SerializeField] private float lyingJitter = 0.2f;       // Temblor en la línea

    [Header("Patrón Verdad")]
    [SerializeField] private float truthPeakHeight = 0.7f;
    [SerializeField] private float truthValleyDepth = -0.4f;
    [SerializeField] private int truthFlatPoints = 25;
    [SerializeField] private int truthRisePoints = 6;
    [SerializeField] private int truthPeakPoints = 4;
    [SerializeField] private int truthFallPoints = 6;
    [SerializeField] private int truthValleyPoints = 4;

    private List<Vector3> points = new List<Vector3>();
    private float currentX = 0f;
    private PolygraphState currentState = PolygraphState.Normal;
    private int pointCounter = 0;
    private float lastUpdateTime = 0f;

    // Estados del ciclo
    private enum CyclePhase { Flat, Rising, Peak, Falling, Valley }
    private CyclePhase currentPhase = CyclePhase.Flat;
    private int phaseCounter = 0;

    public enum PolygraphState
    {
        Normal,
        Lying,
        Truth
    }

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = normalColor;
        lineRenderer.endColor = normalColor;
        lineRenderer.useWorldSpace = true;

        // IMPORTANTE: Esto hace que las líneas sean rectas, no curvas
        lineRenderer.numCornerVertices = 0;
        lineRenderer.numCapVertices = 0;

        SetState(PolygraphState.Normal);
    }

    void Update()
    {
        if (Time.time - lastUpdateTime >= 1f / drawSpeed)
        {
            GenerateStraightLinePoint();
            lastUpdateTime = Time.time;
            pointCounter++;
            phaseCounter++;
        }

        if (points.Count > maxPoints)
        {
            points.RemoveAt(0);
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Vector3(points[i].x - horizontalScale, points[i].y, points[i].z);
            }
            currentX -= horizontalScale;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    void GenerateStraightLinePoint()
    {
        float y = 0f;

        switch (currentState)
        {
            case PolygraphState.Normal:
                y = GenerateNormalStraightPattern();
                break;
            case PolygraphState.Lying:
                y = GenerateLyingStraightPattern();
                break;
            case PolygraphState.Truth:
                y = GenerateTruthStraightPattern();
                break;
        }

        Vector3 newPoint = new Vector3(currentX, y * verticalScale, 0);
        points.Add(newPoint);
        currentX += horizontalScale;
    }

    float GenerateNormalStraightPattern()
    {
        float y = normalLineHeight;

        switch (currentPhase)
        {
            case CyclePhase.Flat:
                y = normalLineHeight;
                if (phaseCounter >= normalFlatPoints)
                {
                    currentPhase = CyclePhase.Rising;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Rising:
                // Subida lineal
                float riseProgress = (float)phaseCounter / normalRisePoints;
                y = Mathf.Lerp(normalLineHeight, normalPeakHeight, riseProgress);
                if (phaseCounter >= normalRisePoints)
                {
                    currentPhase = CyclePhase.Peak;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Peak:
                y = normalPeakHeight;
                if (phaseCounter >= normalPeakPoints)
                {
                    currentPhase = CyclePhase.Falling;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Falling:
                // Bajada lineal
                float fallProgress = (float)phaseCounter / normalFallPoints;
                y = Mathf.Lerp(normalPeakHeight, normalValleyDepth, fallProgress);
                if (phaseCounter >= normalFallPoints)
                {
                    currentPhase = CyclePhase.Valley;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Valley:
                y = normalValleyDepth;
                if (phaseCounter >= normalValleyPoints)
                {
                    currentPhase = CyclePhase.Flat;
                    phaseCounter = 0;
                }
                break;
        }

        return y;
    }

    float GenerateLyingStraightPattern()
    {
        float y = 0f;
        float jitter = Random.Range(-lyingJitter, lyingJitter);

        switch (currentPhase)
        {
            case CyclePhase.Flat:
                y = 0 + jitter;
                if (phaseCounter >= lyingFlatPoints)
                {
                    currentPhase = CyclePhase.Rising;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Rising:
                float riseProgress = (float)phaseCounter / lyingRisePoints;
                y = Mathf.Lerp(0, lyingPeakHeight, riseProgress) + jitter;
                if (phaseCounter >= lyingRisePoints)
                {
                    currentPhase = CyclePhase.Peak;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Peak:
                y = lyingPeakHeight + jitter;
                if (phaseCounter >= lyingPeakPoints)
                {
                    currentPhase = CyclePhase.Falling;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Falling:
                float fallProgress = (float)phaseCounter / lyingFallPoints;
                y = Mathf.Lerp(lyingPeakHeight, lyingValleyDepth, fallProgress) + jitter;
                if (phaseCounter >= lyingFallPoints)
                {
                    currentPhase = CyclePhase.Valley;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Valley:
                y = lyingValleyDepth + jitter;
                if (phaseCounter >= lyingValleyPoints)
                {
                    currentPhase = CyclePhase.Flat;
                    phaseCounter = 0;
                }
                break;
        }

        return y;
    }

    float GenerateTruthStraightPattern()
    {
        float y = 0f;

        switch (currentPhase)
        {
            case CyclePhase.Flat:
                y = 0;
                if (phaseCounter >= truthFlatPoints)
                {
                    currentPhase = CyclePhase.Rising;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Rising:
                float riseProgress = (float)phaseCounter / truthRisePoints;
                y = Mathf.Lerp(0, truthPeakHeight, riseProgress);
                if (phaseCounter >= truthRisePoints)
                {
                    currentPhase = CyclePhase.Peak;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Peak:
                y = truthPeakHeight;
                if (phaseCounter >= truthPeakPoints)
                {
                    currentPhase = CyclePhase.Falling;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Falling:
                float fallProgress = (float)phaseCounter / truthFallPoints;
                y = Mathf.Lerp(truthPeakHeight, truthValleyDepth, fallProgress);
                if (phaseCounter >= truthFallPoints)
                {
                    currentPhase = CyclePhase.Valley;
                    phaseCounter = 0;
                }
                break;

            case CyclePhase.Valley:
                y = truthValleyDepth;
                if (phaseCounter >= truthValleyPoints)
                {
                    currentPhase = CyclePhase.Flat;
                    phaseCounter = 0;
                }
                break;
        }

        return y;
    }

    public void SetState(PolygraphState newState)
    {
        currentState = newState;
        currentPhase = CyclePhase.Flat;
        phaseCounter = 0;

        switch (currentState)
        {
            case PolygraphState.Normal:
                lineRenderer.startColor = normalColor;
                lineRenderer.endColor = normalColor;
                Debug.Log("Estado: NORMAL - Pulso regular");
                break;

            case PolygraphState.Lying:
                lineRenderer.startColor = lyingColor;
                lineRenderer.endColor = lyingColor;
                Debug.Log("Estado: MINTIENDO - Señales erráticas");
                break;

            case PolygraphState.Truth:
                lineRenderer.startColor = truthColor;
                lineRenderer.endColor = truthColor;
                Debug.Log("Estado: VERDAD - Señales estables");
                break;
        }
    }

    public void ResetGraph()
    {
        points.Clear();
        currentX = 0f;
        pointCounter = 0;
        phaseCounter = 0;
        currentPhase = CyclePhase.Flat;
        lineRenderer.positionCount = 0;
    }
}