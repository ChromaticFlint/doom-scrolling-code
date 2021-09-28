using UnityEngine;
using UnityCore.Menu;

namespace UnityCore
{
  namespace Scene
  {
    public class TestScene : MonoBehaviour
    {
      public SceneController sceneController;

      #region Unity Functions
#if UNITY_EDITOR
      private void Update()
      {
        if (Input.GetKeyDown(KeyCode.M))
        {
          sceneController.Load(SceneType.Menu, (_scene) =>
          {
            Debug.Log("Scene [" + _scene + "] loaded from test script!");
          }, false, PageType.Loading);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
          sceneController.Load(SceneType.Game);
        }
      }
#endif
      #endregion
    }
  }
}
