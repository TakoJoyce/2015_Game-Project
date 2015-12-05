using UnityEngine;
using System.Collections.Generic;

public class PrismController : MonoBehaviour
{
    //public UnityStandardAssets.ImageEffects.ScreenOverlay screenOverlay;
    public UnityEngine.UI.Button redButton, greenButton, blueButton, whiteButton;
    
    public Animator swapAnim;
    static int toRedId, toGreenId, toBlueId, toWhiteId;

    public MagicColor currerntMode = MagicColor.White;

    List<MagicObject> magicObjectList;

    void Awake()
    {
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
        foreach (MagicObject mo in magicObjectList)
        {
            mo.OnPrismChange(mode);
        }

        switch (mode)
        {
            case MagicColor.Red:
                swapAnim.SetTrigger(toRedId);
                //screenOverlay.texture = Resources.Load<Texture2D>("RedCover");
                break;
            case MagicColor.Green:
                swapAnim.SetTrigger(toGreenId);
                //screenOverlay.texture = Resources.Load<Texture2D>("GreenCover");
                break;
            case MagicColor.Blue:
                swapAnim.SetTrigger(toBlueId);
               // screenOverlay.texture = Resources.Load<Texture2D>("BlueCover");
                break;
        }
        //Resources.UnloadUnusedAssets();

        currerntMode = mode;
    }

    public void ClosePrism()
    {
        currerntMode = MagicColor.White;
        //screenOverlay.texture = null;
        swapAnim.SetTrigger(toWhiteId);
        foreach (MagicObject mo in magicObjectList)
        {
            mo.OnPrismClose();
        }
    }

    public void RegisterMagicObject(MagicObject mo)
    {
        magicObjectList.Add(mo);
    }

    public void DeregisterMagicObject(MagicObject mo)
    {
        magicObjectList.Remove(mo);
    }
}
