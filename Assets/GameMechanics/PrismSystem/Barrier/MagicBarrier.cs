using UnityEngine;

public class MagicBarrier : MagicObject
{
    Collider2D col;
    static Transform player;

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Start()
    {
        base.Start();
    }

    public void OnWillRenderObject()
    {
        //直立的時候
        if (transform.rotation.eulerAngles.z == 0.0f)
        {
            spriteRenderer.sortingOrder = (transform.position.x > player.position.x) ? -1 : 1;
        }
        else    //水平的時候
        {
            spriteRenderer.sortingOrder = (transform.position.y > player.position.y) ? 1 : -1;
        }
    }

    public override void BecomeNotInteractable()
    {
        base.BecomeNotInteractable();

        spriteRenderer.enabled = false;
        //make it not interactable
        gameObject.layer = LayerMask.NameToLayer("MagicObject_NotInteractable");
        col.enabled = false;
    }

    public override void BecomeInteractable()
    {
        base.BecomeInteractable();

        //BecomeNotInteractable的反操作
        spriteRenderer.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("MagicObject");
        col.enabled = true;
    }
}
