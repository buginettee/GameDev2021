using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsummableMushroom_Controller : MonoBehaviour
{

    private Vector2 velocity;
    private Vector2 currentDirection;

    private Rigidbody2D mushroomBody;
    private bool canMove = true;

    private int initialDirection;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        initialDirection = Random.Range(0, 1);
        switch (initialDirection)
        {
            case (0):
                currentDirection = new Vector2(1, 0);
                break;
            case (1):
                currentDirection = new Vector2(-1, 0);
                break;
        }

        mushroomBody.AddForce(new Vector2(5,5), ForceMode2D.Impulse);
        velocity = new Vector2(5, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector2 nextPosition = mushroomBody.position + velocity * currentDirection * Time.fixedDeltaTime;
            mushroomBody.MovePosition(nextPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // change direction if enemy or pipe
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bounds") || collision.gameObject.CompareTag("Obstacles"))
        {
            currentDirection *= new Vector2(-1, 0);
        }

        // destroy when player touches it
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // change direction if enemy or pipe
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bounds") || collision.gameObject.CompareTag("Obstacles"))
        {
            currentDirection *= new Vector2(-1, 0);
        }

        // destroy when player touches it
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void onStart()
    {
        Destroy(gameObject);
    }
}
