using UnityEngine;
using System.Collections.Generic;

public class PrismController : MonoBehaviour
{
    public PlayerController player;
    public PlayerCamera playerCam;
    BoxCollider2D playerBoxCol;
    CircleCollider2D playerCircleCol;
    AudioSource transSE;

    public UnityEngine.UI.Button redButton, greenButton, blueButton, whiteButton;
    
    public Animator swapAnim;
    static int toRedId, toGreenId, toBlueId, toWhiteId;

    public MagicColor currerntMode = MagicColor.White;

    List<MagicObject> magicObjectList;

    void Awake()
    {
        playerBoxCol = player.GetComponent<BoxCollider2D>();
        playerCircleCol = player.GetComponent<CircleCollider2D>();
        transSE = GetComponent<AudioSource>();

        //button delegate
        redButton.onClick.AddListener(delegate { ChangeTo(MagicColor.Red); });
        greenButton.onClick.AddListener(delegate { ChangeTo(MagicColor.Green); });
        blueButton.onClick.AddListener(delegate { ChangeTo(MagicColor.Blue); });
        whiteButton.onClick.AddListener(delegate { ClosePrism(); });

        //animator parameter id
        toRedId = Animator.StringToHash("toRed");
        toGreenId = Animator.StringToHash("toGreen");
        toBlueId = Animator.StringToHash("toBlue");
        toWhiteId = Animator.StringToHash("toWhite");

        //計算List所需空間
        int capacity = GameObject.FindGameObjectsWithTag("MagicObject").Length * 3;
        magicObjectList = new List<MagicObject>(capacity);
    }

    public void ChangeTo(MagicColor mode)
    {
        if (currerntMode == mode)
            return;

        transSE.Play();

        foreach (MagicObject mo in magicObjectList)
        {
            mo.OnPrismChange(mode);
        }

        switch (mode)
        {
            case MagicColor.Red:
                swapAnim.SetTrigger(toRedId);
                break;
            case MagicColor.Green:
                swapAnim.SetTrigger(toGreenId);
                break;
            case MagicColor.Blue:
                swapAnim.SetTrigger(toBlueId);
                break;
        }
        PushPlayer();
        if (playerCam.state == CameraState.Normal)
            playerCam.FixByPrism(playerCam.transform.position);
        currerntMode = mode;
    }

    public void ClosePrism()
    {
        if (currerntMode == MagicColor.White)
            return;

        transSE.Play();

        currerntMode = MagicColor.White;
        swapAnim.SetTrigger(toWhiteId);
        foreach (MagicObject mo in magicObjectList)
        {
            mo.OnPrismClose();
        }
        if (playerCam.state == CameraState.FixedByPrism)
            playerCam.ReleaseCam();
        PushPlayer();
    }

    public void RegisterMagicObject(MagicObject mo)
    {
        magicObjectList.Add(mo);
    }

    public void DeregisterMagicObject(MagicObject mo)
    {
        magicObjectList.Remove(mo);
    }

    void PushPlayer()
    {
        float detectStartY = player.transform.position.y + playerBoxCol.size.y * 0.5f + playerBoxCol.offset.y;
        Vector2 detectStart = new Vector2(player.transform.position.x, detectStartY);
        float detectEndY = player.transform.position.y - playerCircleCol.radius + playerCircleCol.offset.y;
        Vector2 detectEnd = new Vector2(player.transform.position.x, detectEndY);
        RaycastHit2D hit =  Physics2D.Linecast(detectStart, detectEnd, 1 << LayerMask.NameToLayer("MagicObject"));

        if (hit && !hit.collider.isTrigger)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.position = new Vector2(rb.position.x, rb.position.y + Vector3.Distance(detectStart, detectEnd) - hit.distance);
        }
    }
}
