using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static bool instanceCreated;
    private void Start()
    {
        if (!instanceCreated)
        {
            instanceCreated = true;
            DontDestroyOnLoad(this);  
        }
        else
        {
            Destroy(this);
        }
    }
}
