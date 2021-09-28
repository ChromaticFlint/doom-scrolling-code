using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnityCore
{
  namespace Data
  {
    public class DataController : MonoBehaviour
    {

      public static DataController Instance;
      public GameController gameController;
      public bool debug;

      // Game save slots
      private static readonly string m_SaveSlotOne = "one";
      private static readonly string m_SaveSlotTwo = "two";
      private static readonly string m_SaveSlotThree = "three";

      #region Unity Functions
      private void Awake()
      {
        Configure();
      }

      #endregion

      #region Public Functions
      public void SaveData(SaveDataSlot _slot, GameData _gameData)
      {
        if (_slot != SaveDataSlot.None)
        {
          string _saveSlotPath = SaveSlotToString(_slot);
          string _PATH = Application.persistentDataPath + $"/potato-{_saveSlotPath}.boiled";
          BinaryFormatter _formatter = new BinaryFormatter();
          FileStream _stream = new FileStream(_PATH, FileMode.Create);

          SaveData _data = new SaveData(_gameData);

          _formatter.Serialize(_stream, _data);

          _stream.Close();

          _data.logSaveData("saved");
        }
        else
        {
          LogWarning($"The attempt to save to [" + _slot + "] was unsuccessful as the save slot provided was None.");
        }
      }

      public SaveData LoadData(SaveDataSlot _slot)
      {
        string _saveSlotPath = SaveSlotToString(_slot);
        string _PATH = Application.persistentDataPath + $"/potato-{_saveSlotPath}.boiled";

        if (File.Exists(_PATH))
        {
          BinaryFormatter _formatter = new BinaryFormatter();
          FileStream _stream = new FileStream(_PATH, FileMode.Open);

          SaveData _data = _formatter.Deserialize(_stream) as SaveData;
          _stream.Close();

          _data.logSaveData("loaded");
          return _data;
        }
        else
        {
          Log("The file that you are trying to load does not exist at the following path [" + _PATH + "]");
          return null;
        }
      }

      public void DeleteSaveData(SaveDataSlot _slot)
      {
        // this likely would be better to reset to base data.
        string _saveSlotPath = SaveSlotToString(_slot);
        string _PATH = Application.persistentDataPath + $"/potato-{_saveSlotPath}.boiled";

        if (File.Exists(_PATH))
        {
          File.Delete(_PATH);
          Log("Slot: [" + _slot + "] has been deleted.");
        }
        else
        {
          Log("The file in slot [" + _slot + "] was not able to be deleted, it either didn't exist or was of type None.");
        }
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

      private string SaveSlotToString(SaveDataSlot _slot)
      {
        string _slotStr;

        switch ((int)_slot)
        {
          case 1:
            _slotStr = m_SaveSlotOne;
            break;
          case 2:
            _slotStr = m_SaveSlotTwo;
            break;
          case 3:
            _slotStr = m_SaveSlotThree;
            break;
          default:
            _slotStr = "";
            LogWarning("You are trying to convert the slot: [" + _slot + "] to a string.");
            break;
        }
        return _slotStr;
      }

      private void Log(string _msg)
      {
        if (debug)
        {
          Debug.Log("[Data Controller]: " + _msg);
        }
      }

      private void LogWarning(string _msg)
      {
        if (debug)
        {
          Debug.LogWarning("[Data Controller]: " + _msg);
        }
      }

      #endregion
    }
  }
}
