using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject BushLarge, BushMedium, BushSmall;

    private int choice; //0 none, 1 small, 2 medium, 3 large

    // Start is called before the first frame update
    void Start()
    {
        choice = Random.Range(0, 3);
        switch (choice)
        {
            case 0:
                BushLarge.SetActive(false);
                BushMedium.SetActive(false);
                BushSmall.SetActive(false);
                break;
            case 1:
                BushLarge.SetActive(false);
                BushMedium.SetActive(false);
                BushSmall.SetActive(true);
                break;
            case 2:
                BushLarge.SetActive(false);
                BushMedium.SetActive(true);
                BushSmall.SetActive(false);
                break;
            case 3:
                BushLarge.SetActive(true);
                BushMedium.SetActive(false);
                BushSmall.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
