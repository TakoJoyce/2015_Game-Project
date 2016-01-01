using UnityEngine;

public enum CameraState { Normal, Animation, Fixed, FixedByPrism, ForceForward};

public class PlayerCamera : MonoBehaviour
{
    Camera cam;
    public Transform target;

    PlayerController player;

    public Vector2 constrainBL, constrainTR;

    //configuration
    float smoothTime = 0.2f;
    float upperBound = 0.7f;
    float lowerBound = 0.3f;
    Vector3 anchor = new Vector3(0.5f, 0.3f, 0.0f);
    public CameraState state = CameraState.Normal;

    //use for calculation
    Vector3 cameraDst;  //camera destination
    Vector3 cameraVelocity = Vector3.zero;
    Vector2 fixedPoint = new Vector2();
    public int fixCount = 0;
    float forceSpeed;

    void Awake()
    {
        cam = GetComponent<Camera>();
        player = target.GetComponent<PlayerController>();
        
        //set initial position
        float origZ = cam.transform.position.z;
        Vector3 initPos = cam.transform.position + target.position - cam.ViewportToWorldPoint(anchor);
        initPos.z = origZ;
        cam.transform.position = initPos;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case CameraState.Normal:
                cameraDst = cam.transform.position;
                cameraDst.x = target.position.x;

                Vector3 playerPosInVP = cam.WorldToViewportPoint(target.position);
                if (player.isGrounded)
                {
                    cameraDst = cam.transform.position + target.position - cam.ViewportToWorldPoint(anchor);
                }
                else if (playerPosInVP.y > upperBound)
                {
                    cameraDst.y += target.position.y - cam.ViewportToWorldPoint(new Vector3(0.0f, upperBound, 0.0f)).y;
                }
                else if (playerPosInVP.y < lowerBound)
                {
                    cameraDst.y -= cam.ViewportToWorldPoint(new Vector3(0.0f, lowerBound, 0.0f)).y - target.position.y;
                }

                break;
            case CameraState.FixedByPrism:
            case CameraState.Fixed:
                cameraDst.x = fixedPoint.x;
                cameraDst.y = fixedPoint.y;
                break;
            case CameraState.ForceForward:
                cameraDst = cam.transform.position;
                cameraDst.x += forceSpeed * Time.deltaTime;
                break;
        }

        cameraDst.z = cam.transform.position.z; //Do not move in z-axis.
        if (cameraDst.x > constrainTR.x || cameraDst.x < constrainBL.x)
            cameraDst.x = Mathf.Clamp(cameraDst.x, constrainBL.x, constrainTR.x);
        if (cameraDst.y > constrainTR.y || cameraDst.y < constrainBL.y)
            cameraDst.y = Mathf.Clamp(cameraDst.y, constrainBL.y, constrainTR.y);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraDst, ref cameraVelocity, smoothTime);
    }

    public void AimImmediately()
    {
        Vector3 newPos = cam.transform.position + target.position - cam.ViewportToWorldPoint(anchor);
        newPos.z = cam.transform.position.z;
        cam.transform.position = newPos;
        cameraDst.z = cam.transform.position.z; //Do not move in z-axis.
        if (cameraDst.x > constrainTR.x || cameraDst.x < constrainBL.x)
            cameraDst.x = Mathf.Clamp(cameraDst.x, constrainBL.x, constrainTR.x);
        if (cameraDst.y > constrainTR.y || cameraDst.y < constrainBL.y)
            cameraDst.y = Mathf.Clamp(cameraDst.y, constrainBL.y, constrainTR.y);
    }

    public void ControlByAnimation()
    {
        state = CameraState.Animation;
    }

    public void FixCamera(Vector2 pos)
    {
        fixedPoint = pos;
        state = CameraState.Fixed;
    }

    public void FixByPrism(Vector2 pos)
    {

        fixedPoint = pos;
        state = CameraState.FixedByPrism;
    }

    public void ReleaseCam()
    {
        state = CameraState.Normal;
    }

    public void ForceForward(float speed)
    {
        state = CameraState.ForceForward;
        forceSpeed = speed;
    }
}
