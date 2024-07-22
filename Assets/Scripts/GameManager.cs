using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Tooltip("auto Hides touchscreen panel in android platform")]
    public GameObject touchPanel;
    public GameObject HudPanel, PausePanel, GameOverPanel, settingsPanel, loadingPanel;
    public TextMeshProUGUI scoreTxt, highScoreTxt, GameOverTxt, enemiesDestroidTxt;
    private int currentScore, highScore;
    public bool isGameOver;
    public GameObject Espawner, difficultyManager;
    public Material[] Skybox_mats;
    [Header("For Player")]
    public Image crosshair;
    public FloatingJoystick joystick;
    public bool canFire = false, gamePaused=false;
    public GameObject LaunchButton;
    private Transform _pl;
    public GameObject AmmoText;
    public AudioMixer audioMixer;
    public Slider sensitivitySlider;
    private float sensitivity;
    public GameObject GlobalVolume;
    public Toggle GlobalVolumeToggle;
    public Transform Music;
    public RewardedAdsButton _rAB;
    public GameObject ContinueBtn;
    int gameLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        int index = Random.Range(0, Skybox_mats.Length);
        RenderSettings.skybox = Skybox_mats[index];
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");

        _pl = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(_pl==null)
        {
            print("_pl is null");
        }
        if (Time.timeScale == 0) Time.timeScale = 1;
        isGameOver = false;
        PausePanel.SetActive(false);
        StartCoroutine(SpawnEnemies());

        if(Application.platform==RuntimePlatform.Android)
        {
            touchPanel.SetActive(true);
        }
        else
        {
            touchPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(!PlayerPrefs.HasKey("GlobalVolume"))
        {
            PlayerPrefs.SetInt("GlobalVolume", 0);
        }
        if(PlayerPrefs.GetInt("GlobalVolume")==0)
        {
            TurnOnOffGlobalVolume(false);
            GlobalVolumeToggle.isOn = false;
            
        }
        else
        {
            TurnOnOffGlobalVolume(true);
            GlobalVolumeToggle.isOn = true;
        }
        try
        {
            Music = GameObject.FindGameObjectWithTag("Music").transform.GetComponent<Transform>();
            Destroy(Music.gameObject);
        }
        catch (System.Exception e)
        {
            print(e.ToString());
        }

        if(!PlayerPrefs.HasKey("RewardAds"))
        {
            PlayerPrefs.SetInt("RewardAds", gameLives);
        }

        gameLives = PlayerPrefs.GetInt("RewardAds");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&!isGameOver)
        {
            if(!gamePaused)
            {
                GamePaused();
                gamePaused = true;
            }



            if(Application.platform==RuntimePlatform.Android)
            {
                return;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void GamePaused()
    {
        HudPanel.SetActive(false);
        touchPanel.SetActive(false);
        Time.timeScale = 0;
        PausePanel.SetActive(true);

    }

    public void ResumeGame()
    {
        HudPanel.SetActive(true);
        if(Application.platform==RuntimePlatform.Android)touchPanel.SetActive(true);
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        if (Application.platform == RuntimePlatform.Android)
        {
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gamePaused = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public Image GetCrosshairImage()
    {
        return crosshair;
    }
    public FloatingJoystick GetJoystick()
    {
        return joystick;
    }

    public void ShowSettings()
    {
        PausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }    
    public void SettingsBack()
    {
        PausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void GameOver(int score, int enemiesDestroidCount)
    {
        isGameOver = true;
        highScore = PlayerPrefs.GetInt("highScore");
        int money = PlayerPrefs.GetInt("Money");
        money += score;
        PlayerPrefs.SetInt("Money", money);
        if(highScore<score)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", score);
        }
        scoreTxt.text = "Score: " + score.ToString();
        highScoreTxt.text = "High Score: " + highScore.ToString();
        enemiesDestroidTxt.text = "Destroyed " + enemiesDestroidCount.ToString() + " enemies";
        touchPanel.SetActive(false);
        HudPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(gameoverflicker());
        if(gameLives>0)
        {
            _rAB.LoadAd();
            gameLives--;
            PlayerPrefs.SetInt("RewardAds", gameLives);
        }
    }

    IEnumerator gameoverflicker()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            GameOverTxt.enabled = false;        
            yield return new WaitForSeconds(0.2f);
            GameOverTxt.enabled = true;
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("tempScore", 0);
        PlayerPrefs.DeleteKey("RewardAds");
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void SetSensitivity(float sensitivity)
    {
        this.sensitivity = sensitivity;
        PlayerPrefs.SetFloat("sensitivity",sensitivity);
    }


    public void MainMenu()
    {
        ContinueBtn.SetActive(false);
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3);
        Espawner.SetActive(true);
        difficultyManager.SetActive(true);
    }

    public bool IsGameOver()
    {
        if(isGameOver)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnFireButtonDown()
    {
        canFire = true;
    }
    public void OnFireButtonUp()
    {
        canFire = false;
    }

    public void ShowLaunchButton()
    {
        LaunchButton.SetActive(true);
    }

    public void Launch()
    {
        _pl.GetComponent<RocketLauncher>().Launch();
    }

    public void ShowAmmoText()
    {
        AmmoText.SetActive(true);
    }

    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("bgMusic", vol);
    }

    public void TurnOnOffGlobalVolume(bool tog)
    {
        GlobalVolume.SetActive(!tog);
        if(tog==false)
        {
            PlayerPrefs.SetInt("GlobalVolume", 0);
        }
        else
        {
            PlayerPrefs.SetInt("GlobalVolume", 1);
        }
    }
}
