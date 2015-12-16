using UnityEngine;
using System.Collections;

public class ThrowerGoal : MonoBehaviour
{
    public GameObject thrower;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            thrower.SetActive(false);
        }
    }
}
