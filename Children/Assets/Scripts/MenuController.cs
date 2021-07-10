using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    List<string> toKeep = new List<string>() { "Score Text", "Level Text", "Reset Button", "Mushroom Text" };
    public PlayerController marioController;
    public QuestionBoxController questionBoxController;
    public ParallaxScroller parallaxScroller;

    // read score from gameconstants
    public GameConstants gameConstants;

    // ui components
    public Text scoreText;
    public Text levelText;
    public GameObject orangeMushroom;
    public GameObject redMushroom;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }


    public void onStartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (!toKeep.Contains(eachChild.name))
            {
                Debug.Log("Child found. Name: " + eachChild.name);

                // disable the item
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    // just read from game constants and update the text
    public void updateText()
    {
        // change all the texts
        scoreText.text = gameConstants.currentScore.ToString();
        levelText.text = gameConstants.currentStatus;
        orangeMushroom.SetActive(gameConstants.collectedOrange);
        redMushroom.SetActive(gameConstants.collectedRed);
    }
}