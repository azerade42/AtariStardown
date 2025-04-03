using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerPath path;
    [SerializeField] private bool usePathY;

    private Camera mainCam;

    private bool movementEnabled = true;

    private void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        BallController.OnDeathToggled += ToggleMovement;
    }

    private void OnDisable()
    {
        BallController.OnDeathToggled -= ToggleMovement;
    }

    private void FixedUpdate()
    {
        if (!movementEnabled)
            return;
        
        MovePlayer();
    }

    private void MovePlayer()
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

    private void ToggleMovement(bool enabled) => movementEnabled = enabled;
}
