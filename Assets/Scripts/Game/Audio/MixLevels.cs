using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
  public AudioMixer masterMixer;


  #region Public Functions

  public void OnInit(float _masterlvl, float _bgmlvl, float _sfxlvl)
  {
    SetMasterLvl(_masterlvl);
    SetBGMLvl(_bgmlvl);
    SetSFXLvl(_sfxlvl);
  }

  public void SetMasterLvl(float _masterlvl)
  {
    masterMixer.SetFloat("MasterVolume", _masterlvl);
  }

  public void SetBGMLvl(float _bgmlvl)
  {
    masterMixer.SetFloat("BGMVolume", _bgmlvl);
  }

  public void SetSFXLvl(float _sfxlvl)
  {
    masterMixer.SetFloat("SFXVolume", _sfxlvl);
  }
  #endregion
}
