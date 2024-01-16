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
            //frameRateSlider.value = 60f;
            //ChangeFrameRate();
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
        GameManager.Instance.SetRefreshRate(frameRateSlider.value);
        if (frameRateSlider.value == 241)
        {
            maxFPSText.text = "Unlimited";
            GameManager.Instance.SetRefreshRate(0);
        }
        else maxFPSText.text = frameRateSlider.value.ToString();

        Save();
    }
    private void Load() 
    {
        frameRateSlider.value = PlayerPrefs.GetFloat("frameRate");
        ChangeFrameRate();
    }
    private void Save() 
    {
        PlayerPrefs.SetFloat("frameRate", (frameRateSlider.value));
    }
}
