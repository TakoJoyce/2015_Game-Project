using UnityEngine;
using System.Collections;

public class HugeStoneTrigger : MonoBehaviour {

    public GameObject hugeStone;
    public Animator cameraAnim;
    public Transform spawnPoint;
    

	void OnTriggerEnter2D()
    {
        cameraAnim.SetTrigger("zoomOut");
        cameraAnim.SetTrigger("vibrate");
        Invoke("CameraZoom", 8.0f);
        Instantiate(hugeStone, spawnPoint.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    void CameraZoom()
    {
        cameraAnim.SetTrigger("toNormal");
    }
}
