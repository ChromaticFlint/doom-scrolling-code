using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
  public Transform gameMap;

  public float spawnPositionOne;
  public float spawnPositionTwo;
  public float spawnPositionThree;
  public float spawnPositionFour;

  public GameObject[] pickups;
  private List<float> m_Locations;
  private List<GameObject> m_Prefabs;

  #region Unity Functions
  private void Awake()
  {
    m_Prefabs = new List<GameObject>();
    m_Locations = new List<float>() { spawnPositionOne, spawnPositionTwo, spawnPositionThree, spawnPositionFour };
  }

  #endregion

  #region Public Functions
  public void SpawnPickup(int _index)
  {
    m_Prefabs[_index].transform.position = Vector3.up * m_Locations[_index];
    m_Prefabs[_index].gameObject.SetActive(true);
  }

  public void ResetPickups()
  {
    if (m_Prefabs.Count > 0)
    {
      for (int i = m_Prefabs.Count - 1; i >= 0; i--)
      {
        Destroy(m_Prefabs[i]);
        m_Prefabs.RemoveAt(i);
      }
    }

    PopulatePickupsList();
  }

  #endregion

  #region Private
  private void PopulatePickupsList()
  {
    foreach (GameObject _pickup in pickups)
    {
      GameObject _clone = Instantiate(_pickup);
      m_Prefabs.Add(_clone);
      _clone.transform.parent = gameMap;
      _clone.SetActive(false);
    }
  }

  #endregion
}
