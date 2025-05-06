using UnityEngine;

public class FloatingBounceUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 startPos;
    private Vector3 originalScale;

    [Header("Bounce Settings")]
    public float bounceHeight = 10f;      
    public float bounceSpeed = 2f;        
    public float scaleAmplitude = 0.05f;  

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        float scaleOffset = Mathf.Sin(Time.time * bounceSpeed) * scaleAmplitude;

        rectTransform.anchoredPosition = new Vector2(startPos.x, startPos.y + yOffset);
        rectTransform.localScale = originalScale + new Vector3(scaleOffset, scaleOffset, 0f);
    }
}