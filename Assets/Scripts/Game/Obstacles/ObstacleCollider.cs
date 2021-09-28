using TMPro;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
  private ObstacleObject m_Root;

  #region Unity Functions
  private void Awake()
  {
    m_Root = transform.root.GetComponent<ObstacleObject>();
  }

  private void OnTriggerEnter2D(Collider2D _col)
  {
    if (_col.gameObject.tag.Equals("Player"))
    {
      GameController.Instance.OnPlayerHitObstacle();
      GameController.Instance.deathText = m_Root.textArea;
    }
  }
  #endregion
}
