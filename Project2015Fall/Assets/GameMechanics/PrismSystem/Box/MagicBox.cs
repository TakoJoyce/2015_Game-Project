using UnityEngine;
using System.Collections;

public class MagicBox : MagicObject
{
    Rigidbody2D rb;
    Collider2D col;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPrismChange(MagicColor mode)
    {
        base.OnPrismChange(mode);
        //指定分離出來的物體的速度，因為我發現Instantiate複製出來的物體，在物理引琴李面的速度不會保留，所以要重新指定。
        if (detachedObj != null)
          detachedObj.GetComponent<Rigidbody2D>().velocity = rb.velocity;
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
    }

    public override void BecomeInteractable()
    {
        base.BecomeInteractable();

        //BecomeNotInteractable的反操作
        Color newColor = spriteRenderer.color;
        newColor.a = 1.0f;
        spriteRenderer.color = newColor;

        gameObject.layer = LayerMask.NameToLayer("MagicObject");
    }
}
