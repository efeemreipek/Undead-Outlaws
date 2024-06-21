using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private const string PREFS_AUDIO = "PrefsAudio";
    private const string PREFS_RESOLUTION = "PrefsResolution";
    private const string PREFS_GRAPHIC = "PrefsGraphic";
    private const string PREFS_FULLSCREEN = "PrefsFullscreen";

    public static GameManager Instance { get; private set; }


    [SerializeField] private GameObject startMenuFirstSelected;
    [SerializeField] private GameObject pauseMenuFirstSelected;
    [SerializeField] private GameObject settingsMenuFirstSelected;
    [SerializeField] private GameObject restartMenuFirstSelected;
    [SerializeField] private AudioMixer mainMixer;


    private bool isGamePaused = false;
    private Resolution[] resolutions;
    private bool isGameStarted = false;
    private bool isPlayerDead = false;

    public static Action OnGameRestarted;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        UIManager.Instance.GetPauseMenuPanel().SetActive(isGamePaused);

        StartGame();


        //Get resolutions and add to the dropdown
        resolutions = Screen.resolutions;
        var resolutionDropdown = UIManager.Instance.GetResolutionDropdown();
        resolutionDropdown.ClearOptions();

        List<string> resolutionStringList = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString() + "@" + Mathf.Round((float)resolutions[i].refreshRateRatio.value);
            resolutionStringList.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(resolutionStringList);

        UIManager.Instance.GetAudioSlider().value = PlayerPrefs.GetInt(PREFS_AUDIO);
        UIManager.Instance.GetGraphicsDropdown().value = PlayerPrefs.GetInt(PREFS_GRAPHIC);
        UIManager.Instance.GetResolutionDropdown().value = PlayerPrefs.GetInt(PREFS_RESOLUTION);
        UIManager.Instance.GetFullscreenToggle().isOn = PlayerPrefs.GetInt(PREFS_FULLSCREEN) == 1 ? true : false;

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        PlayerHealth.OnPlayerDead += PlayerHealth_OnPlayerDead;

    }

    private void Update()
    {
        if (InputManager.Instance.GetIsPausePressed())
        {
            Pause();
        }
        else if (InputManager.Instance.GetIsPauseUIPressed())
        {
            if (!isPlayerDead)
            {
                if (UIManager.Instance.GetSettingsMenuPanel().activeInHierarchy)
                {
                    BackButton();
                }
                else if(UIManager.Instance.GetPauseMenuPanel().activeInHierarchy)
                {
                    Unpause();
                }
            }
        }

        if (isGamePaused)
        {
            AudioManager.Instance.PauseSound();
        }
        else
        {
            AudioManager.Instance.UnpauseSound();
        }
    }

    private void Pause()
    {
        SwitchFromPlayerToUI();
        UIManager.Instance.GetPauseMenuPanel().SetActive(isGamePaused);

        EventSystem.current.SetSelectedGameObject(pauseMenuFirstSelected);
    }

    private void Unpause()
    {
        SwitchFromUIToPlayer();
        UIManager.Instance.GetPauseMenuPanel().SetActive(isGamePaused);

    }

    private void StartGame()
    {
        SwitchFromPlayerToUI();
        UIManager.Instance.GetStartMenuPanel().SetActive(isGamePaused);
        EventSystem.current.SetSelectedGameObject(startMenuFirstSelected);
    }

    public void PlayButton()
    {
        Unpause();
        UIManager.Instance.GetStartMenuPanel().SetActive(isGamePaused);
        isGameStarted = true;
    }


    public void ResumeButton()
    {
        Unpause();
    }

    public void SettingsButton()
    {
        if (isGameStarted)
        {
            UIManager.Instance.GetPauseMenuPanel().SetActive(false);
            UIManager.Instance.GetSettingsMenuPanel().SetActive(true);
        }
        else
        {
            UIManager.Instance.GetStartMenuPanel().SetActive(false);
            UIManager.Instance.GetSettingsMenuPanel().SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(settingsMenuFirstSelected);

    }

    public void QuitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackButton()
    {
        if (isGameStarted)
        {
            UIManager.Instance.GetPauseMenuPanel().SetActive(true);
            UIManager.Instance.GetSettingsMenuPanel().SetActive(false);
            EventSystem.current.SetSelectedGameObject(pauseMenuFirstSelected);
        }
        else
        {
            UIManager.Instance.GetStartMenuPanel().SetActive(true);
            UIManager.Instance.GetSettingsMenuPanel().SetActive(false);
            EventSystem.current.SetSelectedGameObject(startMenuFirstSelected);
        }

        PlayerPrefs.SetInt(PREFS_AUDIO, (int)UIManager.Instance.GetAudioSlider().value);
        PlayerPrefs.SetInt(PREFS_GRAPHIC, UIManager.Instance.GetGraphicsDropdown().value);
        PlayerPrefs.SetInt(PREFS_RESOLUTION, UIManager.Instance.GetResolutionDropdown().value);
        PlayerPrefs.SetInt(PREFS_FULLSCREEN, UIManager.Instance.GetFullscreenToggle().isOn ? 1 : 0);
    }

    public void PlayAgainButton()
    {
        OnGameRestarted?.Invoke();

        SwitchFromUIToPlayer();

        UIManager.Instance.GetRestartMenuPanel().SetActive(false);

        
    }

    #region Settings Menu Methods
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
        UIManager.Instance.GetAudioSliderText().text = (volume + 80).ToString();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    #endregion

    private void PlayerHealth_OnPlayerDead()
    {
        isPlayerDead = true;

        SwitchFromPlayerToUI();

        UIManager.Instance.GetRestartMenuPanel().SetActive(true);

        EventSystem.current.SetSelectedGameObject(restartMenuFirstSelected);
    }

    private void SwitchFromPlayerToUI()
    {
        isGamePaused = !isGamePaused;
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    private void SwitchFromUIToPlayer()
    {
        isGamePaused = !isGamePaused;
        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = isGamePaused ? 0f : 1f;
    }


    public bool GetIsGameStarted() => isGameStarted;
    public bool GetIsGamePaused() => isGamePaused;
}
