
using UnityEngine;

public class PoweredGear : MonoBehaviour
{
    public GameUI gameUI;
    public float rotationSpeed = 10;
    public int direction = 1;
    public int Xposition;
    
    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed * direction * Time.fixedDeltaTime);
    }
}
