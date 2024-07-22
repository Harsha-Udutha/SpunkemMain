using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel, loadingPanel , creditsPanel;
    public TextMeshProUGUI HintTxt, highScore;
    int Score;

    public string[] hints;

    void Start()
    {
        Time.timeScale = 1;
        if(!PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("sensitivity", 10);
        }

        if(PlayerPrefs.HasKey("tempScore"))
        {
            PlayerPrefs.DeleteKey("tempScore");
        }
        if(PlayerPrefs.HasKey("RewardAds"))
        {
            PlayerPrefs.DeleteKey("RewardAds");
        }
        if (PlayerPrefs.HasKey("highScore"))
        {
            Score = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            Score = 0;
        }

        highScore.text = "high score: " + Score.ToString();
    }

    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        HintTxt.text = "Hint: " +hints[Random.Range(0, hints.Length)];
        loadingPanel.SetActive(true);
        StartCoroutine(LoadNow());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadNow()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SelectionMenu");
    }

    public void setMoney()
    {
        PlayerPrefs.SetInt("Money", 99999999);
    }

    public void deletePlayerprefs()
    {
        PlayerPrefs.DeleteAll();
    }    

    public void ShowCredits()
    {
        MainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackBtn()
    {
        creditsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

     public void SendEmail()
    {
        string email = "huudgamestudio@gmail.com";
        string subject = MyEscapeURL("");
        string body = MyEscapeURL("");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    public void openInstagram()
    {
        Application.OpenURL("https://www.instagram.com/huudgamestudios/");
    }

    public void openGamePage()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.HuuDGameStudios.Spunkem");
    }

    public void openWebsite()
    {
        Application.OpenURL("https://huudgamestudios.netlify.app/");
    }
}
