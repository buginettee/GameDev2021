using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject MountainLarge, MountainSmall;

    private int choice; //0 for none, 1 for small, 2 for large

    // Start is called before the first frame update
    void Start()
    {
        choice = Random.Range(0, 2);
        switch (choice)
        {
            case 0:
                MountainLarge.SetActive(false);
                MountainSmall.SetActive(false);
                break;
            case 1:
                MountainLarge.SetActive(false);
                MountainSmall.SetActive(true);
                break;
            case 2:
                MountainLarge.SetActive(true);
                MountainSmall.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
