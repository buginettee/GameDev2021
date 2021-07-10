using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    public GameConstants gameConstants;
    public MenuController menuController;
    public QuestionBoxController questionBoxController;
    public ParallaxScroller parallaxScroller;
    public PlayerController playerController;
    public SpawnManager spawnManager;

    private void Awake()
    {
        gameManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore()
    {
        gameConstants.currentScore += 1;
        menuController.updateText();
    }

    public void onDeath()
    {
        gameConstants.currentStatus = "You died!";
        menuController.updateText();
    }

    public void collectMushroom(bool isOrange)
    {
        if (isOrange)
        {
            gameConstants.collectedOrange = true;
            menuController.updateText();
        }
        if (!isOrange)
        {
            gameConstants.collectedRed = true;
            menuController.updateText();
        }
    }

    public void consumedMushroom(bool isOrange)
    {
        if (isOrange && gameConstants.collectedOrange)
        {
            gameConstants.collectedOrange = false;
            menuController.updateText();
            gameConstants.speed *= 2;
            Debug.Log("increase speed");
            StartCoroutine(removeSpeed());
        }
        if (!isOrange && gameConstants.collectedRed )
        { 
            gameConstants.collectedRed = false;
            menuController.updateText();
            gameConstants.upSpeed += 10;
            Debug.Log("increase height");
            StartCoroutine(removeUpSpeed());
        }
    }

    IEnumerator removeUpSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        gameConstants.upSpeed -= 10;
    }

    IEnumerator removeSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        gameConstants.speed /= 2;
    }


    public void onRestartButtonClicked()
    {
        // reset relevant gameConstants
        gameConstants.currentScore = 0;
        gameConstants.currentStatus = "World 1-1";
        gameConstants.collectedOrange = false;
        gameConstants.collectedRed = false;
        gameConstants.speed = 15.0f;
        gameConstants.maxSpeed = 40.0f;
        gameConstants.upSpeed = 16.0f;

        // reset menu controller
        menuController.updateText();

        // reset question box controller
        questionBoxController.onStart();

        // reset parallax scroller controller
        parallaxScroller.onStart();

        // reset player controller
        playerController.onStart();

        // reset spawnManager
        spawnManager.onStart();
    }

    public void onStartButtonClicked()
    {
        Debug.Log("Starting");

        menuController.onStartButtonClicked();

        onRestartButtonClicked();
    }
}
