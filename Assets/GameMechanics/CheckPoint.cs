using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    static PlayerController player;

    void Start()
    {
        if (player == null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject go in gos)
            {
                player = go.GetComponent<PlayerController>();
                if (player != null)
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            player.checkPoint = transform;
        }
    }
}
