using UnityEngine;

namespace UnityCore
{

  namespace Menu
  {

    public class TestMenu : MonoBehaviour
    {
      public PageController pageController;

#if UNITY_EDITOR
      private void Update()
      {
        // test turning page on
        if (Input.GetKeyDown(KeyCode.F))
        {
          pageController.TurnPageOn(PageType.Loading);
        }

        // tests turning page off
        if (Input.GetKeyDown(KeyCode.G))
        {
          pageController.TurnPageOff(PageType.Loading);
        }

        // tests animating loading out while animating menu in
        if (Input.GetKeyDown(KeyCode.H))
        {
          pageController.TurnPageOff(PageType.Loading, PageType.Menu);
        }
        // tests fully animated out of loading to menu before animating the menu fade in
        if (Input.GetKeyDown(KeyCode.J))
        {
          pageController.TurnPageOff(PageType.Loading, PageType.Menu, true);
        }
      }
#endif
    }
  }
}
