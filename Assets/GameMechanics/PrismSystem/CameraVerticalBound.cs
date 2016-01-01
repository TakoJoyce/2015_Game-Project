using UnityEngine;
using System.Collections;

public class CameraVerticalBound : MonoBehaviour
{
    public PrismController prism; 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            prism.ClosePrism();
    }
}