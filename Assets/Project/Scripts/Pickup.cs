using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] BoxCollider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 randomPos = new Vector2(Mathf.Round(x), Mathf.Round(y));

        //Collider2D collider = Physics2D.OverlapPoint(randomPos);
        
        //if (collider != null)
        //{
        //    SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
        //    if (spriteRenderer != null && (spriteRenderer.sortingLayerName == this.sortingLayerName))
        //    {
        //        RandomizePosition();
        //    }
        //}        

        this.transform.position = randomPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake>() != null)
        {
            RandomizePosition();
        }
    }
}
