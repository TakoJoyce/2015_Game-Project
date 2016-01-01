using UnityEngine;
using System.Collections;

public class HugeStoneGoal : MonoBehaviour
{
    public Animator cameraAnim;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("KillZone"))
            cameraAnim.SetTrigger("toNormal");
    }
}