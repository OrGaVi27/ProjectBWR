using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject FPSToggle;
    public GameObject vSyncToggle;
    public GameObject fullScreen;
    private float startTime;
    void OnEnable()
    {
        startTime = Time.time;
        FPSToggle.GetComponent<Toggle>().isOn = GameManager.Instance.GetComponent<FPSCounter>().enabled;
        vSyncToggle.GetComponent<Toggle>().isOn = Convert.ToBoolean(QualitySettings.vSyncCount);
        fullScreen.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("fullscreen", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("FPSCounter", Convert.ToInt32(GameManager.Instance.GetComponent<FPSCounter>().enabled));
        PlayerPrefs.SetFloat("vsync", QualitySettings.vSyncCount);
        PlayerPrefs.Save();
    }
    public void SetFullScreen()
    {
        if (Time.time - startTime > 0.5f)
        {
            Screen.fullScreen = !Screen.fullScreen;
            Debug.Log("FS\n");
        }
    }
    public void SetVSync()
    {
        if (Time.time - startTime > 0.5f)
        {
            switch (QualitySettings.vSyncCount)
            {
                case 0:
                    QualitySettings.vSyncCount = 1;
                    break;
                case 1:
                    QualitySettings.vSyncCount = 0;
                    break;
            }
            Debug.Log("Vsync\n");
        }
    }
    public void SetFPSCounter()
    {
        if(Time.time - startTime > 0.5f)
        {
            GameManager.Instance.GetComponent<FPSCounter>().enabled = !GameManager.Instance.GetComponent<FPSCounter>().enabled;
            Debug.Log("FPS\n");
        }
    }
}
