using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerPath))]
public class PlayerPathEditor : Editor
{
    private void OnSceneGUI()
    {
        var path = target as PlayerPath;

        path.pos1 = Handles.PositionHandle(path.pos1, path.transform.rotation);
        path.pos2 = Handles.PositionHandle(path.pos2, path.transform.rotation);

        Vector3 toPos2 = path.pos2 - path.pos1;
        path.playerGO.transform.position = path.pos1 + (toPos2 * 0.5f);
        
        float angle = Mathf.Atan2(toPos2.y, toPos2.x) * Mathf.Rad2Deg;
        path.playerGO.transform.rotation = Quaternion.Euler(0, 0, angle);

        Handles.color = Color.magenta;
        Handles.DrawLine(path.pos1, path.pos2);
        Handles.color = Color.red;
        Handles.CircleHandleCap(0, path.pos1, Quaternion.identity, 0.2f, EventType.Repaint);
        Handles.color = Color.blue;
        Handles.CircleHandleCap(0, path.pos2, Quaternion.identity, 0.2f, EventType.Repaint);
    }
}
