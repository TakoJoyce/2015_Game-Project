using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

    bool hasBeenSeen = false;
	
	void OnBecameVisible()
    {
        hasBeenSeen = true;
    }

    void OnBecameInvisible()
    {
        if (hasBeenSeen)
            Destroy(gameObject);
    }
}
