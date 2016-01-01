using UnityEngine;
using System.Collections;

public class ChangeCameraConstrain : MonoBehaviour
{
    public PlayerCamera playerCam;
    public float buttomChangeTo = -Mathf.Infinity;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            playerCam.constrainBL = new Vector2(playerCam.constrainBL.x, buttomChangeTo);
        }
    }
}