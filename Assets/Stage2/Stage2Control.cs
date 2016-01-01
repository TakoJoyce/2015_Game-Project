using UnityEngine;
using System.Collections;

public class Stage2Control : StageControl
{
    public Collider2D hugeStoneTrigger;
    public StoneThrower stoneThrower;
    public Animator camAnim;

    public override void ResetStage()
    {
        hugeStoneTrigger.enabled = true;
        stoneThrower.ResetStatus();
        Stone[] stones = (Stone[]) GameObject.FindObjectsOfType<Stone>();
        for (int i = 0; i < stones.Length; i++)
            Destroy(stones[i].gameObject);

        camAnim.SetTrigger("toNormal");
    }
}
