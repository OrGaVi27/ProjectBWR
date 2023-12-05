using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateSlider : MonoBehaviour    
{
    Slider frameRateSlider;
    [SerializeField] private TextMeshProUGUI maxFPSText;
    void Start()
    {
        frameRateSlider = GetComponent<Slider>();
        if (!PlayerPrefs.HasKey("frameRate"))
        {
            PlayerPrefs.SetFloat("frameRate", 60f);
            Load();
        }

        else 
        {
            Load();
        }
    }

    public void ChangeFrameRate() 
    {
        GameManager.Instance.SetRefreshRate(frameRateSlider.value * 60);
        maxFPSText.text = Mathf.Round(frameRateSlider.value * 60).ToString();
        Save();
    }
    private void Load() 
    {
        frameRateSlider.value = PlayerPrefs.GetFloat("frameRate") / 60;
    }
    private void Save() 
    {
        PlayerPrefs.SetFloat("frameRate", (float)(frameRateSlider.value * 60));
    }
}
