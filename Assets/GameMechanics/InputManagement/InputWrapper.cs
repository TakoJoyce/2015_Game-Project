using UnityEngine;


public class InputWrapper : MonoBehaviour
{
    public TouchInput touchInput;

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
            touchInput.dPadRight = true;
        else if (Input.GetAxis("Horizontal") < 0)
            touchInput.dPadLeft = true;

        if (Input.GetButton("Jump"))
            touchInput.jump = true;
    }
    
}