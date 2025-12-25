using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health;
    public Material opaque;
    public Material wireframe;
    public GameObject particle_system;
    public GameObject projectile;
    private MeshRenderer renderer;
    private Transform projectile_rotator;
    private float delay;
    public AudioClip hit_sfx;
    public AudioClip death_sfx;
    IEnumerator HitAnimation()
    {
        renderer.material = opaque;
        yield return new WaitForSeconds(0.1f);
        renderer.material = wireframe;
    }

    void OnDeath()
    {
        SoundSystem.singleton.PlaySound(death_sfx);
        Instantiate(particle_system, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        Player.singleton.score += 10;
        UI.singleton.score += 10;
        Destroy(gameObject);
    }

    IEnumerator Fire(float delay)
    {
        yield return new WaitForSeconds(delay);
        while(true)
        {
            Instantiate(projectile, transform.position, transform.rotation, projectile_rotator);
            yield return new WaitForSeconds(5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            SoundSystem.singleton.PlaySound(hit_sfx);
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            health -= projectile.damage;
            
            StartCoroutine(HitAnimation());

            Destroy(other.gameObject);

            if (health <= 0)
                OnDeath();
        }
            
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        delay = Random.Range(0, 5);
        renderer = GetComponent<MeshRenderer>();
        projectile_rotator = GameObject.FindWithTag("EnemyProjectileRotator").transform;
        StartCoroutine(Fire(delay));
    }
}
