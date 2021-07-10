using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{

    private bool isBroken = false;
    public GameObject debrisPrefab;
    public int numberOfDebris = 5;
    public AudioSource breakSound;

    public GameObject coin;
    public SpawnManager spawnManager;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBroken)
        {
            isBroken = true;

            for (int i = 0; i < numberOfDebris; i++)
            {
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            }

            breakSound.Play();

            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;

            // spawn the coin
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            Instantiate(coin, newPosition, Quaternion.identity);

            // spawn new enemy
            spawnManager.spawnFromPooler(ObjectType.greenEnemy);

            Destroy(gameObject);
        }
    }
}
