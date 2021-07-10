using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    // on destroying the tile, spawn coin and for collecting
    public Transform starBody;

    // Start is called before the first frame update
    void Start()
    {
        //starBody.localPosition = new Vector3(0, 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
