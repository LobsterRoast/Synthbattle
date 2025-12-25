using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float interval;
    private Bounds bounds;
    private Collider collider;
    private Transform enemy_rotator;

    IEnumerator SpawnEnemy()
    {
        while (true) {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
            Instantiate(enemy, pos, enemy.transform.rotation, enemy_rotator);
            yield return new WaitForSeconds(interval);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_rotator = GameObject.FindWithTag("EnemyRotator").transform;
        collider = GetComponent<Collider>();
        bounds = collider.bounds;
        StartCoroutine(SpawnEnemy());
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
