using UnityEngine;

public class PickupObject : MonoBehaviour
{
  public int pickupIndex;
  public GameObject letterText;

  private void Awake()
  {
    if (GameController.Instance.gameData.ViewInversion)
    {
      letterText.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    else
    {
      letterText.transform.localEulerAngles = new Vector3(0, 0, 180);
    }
  }
}
