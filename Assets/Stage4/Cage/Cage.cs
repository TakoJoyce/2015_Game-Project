using UnityEngine;
using System.Collections;

public class Cage : MagicObject
{
    public AreaEffector2D hideArea;
    public AreaEffector2D windZone;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        hideArea.forceAngle = windZone.forceAngle - 180.0f;
    }

    void Update()
    {
        hideArea.forceMagnitude = windZone.forceMagnitude;
    }

    public override void OnPrismChange(MagicColor mode)
    {
        if ((magicColor & mode) != 0)
        {
            BecomeInteractable();

            switch (mode)
            {
                case MagicColor.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case MagicColor.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case MagicColor.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
            }
        }
        else
        {
            RefreshColor();
            BecomeNotInteractable();
        }
    }

    public override void OnPrismClose()
    {
        BecomeInteractable();
        RefreshColor();
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
        hideArea.enabled = false;
    }

    public override void BecomeInteractable()
    {
        base.BecomeInteractable();

        gameObject.layer = LayerMask.NameToLayer("MagicObject");
        hideArea.enabled = true;
    }

    void RefreshColor()
    {
        switch (magicColor)
        {
            case MagicColor.Red:
                spriteRenderer.color = Color.red;
                break;
            case MagicColor.Green:
                spriteRenderer.color = Color.green;
                break;
            case MagicColor.Blue:
                spriteRenderer.color = Color.blue;
                break;
            case MagicColor.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case MagicColor.Cyan:
                spriteRenderer.color = Color.cyan;
                break;
            case MagicColor.Magenta:
                spriteRenderer.color = Color.magenta;
                break;
        }
    }
}
