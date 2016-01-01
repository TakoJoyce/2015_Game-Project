using UnityEngine;
using System.Collections;

public class MaigcSymbol : MagicObject
{
    Animator anim;

	protected override void Awake ()
	{
        base.Awake();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        Invoke("EnableAnimator", Random.Range(0.0f, 2.0f));
	}

    public override void OnPrismChange(MagicColor mode)
    {
        spriteRenderer.enabled = false;
    }

    public override void OnPrismClose()
    {
        spriteRenderer.enabled = true;
    }

    void EnableAnimator()
    {
        anim.enabled = true;
    }
}