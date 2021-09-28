using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
  public Transform player;
  public float smooth;
  public GameObject[] backgrounds;

  private Camera m_Camera;
  private Vector3 m_TargetPosition;
  private Vector3 m_InitialPosition;

  private bool m_IsInverted;

  #region Public Functions
  public void OnInit()
  {
    m_Camera = GetComponent<Camera>();
    m_TargetPosition = transform.position;
    m_InitialPosition = m_TargetPosition;
  }

  public void OnUpdate()
  {
  }

  public void OnFixedUpdate()
  {
    FollowPlayer();
  }

  public void Reset()
  {
    m_TargetPosition = m_InitialPosition;
    transform.position = m_TargetPosition;
    GetAndCheckInversion();
  }

  public void HandleBackground(bool _disabled, int _backgroundIndex)
  {
    foreach (GameObject background in backgrounds)
    {
      background.gameObject.SetActive(false);
    }

    if (_disabled)
    {
      GameObject _background = backgrounds[2];
      _background.gameObject.SetActive(true);
    }
    else
    {
      GameObject _background = backgrounds[_backgroundIndex];
      _background.gameObject.SetActive(true);
    }
  }

  public void HandleSize(float _ratio)
  {
    if (_ratio > 1.8)
    {
      m_Camera.orthographicSize = 6;
    }
    else if (_ratio <= 1.5)
    {
      m_Camera.orthographicSize = 4;
    }
    else
    {
      m_Camera.orthographicSize = 5;
    }
  }



  #endregion

  #region Private Functions
  private void FollowPlayer()
  {
    if (!player)
    {
      Debug.LogWarning("Camera could not find a reference to the player");
      return;
    }
    m_TargetPosition.y = player.position.y;

    transform.position = Vector3.Lerp(transform.position, m_TargetPosition, smooth * Time.deltaTime);
  }

  private void GetAndCheckInversion()
  {
    m_IsInverted = GameController.Instance.gameData.ViewInversion;
    if (m_IsInverted)
    {
      m_Camera.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else
    {
      m_Camera.transform.eulerAngles = new Vector3(0, 0, 180);
    }
  }
  #endregion

}
