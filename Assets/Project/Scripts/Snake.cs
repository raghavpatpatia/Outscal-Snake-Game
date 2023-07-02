using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] Transform segmentPrefab;
    private Vector2 direction;
    public List<Transform> segments { get; private set; }
    private Food food;

    private void Start()
    {
        food = FindObjectOfType<Food>();
        direction = Vector2.up;
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x, 
            Mathf.Round(this.transform.position.y) + direction.y, 
            0.0f
        );
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction.y != -1)
            {
                direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction.y != 1)
            {
                direction = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction.x != 1)
            {
                direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction.x != -1)
            {
                direction = Vector2.right;
            }
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void Shrink()
    {
        if (segments.Count == 1)
        {
            Debug.Log("GameOver");
        }
        else
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.Remove(segments[segments.Count - 1]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food collidedFood = collision.gameObject.GetComponent<Food>();
        if (collidedFood != null)
        {
            if (collidedFood.GetFoodType() == FoodTypes.MassGainer)
            {
                Grow();
            }
            else if (collidedFood.GetFoodType() == FoodTypes.MassBurner)
            {
                Shrink();
            }
        }
    }
}
