using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float endPositionY = -1000f;
    [SerializeField] private float startPositionY = -500f;
    [SerializeField] private GameObject endCreditObject;

    private RectTransform rectTransform;
    private bool isScrolling = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, startPositionY);
    }

    void Update()
    {
        if (isScrolling)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (rectTransform.anchoredPosition.y >= endPositionY)
            {
                rectTransform.anchoredPosition = new Vector2(0, endPositionY);
                isScrolling = false;
                endCreditObject.SetActive(true);
            }
        }
    }
}