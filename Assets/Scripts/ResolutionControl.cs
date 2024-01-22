using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private List<Resolution> filteredResolutions;

    void Start()
    {
        
        Resolution currentResolution = new Resolution();
        filteredResolutions = GetFilteredResolutions();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (var res in filteredResolutions)
        {
            string resolutionOption = res.width+ "x" + res.height + " ";
            options.Add(resolutionOption);
            if (res.width == Screen.width && res.height == Screen.height) 
            {
                    currentResolution = res;
            }
        }

        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = options.IndexOf(currentResolution.width + "x" + currentResolution.height + " ");

        if (!PlayerPrefs.HasKey("resolution"))
        {
            PlayerPrefs.SetInt("resolution", 1);
            Load();
        }

        else
        {
            Load();
        }
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex) 
    {
        GameManager.Instance.SetResolution(filteredResolutions[resolutionIndex]);
        Save();
    }
    private void Load()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
    }

    public static List<Resolution> GetFilteredResolutions() 
    {
        List<Resolution> filteredResolutions = new List<Resolution>();
        foreach (var res in Screen.resolutions)
        {
            bool rep = false;
            foreach (var filRes in filteredResolutions)
            {
                if (res.width == filRes.width && res.height == filRes.height) rep = true;
            }
            if (!rep) filteredResolutions.Add(res);
        }
        return filteredResolutions;
    }
}
