using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float startX = -6.6f;
    public float startY = -3.45f;

    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;

    private Rigidbody2D marioBody;
    private float moveHorizontal;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        // 30 fps
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    public void onStart()
    {
        alive = true;
        marioBody.MovePosition(new Vector2(startX, startY));
        countScoreState = false;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }


    // for collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        }
    }


    // for physics update
    void FixedUpdate()
    {
        if (alive)
        {
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                marioBody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                countScoreState = true;
            }

            moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }
        }
    }


    // for frame update
    void Update()
    {
        if (alive)
        {
            if (Input.GetKeyDown("a") && faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
            }

            if (Input.GetKeyDown("d") && !faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
            }

            if (!onGroundState && countScoreState)
            {
                if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
                {
                    countScoreState = false;
                    score++;
                    Debug.Log(score);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("player collided with gomba!");
            alive = false;

        }
    }
}
