using UnityEngine;

public class OldPlayerCamera : MonoBehaviour
{
    Camera cam;
    public Transform target;

    PlayerController player;

    //configuration
    float smoothTime = 0.3f;
    float fallingThreshold = 0.15f;
    Vector3 cameraVelocity = Vector3.zero;
    Vector3 normalFocus = new Vector3(0.5f, 0.3f, 0.0f);
    Vector3 fallingFocus = new Vector3(0.5f, 0.7f, 0.0f);
    int cameraState = 0;    //0 : normal, 1 : falling

    //use for calculation
    Vector3 currentFocus;
    Vector3 cameraDst;  //camera destination

    void Awake()
    {
        cam = GetComponent<Camera>();
        player = target.GetComponent<PlayerController>();
    }
	
	void FixedUpdate ()
    {
        switch (cameraState)
        {
            //normal case
            case 0:
                Vector3 fallingLine = cam.ViewportToWorldPoint(new Vector3(0.0f, fallingThreshold, 0.0f));
                if (fallingLine.y > player.transform.position.y)
                {
                    currentFocus = cam.ViewportToWorldPoint(fallingFocus);
                    cameraState = 1;
                }
                else
                {
                    currentFocus = cam.ViewportToWorldPoint(normalFocus);
                }

                cameraDst = cam.transform.position + (target.position - currentFocus);
                if (!player.isGrounded)
                    cameraDst.y = cam.transform.position.y;

                break;
            //when falling
            case 1:
                if (player.isGrounded)
                {
                    currentFocus = cam.ViewportToWorldPoint(normalFocus);
                    cameraState = 0;
                }
                else
                {
                    currentFocus = cam.ViewportToWorldPoint(fallingFocus);
                }

                cameraDst = cam.transform.position + (target.position - currentFocus);
                break;
        }
        
        cameraDst.z = cam.transform.position.z; //Do not move in z-axis.
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraDst, ref cameraVelocity, smoothTime);

    }
}
