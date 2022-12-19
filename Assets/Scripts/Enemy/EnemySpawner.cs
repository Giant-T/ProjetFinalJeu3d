using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform player;
    [SerializeField] Vector3 size;
    [SerializeField] float secondsBetweenEnemies = 5;
    private bool enemyCanSpawn = true;

    private void Update()
    {
        if (enemyCanSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        enemyCanSpawn = false;
        Vector3 enemyPosition = transform.position;
        enemyPosition.x += Random.Range(-size.x, size.x) / 2f;
        enemyPosition.z += Random.Range(-size.z, size.z) / 2f;
        GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity, transform);
        enemy.GetComponent<Enemy>().SetPlayer(player);

        yield return new WaitForSeconds(secondsBetweenEnemies);
        enemyCanSpawn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
