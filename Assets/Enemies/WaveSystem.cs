using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;
    public int enemiesPerWave;

    private int currentWave = 0;
    private float waveTimer = 0.0f;
    private int additionalEnemies = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    //pickups
    [SerializeField] public GameObject[] pickupPrefabs;
    public Transform[] pickupSpawnPoints;

    //ui
    public UIController uiController;

    void Update()
    {
        if (activeEnemies.Count == 0) //check if all enemies from the previous wave are killed
        {
            waveTimer += Time.deltaTime;

            if (waveTimer >= timeBetweenWaves)
            {
                StartNextWave();
                waveTimer = 0f;
            }
        }

        uiController.DisplayText("EnemiesLeftText", "Enemies: " + activeEnemies.Count);
    }

    public void StartNextWave()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave + " begins!");
        uiController.DisplayText("WaveNumberText", "Wave " + currentWave + " begins!");

        UpdateAdditionalEnemies();
        SpawnEnemies();

        if (Random.Range(0.0f, 1.0f) <= 0.25f)
        {
            SpawnPickups();
        }
    }

    public void SpawnEnemies()
    {
        int totalEnemies = enemiesPerWave + additionalEnemies;

        for (int i = 0; i < totalEnemies; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            activeEnemies.Add(enemy);
        }

        Debug.Log(totalEnemies);
    }

    private void UpdateAdditionalEnemies()
    {
        if (currentWave % 2 == 0)
        {
            additionalEnemies += 2; 
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy); //eemove defeated enemy from the list of active enemies
    }

    public void SpawnPickups()
    {
        GameObject pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
        Transform spawnPoint = pickupSpawnPoints[Random.Range(0, pickupSpawnPoints.Length)];

        Instantiate(pickupPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
