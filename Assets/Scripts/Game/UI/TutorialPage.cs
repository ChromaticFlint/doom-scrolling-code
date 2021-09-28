using UnityEngine;
using TMPro;
using UnityCore.Menu;

public class TutorialPage : Page
{
  public PageController pages;
  public GameObject startTutorial;
  public GameObject tutorialResponsibility;
  public GameObject tutorialMovement;
  public GameObject tutorialMoveSlow;
  public GameObject tutorialObstacles;
  public GameObject tutorialDoomTokens;
  public GameObject tutorialSettings;
  public GameObject tutorialGotIt;

  public GameObject hudContainer;

  #region Public Functions
  public void SkipTutorial()
  {
    GameController.Instance.gameData.TutorialComplete = true;
    GameController.Instance.TryAgain();
    startTutorial.gameObject.SetActive(false);
    pages.TurnPageOff(type);
    hudContainer.gameObject.SetActive(true);
  }

  public void StartTutorial()
  {
    startTutorial.gameObject.SetActive(true);
  }

  public void ForwardToResponsibilityTutorial()
  {
    startTutorial.gameObject.SetActive(false);
    tutorialResponsibility.gameObject.SetActive(true);
  }

  public void BackToResponsibilityTutorial()
  {
    tutorialMovement.gameObject.SetActive(false);
    tutorialResponsibility.gameObject.SetActive(true);
  }

  public void ForwardToMovementTutorial()
  {
    tutorialResponsibility.gameObject.SetActive(false);
    tutorialMovement.gameObject.SetActive(true);
  }

  public void BackToMovementTutorial()
  {
    tutorialMoveSlow.gameObject.SetActive(false);
    tutorialMovement.gameObject.SetActive(true);
  }

  public void ForwardToMoveSlowTutorial()
  {
    tutorialMovement.gameObject.SetActive(false);
    tutorialMoveSlow.gameObject.SetActive(true);
  }

  public void BackToMoveSlowTutorial()
  {
    tutorialObstacles.gameObject.SetActive(false);
    tutorialMoveSlow.gameObject.SetActive(true);
  }

  public void ForwardToObstaclesTutorial()
  {
    tutorialMoveSlow.gameObject.SetActive(false);
    tutorialObstacles.gameObject.SetActive(true);
  }

  public void BackToObstaclesTutorial()
  {
    tutorialDoomTokens.gameObject.SetActive(false);
    tutorialObstacles.gameObject.SetActive(true);
  }

  public void ForwardDoomTokenTutorial()
  {
    tutorialObstacles.gameObject.SetActive(false);
    tutorialDoomTokens.gameObject.SetActive(true);
  }

  public void BackToDoomTokenTutorial()
  {
    tutorialSettings.gameObject.SetActive(false);
    tutorialDoomTokens.gameObject.SetActive(true);
  }

  public void ForwardToSettingsTutorial()
  {
    tutorialDoomTokens.gameObject.SetActive(false);
    tutorialSettings.gameObject.SetActive(true);
  }

  public void BackToSettingsTutorial()
  {
    tutorialGotIt.gameObject.SetActive(false);
    tutorialSettings.gameObject.SetActive(true);
  }

  public void ForwardToGotIt()
  {
    tutorialSettings.gameObject.SetActive(false);
    tutorialGotIt.gameObject.SetActive(true);
  }

  public void EndTutorial()
  {
    tutorialGotIt.gameObject.SetActive(false);
    GameController.Instance.gameData.TutorialComplete = true;
    GameController.Instance.TryAgain();
    pages.TurnPageOff(type);
    hudContainer.gameObject.SetActive(true);
  }
  #endregion

  #region Override Functions
  protected override void OnPageEnabled()
  {
    base.OnPageEnabled();
    StartTutorial();
    hudContainer.gameObject.SetActive(false);
  }
  #endregion
}

