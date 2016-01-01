using UnityEngine;
using System.Collections;

public class BackgroundAnim : MonoBehaviour
{
    public float duration = 3.0f;
    Material mat;

    float timer = 0.0f;
    Vector2 offset;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
        offset = new Vector2();
    }

	void Update ()
	{
        timer += Time.deltaTime;
        if (timer > duration)
            timer -= duration;
        offset.x = timer / duration;

        mat.SetTextureOffset("_MainTex", offset);
	}
}