using UnityEngine;

public class GearToPower : MonoBehaviour
{
    public bool ispowered = false;
    public int direction = 1;
    public float rotationSpeed = 10;
    public int Xposition;

    private void FixedUpdate()
    {
        if (ispowered)
            transform.Rotate(0f, 0f, rotationSpeed * direction * Time.fixedDeltaTime);
    }

}
