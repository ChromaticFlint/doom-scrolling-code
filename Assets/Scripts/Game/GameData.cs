using UnityEngine;
using UnityCore.Data;

/// <summary>
/// The Game Data for playing and saving the game. Map any and all data that you want to store and manage in your game within this class. Ensure to appropriately map the properties and fields to the appropriate UnityCore.Data.SaveData properties and fields.
/// </summary>

[RequireComponent(typeof(DataController))]
public class GameData : MonoBehaviour
{
  public static GameData Instance;

  private DataController m_DataController;
  private DataController dataController
  {
    get
    {
      if (!m_DataController)
      {
        m_DataController = DataController.Instance;
      }
      if (!m_DataController)
      {
        Debug.Log("The game data is attempting to access the Data Controller instance, but no instance was found.");
      }
      return m_DataController;
    }
  }

  private bool m_ViewInversion;
  private bool m_ScrollInversion;
  private bool m_BackgroundDisabled;
  private int m_Highscore;
  private int m_EndlessHighScore;
  private float m_MasterVolume;
  private float m_BackgroundVolume;
  private float m_SFXVolume;
  private bool m_TutorialComplete;

  #region Properties
  public bool ViewInversion
  {
    get => m_ViewInversion;
    set => m_ViewInversion = value;
  }

  public bool ScrollInversion
  {
    get => m_ScrollInversion;
    set => m_ScrollInversion = value;
  }

  public bool BackgroundDisabled
  {
    get => m_BackgroundDisabled;
    set => m_BackgroundDisabled = value;
  }

  public int Highscore
  {
    get => m_Highscore;
    set => m_Highscore = value;
  }

  public int EndlessHighScore
  {
    get => m_EndlessHighScore;
    set => m_EndlessHighScore = value;
  }

  public float MasterVolume
  {
    get => m_MasterVolume;
    set => m_MasterVolume = value;
  }

  public float BackgroundVolume
  {
    get => m_BackgroundVolume;
    set => m_BackgroundVolume = value;
  }

  public float SFXVolume
  {
    get => m_SFXVolume;
    set => m_SFXVolume = value;
  }

  public bool TutorialComplete
  {
    get => m_TutorialComplete;
    set => m_TutorialComplete = value;
  }
  #endregion

  #region Unity Functions
  private void Awake()
  {
    Configure();
  }
  #endregion

  #region Public Functions
  /// <summary>
  /// Logs the current data of the Game Data object, this needs to be adjusted for the data required by the game.
  /// </summary>
  public void LogCurrentData()
  {
    Debug.Log($"The current View  Inversion: {m_ViewInversion}, The current Scroll Inversion: {m_ScrollInversion}, Background Disabled is: {m_BackgroundDisabled},Highscore is: {m_Highscore}, Endless Highscore is: {m_EndlessHighScore}, Volumes: Master - [{m_MasterVolume}], Background - [{m_BackgroundVolume}], SFX - {m_SFXVolume}, Tutorial Complete - {m_TutorialComplete}");
  }

  public void SaveGameData(SaveDataSlot _slot = SaveDataSlot.One)
  {
    dataController.SaveData(_slot, this);
  }

  /// <summary>
  /// Loads the data from a SaveData file. This requires being mapped approrpiately betwee nthe SaveData type, and the GameData object. WARNING: This doesn't check if the data and save data is compatible, this could result in lost data if there types change and a editted incorrectly.
  /// </summary>
  public bool LoadGameData(SaveDataSlot _slot = SaveDataSlot.One)
  {
    SaveData _saveData = dataController.LoadData(_slot);

    if (_saveData == null)
    {
      Debug.LogWarning("No data was retrieved from the Data Controller, enable the Data Controller debug flag for more details.");
      return false;
    }
    else
    {

      // Map the GameData to SaveData here.
      this.ViewInversion = _saveData.ViewInversion;
      this.ScrollInversion = _saveData.ScrollInversion;
      this.BackgroundDisabled = _saveData.BackgroundDisabled;
      this.EndlessHighScore = _saveData.EndlessHighscore;
      this.Highscore = _saveData.Highscore;
      this.MasterVolume = _saveData.MasterVolume;
      this.BackgroundVolume = _saveData.BackgroundVolume;
      this.SFXVolume = _saveData.SFXVolume;
      this.TutorialComplete = _saveData.TutorialComplete;
      return true;
    }
  }

  public void DeleteGameData(SaveDataSlot _slot)
  {
    dataController.DeleteSaveData(_slot);
  }

  #endregion

  #region Private Functions
  private void Configure()
  {
    if (!Instance)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
  #endregion
}
