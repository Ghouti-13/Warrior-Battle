using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public string Name;
    public Enemy enemyPrefab;

    [Range(0, 100)]
    public int spawnPercentage;

}
[System.Serializable]
internal class EnemyWave
{
    public List<EnemyInfo> enemies;
    public int spawnAmount;

    public Enemy GetRandomEnemy()
    {
        float totalSpawnPercentage = 0f;

        foreach (var enemy in enemies)
        {
            totalSpawnPercentage += enemy.spawnPercentage;
        }

        System.Random random = new System.Random();
        double randomValue = random.NextDouble() * totalSpawnPercentage;

        float percentageSum = 0f;

        foreach (var enemy in enemies)
        {
            percentageSum += enemy.spawnPercentage;

            if (percentageSum > randomValue) return enemy.enemyPrefab;
        }

        return null;
    }
}
public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance = null;

    [SerializeField] private List<EnemyWave> _enemyWaves = new List<EnemyWave>();
    [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();

    private int _currentWave = 0;
    private int _currentEnemiesAmount = 0;
    private int _currentSpawnIndex = 0;

    public int CurrentWave => _currentWave;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }
    private void Start()
    {
        UIManager.Instance.SetMaxWaves(_enemyWaves.Count);

        StartWave();
    }
    public void StartWave()
    {
        if (_currentWave > _enemyWaves.Count - 1)
        {
            GameManager.Instance.EndGame(true);
            return;
        }

        for (int i = 0; i < _enemyWaves[_currentWave].spawnAmount; i++)
        {
            SpawnEnemy(_enemyWaves[_currentWave].GetRandomEnemy(), _enemySpawnPoints[GetTransformIndex()]);
        }

        _currentEnemiesAmount = _enemyWaves[_currentWave].spawnAmount;
        _currentWave++;

        UIManager.Instance.SetMaxEnemies(_enemyWaves[_currentWave - 1].spawnAmount);
        UIManager.Instance.SetWaveValue(_currentWave);
    }
    private void SpawnEnemy(Enemy enemyPrefab, Transform spawnTransfrom)
    {
        Instantiate(enemyPrefab.gameObject, spawnTransfrom.position, Quaternion.identity);
    }
    private int GetTransformIndex()
    {
        _currentSpawnIndex++;

        if (_currentSpawnIndex > _enemySpawnPoints.Count - 1) return _currentSpawnIndex = 0;

        return _currentSpawnIndex;
    }
    private bool AllEnemiesDied()
    {
        return _currentEnemiesAmount == 0;
    }
    private void Enemy_OnEnemyDeath()
    {
        _currentEnemiesAmount -= 1;
        UIManager.Instance.SetKillsText(_enemyWaves[_currentWave - 1].spawnAmount - _currentEnemiesAmount);

        if (AllEnemiesDied()) StartWave();
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
    }
}
