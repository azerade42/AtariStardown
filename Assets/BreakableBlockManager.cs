using Unity.VisualScripting;
using UnityEngine;

public class BreakableBlockManager : MonoBehaviour
{
    int numBreakablesRemaining;
    public EndGameCanvas endGameCanvas;
    private void Start()
    {
        Breakable[] breakables = transform.GetComponentsInChildren<Breakable>();
        foreach (Breakable breakable in breakables)
        {
            breakable.OnDestroy += TrackBreakable;
        }
    }

    private void TrackBreakable()
    {
        if (--numBreakablesRemaining <= 0)
        {
            endGameCanvas.EndGame(true);
        }
    }
}
