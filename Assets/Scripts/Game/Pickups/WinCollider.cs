using UnityEngine;

public class WinCollider : MonoBehaviour
{
  private PickupObject m_Root;

  private void Awake()
  {
    m_Root = transform.root.GetComponent<PickupObject>();
  }

  private void OnTriggerEnter2D(Collider2D _col)
  {
    if (_col.gameObject.tag.Equals("Player"))
    {
      GameController.Instance.PlayerHitVictoryPickup();
      m_Root.gameObject.SetActive(false);
    }
  }
}
