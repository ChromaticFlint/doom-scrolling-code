using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
  public static ObstacleController Instance;

  public Transform gameMap;
  public GameObject[] obstacles;
  public GameObject basicBossObstacle;
  public GameObject specialBossObstacle;
  public GameObject victoryPickup;
  public int doomTier = 0;
  public int doomTierTrigger = 5;

  private int m_Index = 0;
  private float m_CurrentInterval = 6;
  private float m_PreviousInterval = 0;
  private float m_ProgressPoint = -6;
  private float m_SpawnPoint;
  private int m_ObstacleStartingIndex = -1;
  private int m_ObstacleIndex = -1;
  private bool m_FirstSpawned = false;
  private bool m_SecondSpawned = false;
  private bool m_BossSpawned = false;

  public float obstacleProgress => m_ProgressPoint;
  public float spawnPoint => m_SpawnPoint;

  private List<GameObject> m_Obstacles;

  #region Unity Functions
  private void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }

    m_Obstacles = new List<GameObject>();
    m_PreviousInterval = m_CurrentInterval;
  }
  #endregion

  #region Public Functions
  public void AddObstacle(bool _endless = false, int _progress = 0)
  {
    // Uses ternary to use AddObstacle for standard and endless modes
    GameObject _prefab = _endless & m_FirstSpawned & m_SecondSpawned ? GetRandomObstacle(obstacles) : GetNextObstacle(obstacles);

    if (!_prefab)
    {
      LogWarning("No prefab was found in the obstacle controller.");
      return;
    }

    CheckFirstSpawned();
    CheckSecondSpawned();
    m_FirstSpawned = true;

    SetIntervalByObstacleLength(_prefab);

    // Set spawn point to progress point for GameController Check Progress
    m_SpawnPoint = m_ProgressPoint;
    m_ProgressPoint += m_CurrentInterval;

    GameObject _new = Instantiate(_prefab);

    _new.transform.parent = gameMap;

    float _y = (m_ProgressPoint) * (_progress + 1);

    _new.transform.position = Vector3.up * (_y);
    m_Obstacles.Insert(0, _new);

    CleanupOldObstacles();
    CheckForBossSpawn(_y);
  }

  public void Reset()
  {
    for (int i = m_Obstacles.Count - 1; i >= 0; i--)
    {
      Destroy(m_Obstacles[i]);
      m_Obstacles.RemoveAt(i);
    }
    m_ObstacleIndex = m_ObstacleStartingIndex;
    m_ProgressPoint = -6;
    m_FirstSpawned = false;
    m_SecondSpawned = false;
    m_PreviousInterval = 0;
    m_BossSpawned = false;
    doomTier = 0;
    m_Index = 0;
  }

  public void CheckAndSetDoom()
  {
    m_Index++;
    if (m_Index >= doomTierTrigger)
    {
      m_Index = 0;
      doomTier++;
    }
  }

  public string currentObstacleText()
  {
    return GetCurrentObstacle().textArea;
  }

  public float getObstacleAverageLength()
  {
    return (m_CurrentInterval + m_PreviousInterval) / 2;
  }

  #endregion

  #region Private Functions
  private GameObject GetNextObstacle(GameObject[] _arr)
  {
    if (_arr.Length == 0)
    {
      Debug.LogWarning("Trying to get an obstacle, but no obstacles were found.");
      return null;
    }

    if (m_ObstacleIndex < obstacles.Length - 1)
    {
      m_ObstacleIndex++;
    }
    else
    {
      // The end game content should likely not be in here...
      if (GameController.Instance.bonusComplete)
      {
        Debug.Log("Special boss spawned");
        m_BossSpawned = true;
        return specialBossObstacle;
      }
      else
      {
        Debug.Log("Non Special Boss Spawned");
        m_BossSpawned = true;
        return basicBossObstacle;
      }
    }

    int _next = m_ObstacleIndex;
    return _arr[_next];
  }

  private GameObject GetRandomObstacle(GameObject[] _arr)
  {
    if (_arr.Length == 0)
    {
      Debug.LogWarning("Trying to get a random obstacle, but no obstacles were found.");
      return null;
    }

    int _random = Random.Range(2, _arr.Length);
    return _arr[_random];
  }

  private void SetIntervalByObstacleLength(GameObject _prefab)
  {
    ObstacleObject _obstacle = GetObstacleObject(_prefab);
    m_CurrentInterval = _obstacle.length;
  }

  private ObstacleObject GetObstacleObject(GameObject _prefab)
  {
    return _prefab.GetComponent<ObstacleObject>();
  }

  private void CheckFirstSpawned()
  {
    if (m_FirstSpawned)
    {
      m_PreviousInterval = m_CurrentInterval;
    }
  }

  private void CheckSecondSpawned()
  {
    if (!m_SecondSpawned & m_FirstSpawned)
    {
      m_SecondSpawned = true;
    }
  }

  private void CleanupOldObstacles()
  {
    if (m_Obstacles.Count > 5)
    {
      Destroy(m_Obstacles[m_Obstacles.Count - 1]);
      m_Obstacles.RemoveAt(m_Obstacles.Count - 1);
    }
  }

  private void CheckForBossSpawn(float _y)
  {
    if (m_BossSpawned)
    {
      GameObject _victoryPrefab = Instantiate(victoryPickup);
      _victoryPrefab.transform.parent = gameMap;
      _victoryPrefab.transform.position = Vector3.up * (_y + 10);
      m_Obstacles.Insert(0, _victoryPrefab);
      m_ProgressPoint = 10000;
    }
  }

  private ObstacleObject GetCurrentObstacle()
  {
    return GetObstacleObject(obstacles[m_ObstacleIndex]);
  }

  private void Log(string _msg)
  {
    Debug.Log("[Obstacle Controller]: " + _msg);
  }

  private void LogWarning(string _msg)
  {
    Debug.LogWarning("[Obstacle Controller]: " + _msg);
  }

  #endregion
}
