using UnityEngine;
using System.Collections;

public class HugeStone : Stone
{
    ConstantForce2D cf;

    void Awake()
    {
        cf = GetComponent<ConstantForce2D>();
        cf.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        cf.enabled = true;
    }
}
