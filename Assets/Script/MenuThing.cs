using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MenuThing : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public UnityEngine.UI.Toggle tog;
    Resolution[] resolutions;
    private void Start()
    {
        //resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentres = 0;
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentres = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        tog.isOn = Screen.fullScreen;
        resolutionDropdown.value = currentres;
        resolutionDropdown.RefreshShownValue();

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue(); 
    }

    public void setRes(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullscreen(bool screen)
    {
        Screen.fullScreen = screen;
    }
}
