using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class RaycastMask : MonoBehaviour, ICanvasRaycastFilter
{
    public float innerRadiusRatio = 0.66f;
    public float outerRadiusRatio = 1.0f;
    public float minDegree = 0;
    public float maxDegree = 0;

    RectTransform prt;
    Vector2 centerPos;
    float innerRadius, outerRadius;

    void Awake()
    {
        prt = transform.parent.GetComponent<RectTransform>();
        float radius = (prt.anchorMax.x - prt.anchorMin.x) * Screen.width / 2.0f;
        outerRadius = outerRadiusRatio * radius;
        innerRadius = innerRadiusRatio * radius;
    }

    void Start()
    {
        centerPos = Camera.main.WorldToScreenPoint(transform.position);
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        float distance = Vector2.Distance(centerPos, sp);
        if  (distance > outerRadius || distance < innerRadius)
            return false;

        float degree = Vector2.Angle(Vector2.right, sp - centerPos);
        if (degree > maxDegree || degree < minDegree)
            return false;
        
        return true;
    }
}