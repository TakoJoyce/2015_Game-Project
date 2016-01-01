using UnityEngine;
using System.Collections;

public class Stage4Control : StageControl
{
    public float moveSpeed = 2.0f;

    public Transform[] objsNeedToReset;
    Vector3[] posRecord;

    public Transform playerCam;

    void Awake()
    {
        posRecord = new Vector3[objsNeedToReset.Length];
        for (int i = 0; i < objsNeedToReset.Length; i++)
        {
            posRecord[i] = objsNeedToReset[i].position;
        }
    }

    public override void ResetStage()
    {
        for (int i = 0; i < objsNeedToReset.Length; i++)
        {
            objsNeedToReset[i].position = posRecord[i];
        }
    }
    /*
    IEnumerator ForceMoveCoroutine()
    {
        playerCam.Translate(new Vector3(moveSpeed * Time.deltaTime, 0.0f)
    }*/
}