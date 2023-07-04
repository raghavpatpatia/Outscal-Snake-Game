using UnityEngine;
using TMPro;
using System.Collections;

public class HiddenMessage : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private IEnumerator flyingText;
    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        this.gameObject.SetActive(false);
    }

    public void UpdateMessage(string message)
    {
        this.gameObject.SetActive(true);
        textMeshPro.text = message;
        if (flyingText != null)
        {
            StopCoroutine(flyingText);
        }
        flyingText = TextMove();
        StartCoroutine(flyingText);
    }

    private IEnumerator TextMove()
    {
        yield return new WaitForSeconds(0.2f);
        Vector2 initialPosition = transform.position;
        Vector2 targetPosition = initialPosition + Vector2.up * 30f;

        float t = 0f;
        float duration = 2f;
        while (t < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        this.gameObject.SetActive(false);
        transform.position = initialPosition;
    }


}
