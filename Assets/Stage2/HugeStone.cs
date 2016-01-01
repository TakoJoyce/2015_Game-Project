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

    void FixedUpdate()
    {
        if (cf.enabled == true)
        {
            cf.force = cf.force + new Vector2(40.0f * Time.fixedDeltaTime, 0.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Invoke("StartPush", 1.0f);
    }

    void StartPush()
    {
        cf.enabled = true;
    }
}
