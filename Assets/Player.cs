using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float lr_vel;
    public InputActionReference move;
    public InputActionReference fire;
    public float rotation_factor;
    public float rotation_speed;
    private Rigidbody rb;
    private Vector2 movement_vec;
    private Vector3 standard_rotation_euler;
    private Quaternion standard_rotation_quaternion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        standard_rotation_euler = transform.rotation.eulerAngles;
        standard_rotation_quaternion = Quaternion.Euler(standard_rotation_euler);
    }

    // Update is called once per frame
    void Update()
    {
        movement_vec = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement_vec * lr_vel, ForceMode.Force);
        Vector3 target_rotation_euler = new Vector3(standard_rotation_euler.x, standard_rotation_euler.y, standard_rotation_euler.z + movement_vec.x * rotation_factor);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target_rotation_euler), rotation_speed);
    }
}
