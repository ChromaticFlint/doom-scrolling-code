using System.Collections;
using UnityEngine;
using TMPro;
using UnityCore.Menu;

public class PauseMenuPage : Page
{
  public PageController pages;
  public GameObject confirmationModal;

  #region Public Functions
  public void Resume()
  {
    pages.TurnPageOn(PageType.Countdown);
    pages.TurnPageOff(type);
  }

  public void OnQuit()
  {
    confirmationModal.SetActive(true);
  }

  public void OnCancelQuit()
  {
    confirmationModal.SetActive(false);
  }

  public void GoToMainMenu()
  {
    GameController.Instance.CheckAndSetHighscore();
    pages.TurnPageOff(type);
    pages.TurnPageOn(PageType.Menu);
    confirmationModal.SetActive(false);
  }

  #endregion


  #region Override Functions
  protected override void OnPageEnabled()
  {
    base.OnPageEnabled();
  }

  #endregion
}
