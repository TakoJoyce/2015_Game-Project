using UnityEngine;
using UnityEngine.Events;

public class FanButton : MonoBehaviour
{
    public Sprite pressed, unpressed;
    public GameObject fanLeavesObject;
    public AreaEffector2D windZone;
    public ParticleSystem windParticle;
    public float windMagnitudeLow = 100,
                    windMagnitudeMid = 200,
                    windMagnitudeHigh = 300,
                    windParticleLow = 3,
                    windParticleMid = 8,
                    windParticleHigh = 16,
                    windSpeedLow = 5,
                    windSpeedMid = 8,
                    windSpeedHigh = 16;
    public float allowedBias = 25;

    SpriteRenderer  sRenderer;
    PrismController prismController;

    short pressWeight = 0;  //有多少player站在按鈕上？

    [SerializeField]bool _isPressed = false;
    public bool isPressed
    {
        get { return _isPressed; }
        set { _isPressed = value; sRenderer.sprite = isPressed ? pressed : unpressed; }
    }
    
    void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            pressWeight++;

            if (!isPressed)
            {
                isPressed = true;
                int numOfSpinningLeaves = 0;
                foreach (FanLeaf f in fanLeavesObject.GetComponentsInChildren<FanLeaf>())
                {
                    f.OnButtonPressed();
                    if (f.isSpinning)
                        numOfSpinningLeaves++;
                }
                switch (numOfSpinningLeaves)
                {
                    case 0:
                        windZone.forceMagnitude = 0;
                        windParticle.emissionRate = 0;
                        break;
                    case 1:
                        windZone.forceMagnitude = windMagnitudeLow;
                        windParticle.emissionRate = windParticleLow;
                        windParticle.startSpeed = windSpeedLow;
                        break;
                    case 2:
                        windZone.forceMagnitude = windMagnitudeMid;
                        windParticle.emissionRate = windParticleMid;
                        windParticle.startSpeed = windSpeedMid;
                        break;
                    case 3:
                        OnLeavesAllOpen();
                        break;
                    default:
                        windZone.forceMagnitude = 0;
                        windParticle.emissionRate = 0;
                        break;
                }
            }
        }
    }

    void OnLeavesAllOpen()
    {
        //計算三片扇葉的角度差，如果其中兩個的數字在120±bias的範圍內的話，就把風扇的強度調到high
        FanLeaf[] fanLeaves = fanLeavesObject.GetComponentsInChildren<FanLeaf>();
        float delta1 = Quaternion.Angle(fanLeaves[0].transform.rotation, fanLeaves[1].transform.rotation);
        float delta2 = Quaternion.Angle(fanLeaves[1].transform.rotation, fanLeaves[2].transform.rotation);
        float delta3 = 360f - delta1 - delta2;
        short acceptableCount = 0;
        if (Mathf.Abs(delta1 - 120f) < allowedBias) acceptableCount++;
        if (Mathf.Abs(delta2 - 120f) < allowedBias) acceptableCount++;
        if (Mathf.Abs(delta3 - 120f) < allowedBias) acceptableCount++;
        if (acceptableCount >= 2)
        {
            windZone.forceMagnitude = windMagnitudeHigh;
            windParticle.emissionRate = windParticleHigh;
            windParticle.startSpeed = windSpeedHigh;
        }
        else
        {
            windZone.forceMagnitude = windMagnitudeMid;
            windParticle.emissionRate = windParticleMid;
            windParticle.startSpeed = windSpeedMid;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            pressWeight--;

            if (pressWeight < 1)
            {
                isPressed = false;
            }
        }
    }
}
