using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    List<string> toKeep = new List<string>() { "Score Text", "Level Text", "Reset Button" };
    public PlayerController marioController;

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

    public void onRestartButtonClicked()
    {
        Debug.Log("Restarting");
        marioController.onStart();
    }
}
