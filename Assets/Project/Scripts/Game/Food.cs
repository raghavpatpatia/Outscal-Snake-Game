using System;
using System.Collections;
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
        Snake snake = collision.gameObject.GetComponent<Snake>();
        if (snake != null && type == FoodTypes.MassGainer)
        {
            if (snake.scoreBoost == true)
            {
                score.UpdateScore(2);
            }
            else
            {
                score.UpdateScore(1);
            }
        }
        else if (snake != null && type == FoodTypes.MassBurner && snake.hasShield == false)
        {
            score.UpdateScore(-1);
        }
    }
}
