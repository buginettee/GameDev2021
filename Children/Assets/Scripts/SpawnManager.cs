using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Awake()
    {

    }
    // Start is called before the first frame update
    public void onStart()
    {
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void spawnFromPooler(ObjectType i)
    {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-1f, 1f), item.transform.position.y, 0);
            item.SetActive(true);
            item.transform.localScale = new Vector3(0.7f,0.7f,1.0f);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }
}
