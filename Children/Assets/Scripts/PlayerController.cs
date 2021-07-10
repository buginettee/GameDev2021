using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameConstants gameConstants;

    private Rigidbody2D marioBody;
    private float moveHorizontal;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Transform enemyLocation;
    private bool countScoreState = false;
    public bool alive = true;
    public ParticleSystem dustCloud;

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
        gameConstants.isAlive = true;
        marioBody.MovePosition(new Vector2(gameConstants.startX, gameConstants.startY));
        countScoreState = false;
    }


    // for collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("QuestionBox"))
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false;
            dustCloud.Play();
        }

        if (col.gameObject.CompareTag("Mushroom"))
        {
            bool mushroomType = col.gameObject.GetComponentInChildren<ConsummableMushroom_Controller>().status;
            Debug.Log("mushroom type"+ mushroomType.ToString());
            GameManager.gameManagerInstance.collectMushroom(mushroomType);
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
                marioBody.AddForce(Vector2.up * gameConstants.upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                countScoreState = true;
                marioAnimator.SetBool("onGround", onGroundState);
            }

            moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < gameConstants.maxSpeed)
                    marioBody.AddForce(movement * gameConstants.speed);
            }

            if (Input.GetKeyDown("z"))
            {
                GameManager.gameManagerInstance.consumedMushroom(true);
            }
            if (Input.GetKeyDown("x"))
            {
                GameManager.gameManagerInstance.consumedMushroom(false);
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
                    GameManager.gameManagerInstance.increaseScore();
                }
            }
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float yoffset = (this.transform.position.y - collision.transform.position.y);
            // did not stomp
            if (yoffset < 0.2f)
            {
                alive = false;
                gameConstants.isAlive = false;
                GameManager.gameManagerInstance.onDeath();
            }
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            alive = false;
            gameConstants.isAlive = false;
            GameManager.gameManagerInstance.onDeath();
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Collected star");
            GameManager.gameManagerInstance.increaseScore();
        }
    }
}
