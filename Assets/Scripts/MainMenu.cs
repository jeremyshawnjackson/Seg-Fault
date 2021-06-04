using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Dropdown ResolutionDropdown; 
    public Dropdown LevelSelectDropdown;
    Resolution[] Resolutions;
    void Start()
    {
        Debug.Log(Screen.resolutions);
        Resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < Resolutions.Length; i++)
        {
            string option = Resolutions[i].width + " x " + Resolutions[i].height;
            Debug.Log(option);
            options.Add(option);

            if (Resolutions[i].width == Screen.currentResolution.width &&
                Resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            ResolutionDropdown.value = currentResolutionIndex;
            ResolutionDropdown.RefreshShownValue();
        }

        ResolutionDropdown.AddOptions(options);

        LevelSelectDropdown.ClearOptions();
        options = new List<string>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            options.Add("Level " + i);
        }
        LevelSelectDropdown.AddOptions(options);
        LevelSelectDropdown.RefreshShownValue();

    }

    public AudioMixer audioMixer;
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1-1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SelectLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
