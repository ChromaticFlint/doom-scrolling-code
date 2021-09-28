using UnityEngine;
using TMPro;
using UnityCore.Menu;

public class GameOverPage : Page
{
  public PageController pages;
  public TMP_Text deathText;
  public TMP_Text scoreText;
  public TMP_Text highScoreText;
  public TMP_Text pageNameText;
  public BannerAdScript adScript;

  #region Public Functions
  public void TryAgain()
  {
    GameController.Instance.TryAgain();
    adScript.HideBanner();
    pages.TurnPageOff(type);
  }

  public void GoToMainMenu()
  {
    pages.TurnPageOff(type);
    adScript.HideBanner();
    pages.TurnPageOn(PageType.Menu);
  }

  #endregion
  #region Private Functions
  private void SetDeathText(string _text)
  {
    deathText.text = "You have met your doom by: " + _text;
  }

  private void SetScores()
  {
    scoreText.text = "Score: " + GameController.Instance.score.ToString();
    highScoreText.text = GameController.Instance.CheckAndSetHighscore();
  }
  #endregion

  #region Override Functions
  protected override void OnPageEnabled()
  {
    base.OnPageEnabled();
    adScript.ShowBanner();

    if (GameController.Instance.victory & !GameController.Instance.bonusComplete)
    {
      SetScores();
      pageNameText.text = "Victory!";
      deathText.color = (Color.yellow);
      deathText.text = "You are victorious... basically";
    }
    else if (GameController.Instance.victory & GameController.Instance.bonusComplete)
    {
      SetScores();
      pageNameText.text = "Victory!";
      deathText.color = (Color.yellow);
      deathText.text = "You are gloriously victorious";
    }
    else
    {
      SetScores();
      SetDeathText(GameController.Instance.deathText);
    }
  }

  #endregion

}
