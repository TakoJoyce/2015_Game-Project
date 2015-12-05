using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    Camera cam;
    public Transform target;

    PlayerController player;

    //configuration
    float smoothTime = 0.3f;
    Vector3 cameraVelocity = Vector3.zero;
    Vector3 focus = new Vector3(0.5f, 0.3f, 0.0f);

    void Awake()
    {
        cam = GetComponent<Camera>();
        player = target.GetComponent<PlayerController>();
    }
	
	void FixedUpdate ()
    {
        //水平方向：加速滑動、垂直方向：平台貼齊
        Vector3 currentFocus = cam.ViewportToWorldPoint(focus);
        Vector3 cameraDst = cam.transform.position + target.position - currentFocus;
        cameraDst.z = cam.transform.position.z; //Do not move in z-axis.
        if (!player.isGrounded)
        {
            cameraDst.y = cam.transform.position.y;
        }
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraDst, ref cameraVelocity, smoothTime);

    }
}
