using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject[] _PowerUp;

    //we don't need to use container for powerup bcz it's immediately being destroyed
    // unlike enemies who are respawning(re-use) again

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_stopSpawning == false) 
        {
            yield return new WaitForSeconds(2f);
            Vector3 postospawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postospawn, Quaternion.identity);

            newEnemy.transform.parent = _EnemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator PowerUpSpawnRoutine()
    {

        yield return new WaitForSeconds(2f);
        while (_stopSpawning == false)
        {
            Vector3 postospawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int rand_powerup = Random.Range(0, 3);

            Instantiate(_PowerUp[rand_powerup], postospawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
