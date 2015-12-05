using UnityEngine;
using System.Collections;

public class MagicPlatform : MagicObject {

    Collider2D col;
    Animator anim;
    static int appearId;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        appearId = Animator.StringToHash("appear");
    }

    protected override void Start ()
    {
        base.Start();
	}

    public override void OnPrismChange(MagicColor mode)
    {
        if ((mode & magicColor) != MagicColor.None)
        {
            col.isTrigger = false;
            spriteRenderer.enabled = true;
            anim.SetBool(appearId, true);
        }
        else
        {
            col.isTrigger = true;
            spriteRenderer.enabled = false;
            anim.SetBool(appearId, false);
        }
    }

    public override void OnPrismClose()
    {
        col.isTrigger = true;
        spriteRenderer.enabled = true;
        anim.SetBool(appearId, false);
    }
}
