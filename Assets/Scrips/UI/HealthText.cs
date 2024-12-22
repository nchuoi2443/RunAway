using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Vector3 moveSpeed;
    [SerializeField] private float timeToFade = 1f;
    [SerializeField] private Transform target; // Thêm biến target để theo dõi đối tượng bị sát thương

    RectTransform rectTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed = 0f;
    private Color startColor;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        //moveSpeed = new Vector3(0, 1, 0);
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        // Cập nhật vị trí của HealthText để theo dõi đối tượng bị sát thương
        if (target != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
            rectTransform.position = new Vector3(screenPos.x + 10, rectTransform.position.y + 5, rectTransform.position.z);
        }
        

        MoveText();
        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveText()
    {
        rectTransform.position += moveSpeed * Time.deltaTime;
    }

    // Phương thức để thiết lập đối tượng bị sát thương
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
