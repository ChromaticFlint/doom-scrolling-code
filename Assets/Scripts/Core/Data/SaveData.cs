using UnityEngine;

/// <summary>
/// See GameData and UnityCore.Data.DataController summaries for formatting needs.
/// </summary>

namespace UnityCore
{
  namespace Data
  {
    [System.Serializable]
    public class SaveData
    {
      public bool ViewInversion;
      public bool ScrollInversion;
      public bool BackgroundDisabled;
      public int Highscore;
      public int EndlessHighscore;
      public float MasterVolume;
      public float BackgroundVolume;
      public float SFXVolume;
      public bool TutorialComplete;

      public SaveData(GameData _data)
      {
        ViewInversion = _data.ViewInversion;
        ScrollInversion = _data.ScrollInversion;
        BackgroundDisabled = _data.BackgroundDisabled;
        Highscore = _data.Highscore;
        EndlessHighscore = _data.EndlessHighScore;
        MasterVolume = _data.MasterVolume;
        BackgroundVolume = _data.BackgroundVolume;
        SFXVolume = _data.SFXVolume;
        TutorialComplete = _data.TutorialComplete;
      }

      public void logSaveData(string _msg)
      {
        Debug.Log(
          $"The following Data has been {_msg}: [ViewInversion]: {ViewInversion}, [ScrollInversion]: {ScrollInversion}, [Background Disabled]: {BackgroundDisabled}, [Highscore]: {Highscore}, [Endless Highscore]: {EndlessHighscore}, [Volumes]: Master - {MasterVolume}, Background - {BackgroundVolume}, SFX - {SFXVolume}, Tutorial Complete: {TutorialComplete}");
      }
    }
  }
}

