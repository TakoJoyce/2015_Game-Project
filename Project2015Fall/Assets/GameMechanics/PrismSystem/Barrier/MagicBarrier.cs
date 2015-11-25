using UnityEngine;
using System.Collections;

public class MagicBarrier : MagicObject
{
    Collider2D col;

    protected override void Awake()
    {
        base.Awake();
        
        col = GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void BecomeNotInteractable()
    {
        base.BecomeNotInteractable();

        //weaken the color
        Color newColor = spriteRenderer.color;
        newColor.a = 0.3f;
        spriteRenderer.color = newColor;
        //make it not interactable
        gameObject.layer = LayerMask.NameToLayer("MagicObject_NotInteractable");
        col.isTrigger = true;
    }

    public override void BecomeInteractable()
    {
        base.BecomeInteractable();

        //BecomeNotInteractable的反操作
        Color newColor = spriteRenderer.color;
        newColor.a = 1.0f;
        spriteRenderer.color = newColor;

        gameObject.layer = LayerMask.NameToLayer("MagicObject");
        col.isTrigger = false;
    }
}
