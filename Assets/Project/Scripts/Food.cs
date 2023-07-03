using UnityEngine;

public enum FoodTypes
{
    MassBurner, MassGainer
}

class Food : MonoBehaviour
{
    [SerializeField] FoodTypes type;
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    public FoodTypes GetFoodType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassGainer)
        {
            score.UpdateScore(1);
        }
        else if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassBurner)
        {
            score.UpdateScore(-1);
        }
    }
}
