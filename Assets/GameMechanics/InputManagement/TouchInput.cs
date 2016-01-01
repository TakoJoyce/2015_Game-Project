using UnityEngine;

//其實這些操作應該可以弄一個Dictionary之類的存起來再查詢，程式碼會好看一點，不過以後有空的時候再來改好了……

public class TouchInput : MonoBehaviour
{
    public Collider2D upButton, downButton, leftButton, rightButton, jumpButton;
    
    public bool dPadUp, dPadDown, dPadLeft, dPadRight, jump;

    int dPadUpId, dPadDownId, dPadLeftId, dPadRightId, jumpId;

    Touch[] touches;
    RaycastHit2D hitInfo;

    void Awake()
    {
        dPadUpId    = upButton.GetHashCode();
        dPadDownId  = downButton.GetHashCode();
        dPadLeftId  = leftButton.GetHashCode();
        dPadRightId = rightButton.GetHashCode();
        jumpId      = jumpButton.GetHashCode();
    }

    void Update ()
    {
        Clear();

        touches = Input.touches;
        foreach(Touch touch in touches)
        {
            Vector2 castPoint = Camera.main.ScreenToWorldPoint(touch.position);

            hitInfo = Physics2D.Linecast(castPoint, castPoint);
            if (hitInfo)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    TouchEvent te = hitInfo.transform.GetComponent<TouchEvent>();
                    if (te) te.OnTouch();
                }

                int id = hitInfo.collider.GetHashCode();
                if (id == dPadUpId)
                    dPadUp = true;
                else if (id == dPadDownId)
                    dPadDown = true;
                else if (id == dPadLeftId)
                    dPadLeft = true;
                else if (id == dPadRightId)
                    dPadRight = true;
                else if (id == jumpId)
                    jump = true;
            }
        }
    }

    public float GetMoveAxis()
    {
        if (dPadRight)
            return 1.0f;
        else if (dPadLeft)
            return -1.0f;
        else
            return 0.0f;
    }

    void Clear()
    {
        dPadUp = dPadDown = dPadLeft = dPadRight = jump = false;
    }
}
