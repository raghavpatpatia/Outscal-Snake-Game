using UnityEngine;

public enum FoodTypes
{
    MassBurner, MassGainer
}

class Food : MonoBehaviour
{
    [SerializeField] FoodTypes type;
    [SerializeField] Score score;

    public FoodTypes GetFoodType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassGainer)
        {
            score.UpdateScore(10);
        }
        else if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassBurner)
        {
            score.UpdateScore(-2);
        }
    }
}
