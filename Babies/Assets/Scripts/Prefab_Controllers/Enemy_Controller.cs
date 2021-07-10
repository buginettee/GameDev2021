using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 3.5f;
    private float enemyPatrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        // get starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            // move if still within offset limit
            MoveGomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("gomba collided with player!");
        }
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatrolTime, 0);
    }

    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }
}
