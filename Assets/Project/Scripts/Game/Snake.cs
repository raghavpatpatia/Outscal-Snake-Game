using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Player
{
    Player1, Player2
}

public class Snake : MonoBehaviour
{
    [SerializeField] Transform segmentPrefab;
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] Score score;
    [SerializeField] KeyCode up;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] Player player;
    [SerializeField] GameHandeler gameHandeler;
    [SerializeField] Message message;

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
        speed = false;
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(up))
        {
            if (direction.y != -1)
            {
                direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(down))
        {
            if (direction.y != 1)
            {
                direction = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(left))
        {
            if (direction.x != 1)
            {
                direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(right))
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
        score.UpdateScore(scoreBoost ? 2 : 1);
    }

    private void Shrink()
    {
        if (score.GetScore() <= 0)
        {
            message.UpdateGameOverText("Score less than 0");
            SoundManager.Instance.PlayMusic(Sounds.SnakeDeath);
            GameOver();
        }
        else
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.Remove(segments[segments.Count - 1]);
            score.UpdateScore(hasShield ? 0 : -1);
        }
    }

    private void GameOver()
    {
        gameHandeler.GameOverPanel();
    }

    private void SnakeAttackSelf()
    {
        if (hasShield)
        {
            message.UpdateHiddenMessage("Shield is activated");
        }
        else
        {
            message.UpdateGameOverText(player.ToString() + " attacked itself");
            GameOver();
        }
    }

    private void SnakeAttackOther(string message)
    {
        this.message.UpdateGameOverText(message);
        GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food collidedFood = collision.gameObject.GetComponent<Food>();
        Powerup collidedPowerup = collision.gameObject.GetComponent<Powerup>();
        if (collidedFood != null)
        {
            SoundManager.Instance.PlayMusic(Sounds.Pickup);
            if (collidedFood.GetFoodType() == FoodTypes.MassGainer)
            {
                Grow();
            }
            else if (collidedFood.GetFoodType() == FoodTypes.MassBurner)
            {
                if (hasShield == true)
                {
                    message.UpdateHiddenMessage("Shield is activated");
                }
                else
                {
                    Shrink();
                }
            }
        }

        else if (collidedPowerup != null)
        {
            SoundManager.Instance.PlayMusic(Sounds.Pickup);
            if (collidedPowerup.GetPowerupType() == PowerupTypes.Speed)
            {
                message.UpdateHiddenMessage("Pickup Speed");
                speed = true;
                SpeedPowerup();
            }
            else if (collidedPowerup.GetPowerupType() == PowerupTypes.ScoreBoost)
            {
                message.UpdateHiddenMessage("Pickup ScoreBoost");
                scoreBoost = true;
                ScoreBoostandShieldPowerup();
            }
            else if (collidedPowerup.GetPowerupType() == PowerupTypes.Shield)
            {
                message.UpdateHiddenMessage("Pickup Shield");
                hasShield = true;
                ScoreBoostandShieldPowerup();
            }
        }

        else if (player == Player.Player1)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SoundManager.Instance.PlayMusic(Sounds.SnakeDeath);
                SnakeAttackSelf();
            }
            else if (collision.gameObject.CompareTag("Player2"))
            {
                SoundManager.Instance.PlayMusic(Sounds.SnakeDeath);
                SnakeAttackOther("Blue Snake wins.");
            }
        }

        else if (player == Player.Player2)
        {
            if (collision.gameObject.CompareTag("Player2"))
            {
                SoundManager.Instance.PlayMusic(Sounds.SnakeDeath);
                SnakeAttackSelf();
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                SoundManager.Instance.PlayMusic(Sounds.SnakeDeath);
                SnakeAttackOther("Red Snake wins.");
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
