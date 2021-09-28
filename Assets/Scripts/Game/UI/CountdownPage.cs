using System.Collections;
using UnityEngine;
using TMPro;
using UnityCore.Menu;

public class CountdownPage : Page
{
  public PageController pages;
  public TMP_Text countdownText;

  public IEnumerator RunCountdown()
  {
    int _timer = 2;
    countdownText.text = countdownTextSwitch(_timer);

    while (_timer > 0)
    {
      yield return new WaitForSecondsRealtime(1);
      _timer--;
      countdownText.text = countdownTextSwitch(_timer);
    }
    pages.TurnPageOff(type);
    GameController.Instance.OnUnpause();
  }

  protected override void OnPageEnabled()
  {
    base.OnPageEnabled();
    StartCoroutine(RunCountdown());
  }

  private string countdownTextSwitch(int _timer)
  {
    switch (_timer)
    {
      case 2:
        return "Ready!";
      case 1:
        return "Set!";
      case 0:
        return "Go!"; // This never appears due to the page being deactivated at 0. Might need to adjust this later.
      default:
        return "potato";
    }
  }
}
