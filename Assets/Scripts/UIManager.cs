using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthSlider;
    public Image healthFillImage;
    public TextMeshProUGUI scoreTxt, ammoTxt;
    public AudioMixer audioMixer;

    float maxHealth;

    public void UpdatePlayerScore(int score)
    {
        scoreTxt.text = "Score: " + score.ToString();
    }

    public void SetHealthSliderMaxValue(float val)
    {
        playerHealthSlider.maxValue = val;
        maxHealth = val;
    }

    public void SetAmmo(int ammo)
    {
        ammoTxt.text = ammo.ToString();

    }

    public void SetHealthValue(float health)
    {
        if (health <= (maxHealth*0.2f))
        {
            healthFillImage.color = Color.red;
        }
        else if (health <= (maxHealth * 0.6f))
        {
            healthFillImage.color = Color.yellow;
        }
        else
        {
            healthFillImage.color = Color.green;
        }
        playerHealthSlider.value = health;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
