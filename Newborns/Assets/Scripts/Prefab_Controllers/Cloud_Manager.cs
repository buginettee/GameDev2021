using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject CloudLarge, CloudMedium, CloudSmall;

    private int choice; //0 none, 1 small, 2 medium, 3 large

    // Start is called before the first frame update
    void Start()
    {
        choice = Random.Range(0, 3);
        switch (choice)
        {
            case 0:
                CloudLarge.SetActive(false);
                CloudMedium.SetActive(false);
                CloudSmall.SetActive(false);
                break;
            case 1:
                CloudLarge.SetActive(false);
                CloudMedium.SetActive(false);
                CloudSmall.SetActive(true);
                break;
            case 2:
                CloudLarge.SetActive(false);
                CloudMedium.SetActive(true);
                CloudSmall.SetActive(false);
                break;
            case 3:
                CloudLarge.SetActive(true);
                CloudMedium.SetActive(false);
                CloudSmall.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
