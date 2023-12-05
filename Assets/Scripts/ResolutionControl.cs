using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    void Start()
    {
        Resolution currentResolution = new Resolution();
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        foreach (var res in resolutions)
        {
            bool rep = false;
            foreach (var filRes in filteredResolutions)
            {
                if (res.width == filRes.width && res.height == filRes.height) rep = true;
            }
            if(!rep) filteredResolutions.Add(res);
        }

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
        resolutionDropdown.value = options.IndexOf(currentResolution.width + "x" + currentResolution.height + " ");
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex) 
    {
        GameManager.Instance.SetResolution(filteredResolutions[resolutionIndex]);
    }

}
