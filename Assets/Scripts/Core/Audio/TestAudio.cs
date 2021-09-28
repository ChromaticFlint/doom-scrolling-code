using UnityEngine;

namespace UnityCore
{
  namespace Audio
  {
    public class TestAudio : MonoBehaviour
    {
      public AudioController audioController;

      #region Unity Functions
#if UNITY_EDITOR
      private void Update()
      {
        // Play Audio test for track 1
        if (Input.GetKeyDown(KeyCode.T))
        {
          audioController.PlayAudio(AudioType.ST_01, true, 1.0f);
        }

        // End Audio test for track 1
        if (Input.GetKeyDown(KeyCode.G))
        {
          audioController.StopAudio(AudioType.ST_01, true);
        }

        // Restart Audio test for track 1
        if (Input.GetKeyDown(KeyCode.B))
        {
          audioController.RestartAudio(AudioType.ST_01, true);
        }

        // Play Audio test for track 2
        if (Input.GetKeyDown(KeyCode.Y))
        {
          audioController.PlayAudio(AudioType.SFX_01);
        }

        // End Audio test for track 2
        if (Input.GetKeyDown(KeyCode.H))
        {
          audioController.StopAudio(AudioType.SFX_01);
        }

        // Restart Audio test for track 2
        if (Input.GetKeyDown(KeyCode.N))
        {
          audioController.RestartAudio(AudioType.SFX_01);
        }
      }
#endif
      #endregion

      #region Private Functions

      #endregion
    }
  }
}

