using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject MenuPanel;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(MenuButtonClick);
    }

    private void MenuButtonClick()
    {
        gamePanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
}
