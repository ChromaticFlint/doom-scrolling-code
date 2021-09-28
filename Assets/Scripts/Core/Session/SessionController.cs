using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityCore.Menu;
using UnityCore.Scene;

namespace UnityCore
{
  namespace Session
  {
    public class SessionController : MonoBehaviour
    {
      public static SessionController Instance;

      private SceneController m_Scene;
      private long m_SessionStartTime;
      private bool m_IsPaused;
      private GameController m_Game;
      private float m_FPS;

      public bool debug;

      public long SessionStartTime
      {
        get
        {
          return m_SessionStartTime;
        }
      }

      public float fps
      {
        get
        {
          return m_FPS;
        }
      }

      private SceneController scene
      {
        get
        {
          if (!m_Scene)
          {
            m_Scene = SceneController.Instance;
          }
          if (!m_Scene)
          {
            Debug.LogWarning("Trying to access scenes, but no instance of SceneController was found.");
          }
          return m_Scene;
        }
      }

      #region Unity Functions
      private void Awake()
      {
        Configure();
      }

      private void OnApplicationFocus(bool _focus)
      {
        if (!_focus & m_Game.gameStarted & !m_Game.isPaused)
        {
          m_Game.OnPause();
          m_IsPaused = true;
        }
        else
        {
          m_IsPaused = false;
        }
      }

      private void Update()
      {
        if (m_IsPaused) return;
        if (!m_Game) return;
        m_Game.OnUpdate();
        m_FPS = Time.frameCount / Time.time;
      }

      private void FixedUpdate()
      {
        if (m_IsPaused) return;
        if (!m_Game) return;
        m_Game.OnFixedUpdate();
      }

      #endregion

      #region Public Functions
      public void InitializeGame(GameController _game)
      {
        m_Game = _game;
        m_Game.OnInit();
      }

      public void Unpause()
      {
        m_IsPaused = false;
        m_Game.OnUnpause();
      }

      #endregion

      #region Private Functions
      private void Configure()
      {
        if (!Instance)
        {
          Instance = this;
          StartSession();
          DontDestroyOnLoad(gameObject);
        }
        else
        {
          Destroy(gameObject);
        }
      }

      private async void StartSession()
      {
        m_SessionStartTime = EpochSeconds();
        await Task.Delay(2000);
        Application.targetFrameRate = 66;
        if (scene)
        {
          scene.Load(SceneType.Game, null, false, PageType.Loading);
        }
      }

      private long EpochSeconds()
      {
        var _epoch = new System.DateTimeOffset(System.DateTime.UtcNow);
        return _epoch.ToUnixTimeSeconds();
      }

      private void Log(string _msg)
      {
        if (debug)
        {
          Debug.Log("[SessionController]" + _msg);
        }
      }

      private void LogWarning(string _msg)
      {
        if (debug)
        {
          Debug.LogWarning("[SessionController" + _msg);
        }
      }

      #endregion
    }
  }
}
