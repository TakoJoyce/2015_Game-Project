using UnityEngine;
using System.Collections;

public class FanLeaf : MagicObject
{
    Rigidbody2D rb;
    HingeJoint2D hinge;

    public bool isSpinning = true;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
    }

    public override void BecomeInteractable()
    {
        base.BecomeInteractable();

        Color newColor = spriteRenderer.color;
        newColor.a = 1.0f;
        spriteRenderer.color = newColor;

    }

    public override void BecomeNotInteractable()
    {
        base.BecomeNotInteractable();

        //weaken the color
        Color newColor = spriteRenderer.color;
        newColor.a = 0.3f;
        spriteRenderer.color = newColor;
    }

    public void OnButtonPressed()
    {
        if ((prismController.currerntMode & magicColor) != 0)
            Toggle();
    }
    void Toggle()
    {
        isSpinning = !isSpinning;
        hinge.useMotor = !hinge.useMotor;
        rb.freezeRotation = !rb.freezeRotation;
    }

}
