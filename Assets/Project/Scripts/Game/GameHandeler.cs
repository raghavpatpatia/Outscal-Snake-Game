using UnityEngine;
using UnityEngine.UI;

public class GameHandeler : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button playButton;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        playButton.onClick.AddListener(PlayButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void PlayButtonClick()
    {
        SoundManager.Instance.PlayMusic(Sounds.ButtonClick);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
