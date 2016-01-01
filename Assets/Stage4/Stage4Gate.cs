using UnityEngine;
using System.Collections;

public class Stage4Gate : MonoBehaviour
{
    public Answer ans1, ans2, ans3, ans4;

    int[] ans = { 1, 2, 0, 3 };
    bool correct = false;

	void Start()
    {
        StartCoroutine(CheckAns());
    }

    IEnumerator CheckAns()
    {
        while (!correct)
        {
            if (ans1.value == ans[0] && ans2.value == ans[1] && ans3.value == ans[2] && ans4.value == ans[3])
            {
                correct = true;
                StartCoroutine(OpenGate());
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator OpenGate()
    {
        float destination = transform.position.y + 8.0f;
        while (transform.position.y < destination)
        {
            transform.Translate(0.0f, 6.0f * Time.deltaTime, 0.0f);
            yield return null;
        }
    }
}