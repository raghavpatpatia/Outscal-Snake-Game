using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] HiddenMessage message;
    [SerializeField] TextMeshProUGUI gameOverText;

    public void UpdateHiddenMessage(string message)
    {
        this.message.UpdateMessage(message);
    }

    public void UpdateGameOverText(string message)
    {
        gameOverText.text = message;
    }
}
