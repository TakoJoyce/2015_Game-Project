using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour
{

    bool isVisible = true;
    
    void Awake()
    {
        StartCoroutine(TryToDestroy());
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }
    
    void OnBecameInvisible()
    {
        isVisible = false;
    }

    IEnumerator TryToDestroy()
    {
        yield return new WaitForSeconds(5.0f);

        while (isVisible)
            yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
