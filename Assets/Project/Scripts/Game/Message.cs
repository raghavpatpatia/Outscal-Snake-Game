using System.Collections;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TextMeshProUGUI gameOverText;
    private IEnumerator flyingText;

    private void Start()
    {
        message.gameObject.SetActive(false);   
    }

    public void UpdateHiddenMessage(string message)
    {
        this.message.gameObject.SetActive(true);
        this.message.text = message;
        if (flyingText != null)
        {
            StopCoroutine(flyingText);
        }
        flyingText = TextMove();
        StartCoroutine(flyingText);
    }

    public void UpdateGameOverText(string message)
    {
        gameOverText.text = message;
    }

    private IEnumerator TextMove()
    {
        yield return new WaitForSeconds(0.2f);
        Vector2 initialPosition = message.transform.position;
        Vector2 targetPosition = initialPosition + Vector2.up * 30f;

        float t = 0f;
        float duration = 2f;
        while (t < duration)
        {
            message.transform.position = Vector2.Lerp(initialPosition, targetPosition, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        message.gameObject.SetActive(false);
        message.transform.position = initialPosition;
    }
}
