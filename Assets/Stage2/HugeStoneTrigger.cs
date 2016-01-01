using UnityEngine;

public class HugeStoneTrigger : MonoBehaviour
{

    public GameObject hugeStone;
    public Animator cameraAnim;
    public Transform spawnPoint;
    AudioSource vibrationSE;

    void Awake()
    {
        vibrationSE = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cameraAnim.SetTrigger("zoomOut");
            cameraAnim.SetTrigger("vibrate");
            vibrationSE.Play();
            Instantiate(hugeStone, spawnPoint.position, Quaternion.identity);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
