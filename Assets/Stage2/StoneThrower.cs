using UnityEngine;
using System.Collections;

public class StoneThrower : MonoBehaviour {

    public GameObject stone;
    public Animator cameraAnim;

    bool activated = false;

    float throwInterval = 3.5f;
    float stoneVelo = 0.0f;

	void Start()
    {
        StartCoroutine(Throw());
    }
    
    IEnumerator Throw()
    {
        while (true)
        {
            if (!activated)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            GameObject go = (GameObject)Instantiate(stone, transform.position, Quaternion.identity);
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            Vector2 newVelo = rb.velocity;
            newVelo.x -= stoneVelo;
            rb.velocity = newVelo;
            yield return new WaitForSeconds(throwInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cameraAnim.SetTrigger("vibrate");
            activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
            activated = false;
    }
}
