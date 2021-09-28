using UnityEngine;
using UnityCore.Menu;
using UnityEngine.UI;

public class SettingsMenuPage : Page
{
  public PageController pages;
  public GameObject confirmDeleteModal;
  public GameObject confirmLeaveModal;
  public Toggle viewInversionToggle;
  public Toggle scrollInversionToggle;
  public Toggle backgrounddisableToggle;
  public Slider masterSlider;
  public Slider bGMSlider;
  public Slider sFXSlider;

  public bool viewInversionSetting;
  public bool scrollInversionSetting;
  public bool backgroundDisabled;
  public float masterVolumeSetting;
  public float bGMVolumeSetting;
  public float sFXVolumeSetting;

  private bool m_Saved;

  #region Public Fuctions

  // Settings Page
  public void ToggleViewInversion()
  {
    viewInversionSetting = viewInversionToggle.isOn;
  }

  public void ToggleScrollInversion()
  {
    scrollInversionSetting = scrollInversionToggle.isOn;
  }

  public void ToggleBackground()
  {
    backgroundDisabled = backgrounddisableToggle.isOn;
  }

  public void AdjustMasterVolume()
  {
    masterVolumeSetting = masterSlider.value;
  }

  public void AdjustBGMVolume()
  {
    bGMVolumeSetting = bGMSlider.value;
  }

  public void AdjustSFXVolume()
  {
    sFXVolumeSetting = sFXSlider.value;
  }

  public void SaveSettings()
  {
    GameController.Instance.SaveSettings();
  }

  public void OpenDeleteDataConfirmation()
  {
    confirmDeleteModal.SetActive(true);
  }

  public void CheckForSaved()
  {
    LogSettings();

    if (viewInversionSetting == GameController.Instance.gameData.ViewInversion &
        scrollInversionSetting == GameController.Instance.gameData.ScrollInversion &
        backgroundDisabled == GameController.Instance.gameData.BackgroundDisabled &
        masterVolumeSetting == GameController.Instance.gameData.MasterVolume &
        bGMVolumeSetting == GameController.Instance.gameData.BackgroundVolume &
        sFXVolumeSetting == GameController.Instance.gameData.SFXVolume
        )
    {
      NavigateToMainMenu();
    }
    else
    {
      confirmLeaveModal.SetActive(true);
    }
  }

  public void NavigateToMainMenu()
  {
    pages.TurnPageOff(type);
    pages.TurnPageOn(PageType.Menu);
  }

  public void ResetToDefaults()
  {
    viewInversionSetting = false;
    scrollInversionSetting = true;
    backgroundDisabled = false;
    masterVolumeSetting = 0;
    bGMVolumeSetting = -20;
    sFXVolumeSetting = -20;
    GameController.Instance.gameData.TutorialComplete = false;
    GameController.Instance.SaveSettings();
    UpdateSettingsContent();
  }

  // Delete Data Modal
  public void HandleDeleteData()
  {
    GameController.Instance.DeleteSaveData();
    ResetToDefaults();
    UpdateSettingsContent();
    confirmDeleteModal.SetActive(false);
    pages.TurnPageOff(type);
    pages.TurnPageOn(PageType.Menu);
  }

  public void CancleDeleteData()
  {
    confirmDeleteModal.SetActive(false);
  }

  // Discard Changes Modal
  public void DiscardChanges()
  {
    NavigateToMainMenu();
    UpdateSettingsContent();
    confirmLeaveModal.SetActive(false);
  }

  public void CancelDiscardChanges()
  {
    confirmLeaveModal.SetActive(false);
  }

  #endregion

  #region Override Functions
  protected override void OnPageEnabled()
  {
    base.OnPageEnabled();
    UpdateSettingsContent();
  }

  private void UpdateSettingsContent()
  {
    viewInversionToggle.isOn = GameController.Instance.gameData.ViewInversion;
    scrollInversionToggle.isOn = GameController.Instance.gameData.ScrollInversion;
    backgrounddisableToggle.isOn = GameController.Instance.gameData.BackgroundDisabled;
    masterSlider.value = GameController.Instance.gameData.MasterVolume;
    bGMSlider.value = GameController.Instance.gameData.BackgroundVolume;
    sFXSlider.value = GameController.Instance.gameData.SFXVolume;

    viewInversionSetting = viewInversionToggle.isOn;
    scrollInversionSetting = scrollInversionToggle.isOn;
    backgroundDisabled = backgrounddisableToggle.isOn;
    masterVolumeSetting = masterSlider.value;
    bGMVolumeSetting = bGMSlider.value;
    sFXVolumeSetting = sFXSlider.value;
  }

  private void LogSettings()
  {
    Debug.Log("View Inversion: " + viewInversionSetting + " " + GameController.Instance.gameData.ViewInversion);
    Debug.Log("Scroll Inversion: " + scrollInversionSetting + " " + GameController.Instance.gameData.ScrollInversion);
    Debug.Log("Background: " + backgroundDisabled + " " + GameController.Instance.gameData.BackgroundDisabled);
    Debug.Log("Master Vol: " + masterVolumeSetting + " " + GameController.Instance.gameData.MasterVolume);
    Debug.Log("BGM Vol: " + bGMVolumeSetting + " " + GameController.Instance.gameData.BackgroundVolume);
    Debug.Log("SFX Vol: " + sFXVolumeSetting + " " + GameController.Instance.gameData.SFXVolume);
  }
  #endregion
}
