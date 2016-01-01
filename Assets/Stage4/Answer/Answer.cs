using UnityEngine;
using System.Collections;

public class Answer : TouchEvent
{
    public int value = 0;

	public override void OnTouch ()
	{
        value = (value + 1) % 4;
        float rotationZ = value * 90.0f;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotationZ);
	}
}