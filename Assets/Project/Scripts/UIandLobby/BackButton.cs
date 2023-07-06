using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject OtherPanel;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BackButtonClick);
    }

    private void BackButtonClick()
    {
        SoundManager.Instance.PlayMusic(Sounds.ButtonClick);
        OtherPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}
