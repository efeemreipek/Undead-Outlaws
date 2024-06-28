using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private TextMeshPro reloadingText;
    [SerializeField] private Image healthImage;
    [SerializeField] private TextMeshProUGUI diamondCountText;
    [SerializeField] private TextMeshProUGUI waveCountdownText;
    [SerializeField] private Image bloodOverlay;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TextMeshProUGUI audioSliderText;
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject restartMenuPanel;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private Image enemiesLeftImage;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI highestWaveText;


    public TextMeshProUGUI GetWeaponNameText() => weaponNameText;
    public TextMeshProUGUI GetAmmoCountText() => ammoCountText;
    public TextMeshPro GetReloadingText() => reloadingText;
    public Image GetHealthImage() => healthImage;
    public TextMeshProUGUI GetDiamondCountText() => diamondCountText;
    public TextMeshProUGUI GetWaveCountdownText() => waveCountdownText;
    public Image GetBloodOverlayImage() => bloodOverlay;
    public GameObject GetPauseMenuPanel() => pauseMenuPanel;
    public TMP_Dropdown GetResolutionDropdown() => resolutionDropdown;
    public TextMeshProUGUI GetAudioSliderText() => audioSliderText;
    public GameObject GetStartMenuPanel() => startMenuPanel;
    public GameObject GetSettingsMenuPanel() => settingsMenuPanel;
    public GameObject GetRestartMenuPanel() => restartMenuPanel;
    public TextMeshProUGUI GetWaveNumberText() => waveNumberText;
    public Image GetEnemiesLeftImage() => enemiesLeftImage;
    public Slider GetAudioSlider() => audioSlider;
    public TMP_Dropdown GetGraphicsDropdown() => graphicsDropdown;
    public Toggle GetFullscreenToggle() => fullscreenToggle;
    public TextMeshProUGUI GetScoreText() => scoreText;
    public TextMeshProUGUI GetWaveText() => waveText;
    public TextMeshProUGUI GetHighestScoreText() => highestScoreText;
    public TextMeshProUGUI GetHighestWaveText() => highestWaveText;
}
