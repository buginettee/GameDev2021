using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float startX = -3.25f;
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
    public Text mushroomText;
    private int mushroomsCollected = 0;
    public Text worldOrDeathText;

    private Animator marioAnimator;
    private AudioSource marioAudio;


    // Start is called before the first frame update
    void Start()
    {
        // 30 fps
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    public void onStart()
    {
        alive = true;
        marioBody.MovePosition(new Vector2(startX, startY));
        countScoreState = false;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        mushroomsCollected = 0;
        mushroomText.text = mushroomsCollected.ToString();
        worldOrDeathText.text = "World 1-1";
    }


    // for collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("QuestionBox"))
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        }

        if (col.gameObject.CompareTag("Mushroom"))
        {
            mushroomsCollected += 1;
            mushroomText.text = mushroomsCollected.ToString();
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
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
                marioAnimator.SetBool("onGround", onGroundState);
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
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                {
                    marioAnimator.SetTrigger("onSkid");
                }
                marioSprite.flipX = true;
            }

            if (Input.GetKeyDown("d") && !faceRightState)
            {
                faceRightState = true;
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                {
                    marioAnimator.SetTrigger("onSkid");
                }
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
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("player collided with gomba!");
            alive = false;
            worldOrDeathText.text = "You died!";
        }
    }
}
