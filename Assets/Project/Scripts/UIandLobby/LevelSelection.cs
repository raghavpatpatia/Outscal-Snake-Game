using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] int loadScene;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectGameMode);
    }

    private void SelectGameMode()
    {
        SceneManager.LoadScene(loadScene);
    }
}
