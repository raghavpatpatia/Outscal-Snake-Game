using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;
    [SerializeField] Snake snake;
    [SerializeField] GameObject[] prefabsPickups;
    private bool intersectsSnake;
    public List<GameObject> instantiatedPickup { get; private set; }

    private void Start()
    {
        instantiatedPickup = new List<GameObject>();
        GeneratePickup();
    }

    public void GeneratePickup()
    {
        InvokeRepeating("RandomizePosition", 0f, 8f);
        InvokeRepeating("DestroyPickup", 8f, 8f);
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        foreach (GameObject prefab in prefabsPickups)
        {
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
                GameObject pickup = Instantiate(prefab, randomPos, Quaternion.identity);
                pickup.tag = "Pickup";
                instantiatedPickup.Add(pickup);
            }
        }
    }

    private void DestroyPickup()
    {
        foreach (GameObject pickup in instantiatedPickup)
        {
            Destroy(pickup);
        }
        instantiatedPickup.Clear();
    }

}
