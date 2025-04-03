using UnityEngine;

public class SpinController : MonoBehaviour
{
    public float SpinSpeed = 0.5f;

    private int spinRotation;
    [SerializeField] bool accelerateOnWin;

    private void Start()
    {
        spinRotation = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void OnEnable()
    {
        if (accelerateOnWin)
            StarControllerManager.OnAllStarsSaved += Accelerate;
    }

    private void OnDisable()
    {
        if (accelerateOnWin)
            StarControllerManager.OnAllStarsSaved -= Accelerate;
    }
    void Update()
    {
        float rotationThisFrame = spinRotation * SpinSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, 0, rotationThisFrame);
    }

    private void Accelerate()
    {
        spinRotation *= 2;
    }
}
