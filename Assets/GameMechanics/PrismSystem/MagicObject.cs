using UnityEngine;
using System.Collections;

[System.Flags]
public enum MagicColor : byte {Red = 1, Green = 2, Blue = 4,
                        Yellow = Red | Green, Cyan = Green | Blue, Magenta = Blue | Red,
                        White = Red | Green | Blue, None = 0};

public abstract class MagicObject : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected static PrismController prismController;

    protected bool interactable = true;
    protected bool isVisible = false;

    protected GameObject detachedObj;    //分離出來的物體的參考，以便讓derived class可以存取他

    [SerializeField]
    MagicColor _magicColor;
    public MagicColor magicColor
    {
        get
        {
            return _magicColor;
        }
        set
        {
            _magicColor = value;
            ChangeRendererColor(value);
        }
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeRendererColor(_magicColor);
    }

    protected virtual void Start()
    {
        if (prismController == null)
            prismController = GameObject.FindWithTag("StageManager").GetComponent<PrismController>();
        prismController.RegisterMagicObject(this);

    }

    protected virtual void OnDestroy()
    {
        prismController.DeregisterMagicObject(this);
    }

    void ChangeRendererColor(MagicColor color)
    {
        Color rendererColor = spriteRenderer.color;
        if ((magicColor & MagicColor.Red) != 0)
            rendererColor.r = 1.0f;
        else
            rendererColor.r = 0.0f;
        if ((magicColor & MagicColor.Green) != 0)
            rendererColor.g = 1.0f;
        else
            rendererColor.g = 0.0f;
        if ((magicColor & MagicColor.Blue) != 0)
            rendererColor.b = 1.0f;
        else
            rendererColor.b = 0.0f;
        spriteRenderer.color = rendererColor;
    }

    public virtual void OnPrismChange(MagicColor mode)
    {
        detachedObj = null; //先歸零

        if (magicColor == mode)
        {
            BecomeInteractable();
            return;
        }

        if ((magicColor & mode) != 0)
        {
            //減掉當前模式的顏色
            //subtract color of mode
            /*  true table:
                old color: 0 1 0 1
                subtract : 0 0 1 1
                result   : 0 1 0 0  */

			magicColor = magicColor & (MagicColor.White ^ mode);// & magicColor;
			//clone the same object in the same place
            detachedObj = (GameObject)Instantiate(gameObject, transform.position, transform.rotation);
            detachedObj.transform.SetParent(transform.parent);
			//
			MagicObject mo = detachedObj.GetComponent<MagicObject>();
            mo.magicColor = mode;
            mo.BecomeInteractable();
        }

        BecomeNotInteractable();
    }

    public virtual void OnPrismClose()
    {
        if (!interactable)
        {
            BecomeInteractable();
            //偵測重疊的物體
            RaycastHit2D[] hits;
            hits = Physics2D.LinecastAll(transform.position, transform.position);
            foreach (RaycastHit2D hit in hits)
            {
                //排除偵測到自己的狀況
                if (hit.transform == transform)
                    continue;
                //如果偵測到的物體是可以互動的MagicObject，把它的顏色加到自己身上，然後摧毀它。
                MagicObject mo;
                mo = hit.transform.GetComponent<MagicObject>();
                if ((mo != null) && mo.interactable)
                {
                    magicColor = magicColor | mo.magicColor;
                    Destroy(mo.gameObject);
                    break;
                }
            }
        }
    }

    public virtual void BecomeInteractable()
    {
        interactable = true;
    }

    public virtual void BecomeNotInteractable()
    {
        interactable = false;
    }

    void OnBecameInvisible() { isVisible = false; }

    void OnBecameVisible() { isVisible = true; }
}
