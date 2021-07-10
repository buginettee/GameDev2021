using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public List<QuestionBox_Controller> controllers = new List<QuestionBox_Controller>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStart()
    {
        foreach (QuestionBox_Controller child in controllers)
        {
            child.onStart();
        }


    }
}
