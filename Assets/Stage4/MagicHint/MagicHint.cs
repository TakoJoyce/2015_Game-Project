using UnityEngine;
using System.Collections;

public class MagicHint : MagicObject
{
    Animator anim;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer.enabled = false;
    }

    public override void OnPrismChange(MagicColor mode)
    {
        if (mode == magicColor)
            spriteRenderer.enabled = true;
        else
            spriteRenderer.enabled = false;
    }

    public override void OnPrismClose()
    {
        spriteRenderer.enabled = false;
    }
}