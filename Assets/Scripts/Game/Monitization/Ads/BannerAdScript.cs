using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{
  public string gameId = "TestID";
  public string placementId = "TestPlacement";
  public bool testMode = true;

  private bool m_BannerActive;

  #region Unity Functions
  private void Start()
  {

    // If Android
#if UNITY_ANDROID && !UNITY_EDITOR
  gameId = "XXXXXXX";
  placementId = "Banner_Android";
  testMode = false;
  Debug.Log("On Android");
#endif

    // If iOS
#if UNITY_IPHONE && !UNITY_EDITOR
  gameId = "XXXXXX";
  placementId = "Banner_iOS";
  Debug.Log("On iOS");
  testMode = false;
#endif

    // Initialize the SDK if you haven't already done so:
    Advertisement.Initialize(gameId, testMode);
    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
  }
  #endregion

  #region Public Functions
  public void ShowBanner()
  {
    if (m_BannerActive) return;
    Debug.Log("Ad present");
    Advertisement.Banner.Show(placementId);
    m_BannerActive = true;
  }

  public void HideBanner()
  {
    if (!m_BannerActive) return;
    Debug.Log("Ad hidden");
    Advertisement.Banner.Hide();
    m_BannerActive = false;
  }
  #endregion

  #region Private Functions

  #endregion
}
