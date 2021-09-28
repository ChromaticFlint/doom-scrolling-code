using TMPro;
using UnityEngine;

[SelectionBase]
public class ObstacleObject : MonoBehaviour
{
  public GameObject theObstacle;
  public float length = 6f;

  [Tooltip("Set anchor to lower left.")]
  public GameObject backgroundTextLeft;
  [Tooltip("Set anchor to lower left.")]
  public GameObject backgroundTextRight;
  public GameObject background;
  public SpriteRenderer backgroundRenderer;
  public Color backgroundColor;
  public bool textColorInversion = false;

  [TextArea]
  public string textArea;

  private TextMesh leftBackgroundText;
  private TextMesh rightBackgroundText;

  #region Unity Functions
  private void Awake()
  {
    backgroundRenderer = backgroundRenderer.GetComponent<SpriteRenderer>();
    SetBackground();
  }

  #endregion

  #region Private Functions
  private void SetBackground()
  {
    TextMesh _left = backgroundTextLeft.GetComponentInChildren<TextMesh>();
    TextMesh _right = backgroundTextRight.GetComponentInChildren<TextMesh>();

    SetObstacleSize();

    SetObstacleLocation();

    SetObstacleColor();

    SetObstacleTextLocation();

    SetTextFromTextArea(_left, _right);

    SetColorInversion(_left, _right);

  }

  private void SetObstacleSize()
  {
    background.transform.localScale = new Vector3(6, length, 1);
  }

  private void SetObstacleLocation()
  {
    background.transform.localPosition = Vector3.down * length / 2;
  }

  private void SetObstacleColor()
  {
    if (backgroundColor != null)
    {
      backgroundRenderer.color = backgroundColor;
    }
  }

  private void SetObstacleTextLocation()
  {
    backgroundTextLeft.transform.localPosition = new Vector3(-2.5f, -length + .25f, 0);
    backgroundTextLeft.transform.localEulerAngles = new Vector3(0, 0, 90);
    backgroundTextRight.transform.localPosition = new Vector3(2.5f, -.25f, 0);
    backgroundTextRight.transform.localEulerAngles = new Vector3(0, 0, 270);
  }

  private void SetTextFromTextArea(TextMesh _left, TextMesh _right)
  {
    _left.text = textArea;
    _right.text = textArea;
  }

  private void SetColorInversion(TextMesh _left, TextMesh _right)
  {
    if (textColorInversion)
    {
      _left.color = Color.white;
      _right.color = Color.white;
    }
  }
  #endregion
}
