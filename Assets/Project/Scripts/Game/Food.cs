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
}
