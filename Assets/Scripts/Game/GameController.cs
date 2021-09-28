using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityCore.Session;
using UnityCore.Menu;
using UnityCore.Data;
using UnityCore.Audio;

public class GameController : MonoBehaviour
{
  public static GameController Instance;

  public PlayerController player;
  public CameraController theCamera;
  public MainObstacleController mainObstacle;
  public ObstacleController obstacles;
  public PickupController pickups;
  public PageController pages;
  public SettingsMenuPage settings;
  public GameObject bonusTextOne;
  public GameObject bonusTextTwo;
  public GameObject bonusTextThree;
  public GameObject bonusTextFour;
  public bool gameStarted = false;
  public bool isPaused = false;
  public bool playerInvulnerable = false;
  public int score
  {
    get; private set;
  }
  public bool bonusComplete = false;

  public TMP_Text scoreText;
  public MixLevels mixer;
  public string deathText;
  public bool victory = false;

  private bool m_EndlessModeEnabled = false;
  public bool endlessModeEnabled
  {
    get => m_EndlessModeEnabled;
    private set => m_EndlessModeEnabled = value;
  }

  private int currentDoom = 0;
  private SessionController m_Session;
  private int m_Progress = -1;
  private int m_ScoreMultiplier = 1;
  private bool m_GameOver;
  private bool m_PlayerDied;

  // Bonus objectives
  private bool firstBonus = false;
  private bool secondBonus = false;
  private bool thirdBonus = false;
  private bool fourthBonus = false;

  // get game data with integrity
  private static GameData m_GameData;
  public GameData gameData
  {
    get
    {
      if (!m_GameData)
      {
        m_GameData = GameData.Instance;
      }

      if (!m_GameData)
      {
        Debug.LogWarning("Game is trying to access game data, but no instance of the Game Data was found.");
      }
      return m_GameData;
    }
  }

  // get session with integrity
  private SessionController session
  {
    get
    {
      if (!m_Session)
      {
        m_Session = SessionController.Instance;
      }

      if (!m_Session)
      {
        Debug.LogWarning("Game is trying to access the session, but no instance of the Session Controller was found.");
      }
      return m_Session;
    }
  }

  #region Unity Functions
  private void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }
  }

  private void Start()
  {
    if (!session) return;
    session.InitializeGame(this);
  }

  #endregion

  #region Public Functions
  public void OnInit()
  {
    if (!LoadGameData())
    {
      // initialize save game data if it doesn't exist
      gameData.ViewInversion = false;
      gameData.ScrollInversion = true;
      gameData.BackgroundDisabled = false;
      gameData.Highscore = 0;
      gameData.EndlessHighScore = 0;
      gameData.MasterVolume = 0;
      gameData.BackgroundVolume = -20;
      gameData.SFXVolume = -20;
      gameData.TutorialComplete = false;

      SaveGameData();
    }

    player.OnInit();
    theCamera.OnInit();
    DetectAndSetAspectRatio();
    mainObstacle.OnInit();
    HandleBackground();
    obstacles.AddObstacle(endlessModeEnabled, m_Progress);
    pages.TurnPageOn(PageType.Menu);
    mixer.OnInit(gameData.MasterVolume, gameData.BackgroundVolume, gameData.SFXVolume);
    AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.ST_01);
  }

  public void OnUpdate()
  {
    player.OnUpdate();
    theCamera.OnUpdate();
    mainObstacle.OnUpdate();
    CheckPlayerProgress();
  }

  public void OnFixedUpdate()
  {
    player.OnFixedUpdate();
    theCamera.OnFixedUpdate();
  }

  public void OnPlayerHitObstacle()
  {
#if UNITY_EDITOR
    if (playerInvulnerable)
    {
      Log("The player is invulnerable, but would have died.");
      return;
    }
#endif

    if (!m_PlayerDied)
    {
      AudioController.Instance.PlayAudio(UnityCore.Audio.AudioType.SFX_01, true);
      m_PlayerDied = true;
      DisablePlayerTrail();
    }

    EndGame();
  }

  public void OnPlayerHitPickup(int _bonus)
  {
    switch (_bonus)
    {
      case 0:
        firstBonus = true;
        bonusTextOne.gameObject.SetActive(true);
        break;
      case 1:
        secondBonus = true;
        bonusTextTwo.gameObject.SetActive(true);
        break;
      case 2:
        thirdBonus = true;
        bonusTextThree.gameObject.SetActive(true);
        break;
      case 3:
        fourthBonus = true;
        bonusTextFour.gameObject.SetActive(true);
        break;
      default:
        break;
    }
  }

  public void TryAgain()
  {
    Reset();
  }

  public void OnPause()
  {
    Pause();
  }

  public void OnUnpause()
  {
    Unpause();
  }

  public string CheckAndSetHighscore()
  {
    if (!endlessModeEnabled)
    {
      if (score > gameData.Highscore)
      {
        gameData.Highscore = score;
        return "Congrats! New highscore: " + score;
      }
      else
      {
        return "Current highscore: " + gameData.Highscore;
      }
    }
    else
    {
      if (score > gameData.EndlessHighScore)
      {
        gameData.EndlessHighScore = score;
        return "Congrats! New endless highscore: " + score;
      }
      else
      {
        return "Current endless highscore: " + gameData.EndlessHighScore;
      }
    }
  }

  public void SaveSettings()
  {
    gameData.ViewInversion = settings.viewInversionSetting;
    gameData.ScrollInversion = settings.scrollInversionSetting;
    gameData.BackgroundDisabled = settings.backgroundDisabled;
    gameData.MasterVolume = settings.masterVolumeSetting;
    gameData.BackgroundVolume = settings.bGMVolumeSetting;
    gameData.SFXVolume = settings.sFXVolumeSetting;
    SaveGameData();
  }

  public void DeleteSaveData()
  {
    gameData.Highscore = 0;
    gameData.EndlessHighScore = 0;
    DeleteGameData(SaveDataSlot.One);
  }

  public void DisablePlayerTrail()
  {
    player.DisableTrail();
  }

  public void PlayerHitVictoryPickup()
  {
    Debug.Log("The Player has won");
    victory = true;
    EndGame(victory);
  }

  public void HandleBackground(int _backgroundIndex = 1)
  {
    theCamera.HandleBackground(gameData.BackgroundDisabled, _backgroundIndex);
  }

  public void SetEndless(bool _endless)
  {
    endlessModeEnabled = _endless;
  }

  #endregion

  #region Private Functions
  private void CheckPlayerProgress()
  {
    if (player.transform.position.y > obstacles.spawnPoint)
    {
      score += m_ScoreMultiplier;
      scoreText.text = "Score: " + score.ToString();
      obstacles.AddObstacle(endlessModeEnabled);
      obstacles.CheckAndSetDoom();
      ApplyDoomTier(obstacles.doomTier);
      if (m_EndlessModeEnabled)
      {
        mainObstacle.ReduceMoveSpeedBy(.1f);
      }
    }


    if (firstBonus & secondBonus & thirdBonus & fourthBonus)
    {
      bonusComplete = true;
    }

    if (endlessModeEnabled) return;
    SpawnPickup(firstBonus, 32, 0);
    SpawnPickup(secondBonus, 80, 1);
    SpawnPickup(thirdBonus, 180, 2);
    SpawnPickup(fourthBonus, 230, 3);
  }

  private void ApplyDoomTier(int _doom)
  {
    if (currentDoom == obstacles.doomTier) return;
    if (endlessModeEnabled) return;
    switch (_doom)
    {
      case 1:
        theCamera.HandleBackground(gameData.BackgroundDisabled, 3);
        mainObstacle.ReduceMoveSpeedTo(0f);
        Log("Doom Tier 1");
        break;
      case 2:
        Log("Doom Tier 2");
        break;
      default:
        break;
    }
    currentDoom = _doom;
  }

  private async void EndGame()
  {
    if (m_GameOver) return;
    m_GameOver = true;
    gameStarted = false;
    player.Pause();

    SaveGameData();

    await Task.Delay(1000);
    pages.TurnPageOn(PageType.GameOver);
  }

  private async void EndGame(bool _victory)
  {
    if (m_GameOver) return;
    m_GameOver = true;
    gameStarted = false;
    player.Pause();
    mainObstacle.StopMoving();
    if (bonusComplete)
    {
      score += 1307;
    }
    else
    {
      score += 39;
    }

    SaveGameData();

    await Task.Delay(1000);
    pages.TurnPageOn(PageType.GameOver);
  }

  private void Reset()
  {
    score = 0;
    scoreText.text = "Score: " + score.ToString();
    m_Progress = -1;

    obstacles.Reset();
    pickups.ResetPickups();

    // This negative one causes the first obstacle to be placed in the correct location.
    // This should definitely probably be resolved as it locks in the size of the first obstacle.
    obstacles.AddObstacle(endlessModeEnabled, -1);

    // adds the next obstacle, I should make the add obstacle controller better, but this is a
    // faster solution at the moment
    obstacles.AddObstacle(endlessModeEnabled);
    theCamera.Reset();
    player.Reset();
    mainObstacle.Reset();
    HandleBackground();

    m_ScoreMultiplier = 1;
    m_GameOver = false;
    Time.timeScale = 1;
    m_PlayerDied = false;
    gameStarted = true;
    isPaused = false;
    ResetBonuses();
    victory = false;
  }

  private void Pause()
  {
    Time.timeScale = 0;
    if (!isPaused)
    {
      pages.TurnPageOn(PageType.PausePopup);
    }
    isPaused = true;
  }

  private void Unpause()
  {
    Time.timeScale = 1;
    isPaused = false;
  }

  /// <summary>
  /// Spawns a bonus pickup leveraging the pickup controller
  /// </summary>
  /// <param name="_bonus">GameController boolean for which bonus.</param>
  /// <param name="_position">Y coordinate player must be past for spawn.</param>
  /// <param name="_index">Which bonus to spawn based of a 0 based list index.</param>
  private void SpawnPickup(bool _bonus, float _position, int _index)
  {
    if (!_bonus & player.transform.position.y > _position)
    {
      pickups.SpawnPickup(_index);
    }
  }

  private void ResetBonuses()
  {
    firstBonus = false;
    secondBonus = false;
    thirdBonus = false;
    fourthBonus = false;

    bonusTextOne.gameObject.SetActive(false);
    bonusTextTwo.gameObject.SetActive(false);
    bonusTextThree.gameObject.SetActive(false);
    bonusTextFour.gameObject.SetActive(false);

    bonusComplete = false;
  }

  private void DetectAndSetAspectRatio()
  {
    float _aspectRatio = (float)Screen.height / (float)Screen.width;

    theCamera.HandleSize(_aspectRatio);
  }

  private void SaveGameData(SaveDataSlot _slot = SaveDataSlot.One)
  {
    gameData.SaveGameData(_slot);
  }

  private bool LoadGameData(SaveDataSlot _slot = SaveDataSlot.One)
  {
    return gameData.LoadGameData(_slot);
  }

  private void DeleteGameData(SaveDataSlot _slot)
  {
    gameData.DeleteGameData(_slot);
  }

  private void Log(string _msg)
  {
    Debug.Log("[Game Controller]: " + _msg);
  }

  #endregion
}
