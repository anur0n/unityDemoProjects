using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameObject _enemyContainer;

    private IEnumerator coroutine;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-11.1f, 11.2f), 8f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning == false)
        {
            GameObject newPowerup = Instantiate(_powerups[Random.Range(0,3)], new Vector3(Random.Range(-11.1f, 11.2f), 8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }

    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
