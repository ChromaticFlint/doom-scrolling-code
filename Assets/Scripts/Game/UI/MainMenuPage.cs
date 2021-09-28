using UnityEngine;
using UnityCore.Menu;
using UnityCore.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuPage : Page
{
  public PageController pages;
  public GameObject devTipModal;
  public GameObject creditsModal;
  public GameObject shopModal;
  public GameObject restorePurchasesButton;

  #region Public Fuctions
  public void StartGame()
  {
    if (GameController.Instance.gameData.TutorialComplete)
    {
      GameController.Instance.SetEndless(false);
      GameController.Instance.TryAgain();
      pages.TurnPageOff(type);
    }
    else
    {
      pages.TurnPageOff(type);
      StartTutorial();
    }
  }

  public void StartEndless()
  {
    if (GameController.Instance.gameData.TutorialComplete)
    {
      GameController.Instance.SetEndless(true);
      GameController.Instance.TryAgain();
      pages.TurnPageOff(type);
    }
    else
    {
      GameController.Instance.SetEndless(true);
      pages.TurnPageOff(type);
      StartTutorial();
    }
  }

  public void OpenSettings()
  {
    pages.TurnPageOn(PageType.Settings);
    pages.TurnPageOff(type);
  }

  public void OpenTip()
  {
    devTipModal.gameObject.SetActive(true);
    if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
      restorePurchasesButton.gameObject.SetActive(true);
    }
  }

  public void CloseTip()
  {
    if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
      restorePurchasesButton.gameObject.SetActive(false);
    }
    devTipModal.gameObject.SetActive(false);
  }

  public void OpenShop()
  {
    shopModal.gameObject.SetActive(true);
  }

  public void CloseShop()
  {
    shopModal.gameObject.SetActive(false);
  }

  public void OnTip()
  {
    Debug.Log("Making it rain");
  }

  public void OnTwitterSelected()
  {
    Application.OpenURL("https://twitter.com/StormForged_");
    Debug.Log("Tweet Tweet");
  }

  public void OpenCredits()
  {
    creditsModal.gameObject.SetActive(true);
  }

  public void CloseCredits()
  {
    creditsModal.gameObject.SetActive(false);
  }

  public void OnQuit()
  {
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#endif
    Application.Quit();
  }

  #endregion

  #region Private Functions
  private void StartTutorial()
  {
    Debug.Log("Tutorial Has Started.");
    pages.TurnPageOn(PageType.Tutorial);
  }

  #endregion

  #region Override Functions
  protected override void OnPageEnabled()
  {
    GameController.Instance.SetEndless(false);
    GameController.Instance.DisablePlayerTrail();
    GameController.Instance.TryAgain();
    Time.timeScale = 0;
    GameController.Instance.gameStarted = false;
  }

  #endregion
}
