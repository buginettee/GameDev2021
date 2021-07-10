using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimationController : MonoBehaviour
{
    private Animator flowerAnimator;
    public GameConstants gameConstants;

    // Start is called before the first frame update
    void Start()
    {
        flowerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameConstants.isAlive)
        {
            flowerAnimator.SetBool("PlayerDeath", true);
        }
    }
}
