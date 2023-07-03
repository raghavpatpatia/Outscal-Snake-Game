using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] Snake snake;
    private bool intersectsSnake;

    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        // RandomizePosition();
    }

    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 randomPos = new Vector2(Mathf.Round(x), Mathf.Round(y));

        intersectsSnake = false;

        foreach (Transform segment in snake.segments)
        {
            if (segment.position == (Vector3)randomPos)
            {
                intersectsSnake = true;
                break;
            }
        }

        if (intersectsSnake)
        {
            RandomizePosition();
        }

        else
        {
            this.transform.position = randomPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake>() != null)
        {
            RandomizePosition();
        }
    }
}
