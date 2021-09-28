using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerController : MonoBehaviour
{
  public Rigidbody2D m_Rb;

  [SerializeField]
  private float m_PlayerSpeed = 1.0f;

  private SpriteRenderer m_Sprite;
  private ParticleSystem m_Particle;
  private PlayerControls m_PlayerInput;
  private TrailRenderer m_Trail;
  private Vector3 m_TargetPosition;
  private bool m_IsMoving;
  private Vector2 m_PlayerCurrentMovement;
  private int m_InversionFlag;

  private bool m_Pause;
  private bool m_IsViewInverted;
  private bool m_IsScrollInverted;

  #region Unity Functions
  private void Awake()
  {
    m_PlayerInput = new PlayerControls();
    m_Sprite = GetComponent<SpriteRenderer>();
    m_Particle = GetComponent<ParticleSystem>();
    m_Trail = GetComponent<TrailRenderer>();
  }

  #endregion

  #region Public Functions
  public void OnInit()
  {
    m_TargetPosition = transform.position;
  }

  public void OnUpdate()
  {
    if (m_Pause) return;
  }

  public void OnFixedUpdate()
  {
    if (m_Pause) return;
    Move();
  }

  public void Reset()
  {
    m_TargetPosition = Vector3.up * -4;
    transform.position = m_TargetPosition;
    m_Pause = false;
    m_Sprite.enabled = true;

    SetAndCheckInversion();
    EnableTrail();
  }

  public void Pause()
  {
    m_Sprite.enabled = false;
    m_Particle.Play();
    m_Pause = true;
  }

  public void DisableTrail()
  {
    m_Trail.enabled = false;
  }

  public void EnableTrail()
  {
    m_Trail.enabled = true;
  }

  #endregion

  #region Private Functions
  private void Move()
  {
    Vector2 _movementInput = m_InversionFlag * m_PlayerInput.PlayerMain.Move.ReadValue<Vector2>();

    m_Rb.MovePosition(m_Rb.position + _movementInput * m_PlayerSpeed);

    // face the direction of movement
    FaceMovement(_movementInput.y);
  }

  private void OnEnable()
  {
    m_PlayerInput.Enable();
  }

  private void OnDisable()
  {
    m_PlayerInput.Disable();
  }

  private void FaceMovement(float _direction)
  {
    if (_direction != 0)
    {
      gameObject.transform.up = new Vector3(0f, _direction, 0f);
    }
  }

  private void SetAndCheckInversion()
  {
    m_IsViewInverted = GameController.Instance.gameData.ViewInversion;
    m_IsScrollInverted = GameController.Instance.gameData.ScrollInversion;
    if (m_IsViewInverted & !m_IsScrollInverted)
    {
      m_InversionFlag = 1;
    }
    else if (m_IsViewInverted & m_IsScrollInverted)
    {
      m_InversionFlag = -1;
    }
    else if (!m_IsViewInverted & m_IsScrollInverted)
    {
      m_InversionFlag = 1;
    }
    else
    {
      m_InversionFlag = -1;
    }
  }
  #endregion
}
