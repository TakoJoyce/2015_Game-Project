using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageControl : MonoBehaviour {
    
    public RawImage blackCover;

    public bool isInTransition = false;

    float fadeTime = 0.5f;

	public virtual void ResetStage()
    {
    }

    public IEnumerator FadeOutScreen()
    {
        isInTransition = true;
        float timer = 0.0f;
        Color newColor = blackCover.color;
        while (timer < fadeTime)
        {
            newColor.a = timer / fadeTime;
            blackCover.color = newColor;
            yield return null;
            timer += Time.deltaTime;
        }
        newColor.a = 1.0f;
        blackCover.color = newColor;
        isInTransition = false;
    }

    public IEnumerator FadeInScreen()
    {
        isInTransition = true;
        float timer = 0.0f;
        Color newColor = blackCover.color;
        while (timer < fadeTime)
        {
            newColor.a = 1 - (timer / fadeTime);
            blackCover.color = newColor;
            yield return null;
            timer += Time.deltaTime;
        }
        newColor.a = 0.0f;
        blackCover.color = newColor;
        isInTransition = false;

    }
}
