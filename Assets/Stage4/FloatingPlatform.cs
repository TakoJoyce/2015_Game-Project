using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour
{
    public float posYUpperBound = Mathf.Infinity;
    Rigidbody2D rb;

    float accel = 2.5f;
    int weight = 0;
    int playerCol = 0;

    void Awake ()
	{
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        if (rb.position.y > posYUpperBound)
            rb.velocity = Vector2.zero;

        if (weight + (playerCol > 0 ? 1 : 0) < 2)
            rb.velocity = rb.velocity + new Vector2(0.0f, accel * Time.deltaTime);
        else if (weight + (playerCol > 0 ? 1 : 0) > 2)
            rb.velocity = rb.velocity - new Vector2(0.0f, accel * Time.deltaTime);
        else
            rb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerCol++;
        else
            weight++;

    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerCol--;
        else
            weight--;
    }
}