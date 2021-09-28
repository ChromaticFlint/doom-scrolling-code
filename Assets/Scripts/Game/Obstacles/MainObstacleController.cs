using System.Collections;
using UnityEngine;

public class MainObstacleController : MonoBehaviour
{
  public GameObject nameText;

  [SerializeField]
  private float m_InitialMoveSpeed = 1f;

  [SerializeField]
  private float m_CurrentMoveSpeed;
  private Vector3 m_InitialPosition;
  private Vector3 m_CurrentPostion;
  private Vector3 m_TargetPosition;


  #region Public Functions
  public void OnInit()
  {
    m_CurrentPostion = transform.position;
    m_InitialPosition = m_CurrentPostion;
    m_CurrentMoveSpeed = m_InitialMoveSpeed;


    StartCoroutine(IncrementMoveSpeed(0.2f));
  }

  public void OnUpdate()
  {
    MoveObstacle();
  }

  public void Reset()
  {
    StopCoroutine("IncrementMoveSpeed");
    m_CurrentMoveSpeed = m_InitialMoveSpeed;
    m_CurrentPostion = m_InitialPosition;
    transform.position = m_CurrentPostion;

    TextMesh _text = nameText.GetComponentInChildren<TextMesh>();

    if (GameController.Instance.gameData.ViewInversion)
    {
      _text.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    else
    {
      _text.transform.localEulerAngles = new Vector3(0, 0, 180);
    }

  }

  public void StopMoving()
  {
    // Maybe use this to stop the giant obstacle from moving on player death?
    StopCoroutine("IncrementMoveSpeed");
  }

  public void ReduceMoveSpeedTo(float _value)
  {
    m_CurrentMoveSpeed = _value;
  }

  public void ReduceMoveSpeedBy(float _value)
  {
    m_CurrentMoveSpeed -= _value;
    Debug.Log("Current Movement Speed " + m_CurrentMoveSpeed);
  }

  #endregion

  #region Private Functions
  private void MoveObstacle()
  {
    m_TargetPosition.y = m_CurrentPostion.y += m_CurrentMoveSpeed;

    transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -1), new Vector3(m_TargetPosition.x, m_TargetPosition.y, -1), Time.deltaTime);

    m_CurrentPostion = transform.position;
  }

  // Implement this when you can think again
  private IEnumerator IncrementMoveSpeed(float _moveSpeed)
  {
    yield return new WaitForSeconds(5);
    m_CurrentMoveSpeed += _moveSpeed;
    StartCoroutine(IncrementMoveSpeed(0.2f));
    yield return null;
  }

  #endregion
}
