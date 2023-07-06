using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject levelPanel;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(StartButtonClick);
    }
    
    private void StartButtonClick()
    {
        SoundManager.Instance.PlayMusic(Sounds.ButtonClick);
        gamePanel.SetActive(false);
        levelPanel.SetActive(true);
    }
}
