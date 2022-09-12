using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public GameObject settingPanel;

    public AudioMixer audioMixer;

    public Resolution[] resolution;

    public Dropdown resolutionDropdown;

    private void Start()
    {
        //gather resolutions information
        resolution = Screen.resolutions;

        //clear resolution dropdown menu
        resolutionDropdown.ClearOptions();

        //create list of options and write to resolution dropdown menu
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + " x " + resolution[i].height;
            options.Add(option);
            if (resolution[i].width == Screen.width && resolution[i].height == Screen.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex + 1);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolution[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void CloseSettingsPanel()
    {
        settingPanel.SetActive(false);
    }
}