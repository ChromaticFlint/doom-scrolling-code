using TMPro;
using UnityEngine;

public class MainObstacleCollider : MonoBehaviour
{
  [TextArea]
  public string deathText;

  #region Unity Functions

  private void OnTriggerEnter2D(Collider2D _col)
  {
    if (_col.gameObject.tag.Equals("Player"))
    {
      GameController.Instance.OnPlayerHitObstacle();
      GameController.Instance.deathText = deathText;
    }
  }
  #endregion
}
