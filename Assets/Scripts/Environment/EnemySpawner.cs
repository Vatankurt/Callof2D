using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int MAX_ZOMBIES = 5;

    [SerializeField]
    private bool enable;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Transform target;

    private void Start()
    {
        if (enable)
            InvokeRepeating(nameof(SpawnZombie), Random.Range(0f, spawnRate), spawnRate);
    }

    private void SpawnZombie()
    {
        if (UIManager.IsPaused || CountZombiesInMap() >= MAX_ZOMBIES)
            return;

        int random = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[random];

        Instantiate(enemyToSpawn, transform.position, new Quaternion(0, 0, 90, 0f));
    }

    private int CountZombiesInMap()
    {
        return FindObjectsOfType(typeof(Enemy)).Length;
    }
}