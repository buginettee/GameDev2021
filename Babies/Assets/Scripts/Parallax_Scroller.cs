using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Scroller : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform mario;

    public Transform mainCamera;

    public Renderer[] layers;

    public float[] speedMultiplier;

    private float previousXPositionMario;

    private float previousXPositionCamera;

    private float[] currentOffset;

    void Start()
    {
        currentOffset = new float[layers.Length];
        for (int i=0; i < layers.Length; i++)
        {
            currentOffset[i] = 0.0f;
        }
        previousXPositionMario = mario.transform.position.x;
        previousXPositionCamera = mainCamera.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(previousXPositionCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (int i=0; i < layers.Length; i++)
            {
                if(currentOffset[i] > 1.0f || currentOffset[i] < -1.0f)
                {
                    currentOffset[i] = 0;
                }
                float newOffset = mario.transform.position.x - previousXPositionMario;
                currentOffset[i] = currentOffset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(currentOffset[i], 0);
            }
        }
        previousXPositionCamera = mainCamera.transform.position.x;
        previousXPositionMario = mario.transform.position.x;
    }
}
