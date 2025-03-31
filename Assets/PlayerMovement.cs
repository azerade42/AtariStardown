using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerPath path;

    private Camera mainCam;

    [SerializeField] private bool usePathY;

    private void Start()
    {
        mainCam = Camera.main;
    }
    private void FixedUpdate()
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        float xScale = transform.localScale.x;
        float totalPathDist, offset;

        if (usePathY)
        {
            totalPathDist = path.pos2.y - path.pos1.y;
            offset = mousePos.y - path.pos1.y;
        }
        else
        {
            totalPathDist = path.pos2.x - path.pos1.x;
            offset = mousePos.x - path.pos1.x;
        }
        
        float clampedXOffset = Mathf.Clamp(offset, xScale * 0.5f, totalPathDist - xScale * 0.5f);
        float t = clampedXOffset / totalPathDist;

        Vector2 lerpedPos = Vector2.Lerp(path.pos1, path.pos2, t);
        transform.position = lerpedPos;
    }
}
