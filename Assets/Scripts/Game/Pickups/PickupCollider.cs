using UnityEngine;

public class PickupCollider : MonoBehaviour
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
      GameController.Instance.OnPlayerHitPickup(m_Root.pickupIndex);
      m_Root.gameObject.SetActive(false);
    }
  }
}
