using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpro;
    private int score = 0;

    private void Start()
    {
        tmpro.text = "Score: " + score;
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        tmpro.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

}
