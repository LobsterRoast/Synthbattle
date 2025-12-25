using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player singleton;
    public float lr_vel;
    public InputActionReference move;
    public InputActionReference fire;
    public float rotation_factor;
    public float rotation_speed;
    public GameObject projectile;
    public float score = 0;
    public GameObject particle_system;
    public Material opaque;
    public Material wireframe;
    public GameObject wait_and_reload;
    public AudioClip hit_sfx;
    public AudioClip death_sfx;
    public AudioClip shoot_sfx;
    private float _health = 100;
    private Rigidbody rb;
    private Vector2 movement_vec;
    private Vector3 standard_rotation_euler;
    private Transform projectile_rotator;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            UI.singleton.health = _health;
            if (_health <= 0) {
                OnDeath();
            }
        }
    }

    IEnumerator HitAnimation()
    {
        GetComponent<Renderer>().material = opaque;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material = wireframe;
    }

    void OnDeath()
    {
        Instantiate(particle_system, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        Instantiate(wait_and_reload);
        SoundSystem.singleton.PlaySound(death_sfx);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        health -= projectile.damage;
        SoundSystem.singleton.PlaySound(hit_sfx);
        StartCoroutine(HitAnimation());
        Destroy(other.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        singleton = this;
        rb = GetComponent<Rigidbody>();
        standard_rotation_euler = transform.rotation.eulerAngles;
        fire.action.started += Fire;
        projectile_rotator = GameObject.FindWithTag("ProjectileRotator").transform;
        UI.singleton.LazyInit();
    }

    // Update is called once per frame
    void Update()
    {
        movement_vec = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement_vec * lr_vel, ForceMode.Force);
        Vector3 target_rotation_euler = new Vector3(
            standard_rotation_euler.x, 
            standard_rotation_euler.y, 
            standard_rotation_euler.z + movement_vec.x * rotation_factor
        );
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target_rotation_euler), rotation_speed);
    }

    private void Fire(InputAction.CallbackContext _)
    {
        SoundSystem.singleton.PlaySound(shoot_sfx);
        Instantiate(projectile, transform.position, transform.rotation, projectile_rotator);
    }
}
