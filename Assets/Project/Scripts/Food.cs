using UnityEngine;

public enum FoodTypes
{
    MassBurner, MassGainer
}

class Food : MonoBehaviour
{
    [SerializeField] FoodTypes type;

    public FoodTypes GetFoodType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassGainer)
        {
            // Score Code (Adding Score)
        }
        else if (collision.gameObject.GetComponent<Snake>() != null && type == FoodTypes.MassBurner)
        {
            // Score code (Subtracting score)
        }
    }
}
