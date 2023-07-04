using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [SerializeField] Transform segmentPrefab;
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] Score score;

    public List<Transform> segments { get; private set; }
    public bool scoreBoost { get; private set; }
    public bool hasShield { get; private set; }
    private bool speed;
    private int initialSize = 4;
    private Vector2 direction;
    private IEnumerator powerupTime;

    private void Start()
    {
        direction = Vector2.up;
        segments = new List<Transform>();
        segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }
        FindObjectOfType<Pickup>().GeneratePickup();
    }

    private void Update()
    {
        PlayerInput();
        ScreenWrap();
    }

    private void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector2(
            Mathf.Round(this.transform.position.x) + direction.x, 
            Mathf.Round(this.transform.position.y) + direction.y
        );
    }

    private void PowerupCoroutine()
    {
        if (powerupTime != null)
        {
            StopCoroutine(powerupTime);
        }
        powerupTime = PowerupActiveTime();
        StartCoroutine(powerupTime);
    }

    private void SpeedPowerup()
    {
        if (speed)
        {
            Time.fixedDeltaTime = 0.05f;
            PowerupCoroutine();
            speed = false;
        }
    }

    private void ScoreBoostandShieldPowerup()
    {
        PowerupCoroutine();
    }

    private IEnumerator PowerupActiveTime()
    {
        yield return new WaitForSeconds(3f);
        Time.fixedDeltaTime = 0.1f;
        scoreBoost = false;
        hasShield = false;
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

    private void ScreenWrap()
    {
        Bounds bounds = this.gridArea.bounds;

        if (this.transform.position.x > bounds.max.x)
        {
            transform.position = new Vector2(bounds.min.x, transform.position.y);
        }
        else if (this.transform.position.x < bounds.min.x)
        {
            transform.position = new Vector2(bounds.max.x, transform.position.y);
        }
        else if (this.transform.position.y > bounds.max.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.min.y);
        }
        else if (this.transform.position.y < bounds.min.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.max.y);
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
        if (score.GetScore() < 0)
        {
            GameOver();
        }
        else
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.Remove(segments[segments.Count - 1]);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over, restarting the level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food collidedFood = collision.gameObject.GetComponent<Food>();
        Powerup collidedPowerup = collision.gameObject.GetComponent<Powerup>();
        if (collidedFood != null)
        {
            if (collidedFood.GetFoodType() == FoodTypes.MassGainer)
            {
                Grow();
            }
            else if (collidedFood.GetFoodType() == FoodTypes.MassBurner)
            {
                if (hasShield == true)
                {
                    Debug.Log("Shield is activated.");
                }
                else
                {
                    Shrink();
                }
            }
        }

        else if (collidedPowerup != null)
        {
            if (collidedPowerup.GetPowerupType() == PowerupTypes.Speed)
            {
                speed = true;
                SpeedPowerup();
            }
            else if (collidedPowerup.GetPowerupType() == PowerupTypes.ScoreBoost)
            {
                scoreBoost = true;
                ScoreBoostandShieldPowerup();
            }
            else if (collidedPowerup.GetPowerupType() == PowerupTypes.Shield)
            {
                hasShield = true;
                ScoreBoostandShieldPowerup();
            }
        }

        else if (collision.gameObject.CompareTag("SnakeBody"))
        {
            if (hasShield)
            {
                Debug.Log("Shield is activated so nothing happened.");
            }
            else
            {
                GameOver();
                Debug.Log("Snake Attacked itself.");
            }         
        }

        if (collision.gameObject.CompareTag("Pickup"))
        {
            GameObject collidedPickup = collision.gameObject;
            if (FindObjectOfType<Pickup>().instantiatedPickup.Contains(collidedPickup))
            {
                FindObjectOfType<Pickup>().instantiatedPickup.Remove(collidedPickup);
                Destroy(collidedPickup);
            }
        }
    }
}
