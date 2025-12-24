using UnityEngine;

public class Planet : MonoBehaviour
{
    public float vel = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Only God knows why giving this an x-axis spin in code translates to a z-axis spin in-engine
        rb.angularVelocity = new Vector3(vel, 0f, 0f);
    }

    void OnValidate() {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        } else {
            rb.angularVelocity = new Vector3(vel, 0f, 0f);
        }
    }
}
