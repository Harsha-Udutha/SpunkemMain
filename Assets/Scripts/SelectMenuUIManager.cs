using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenuUIManager : MonoBehaviour
{
    Spaceship one = new Spaceship(0, "Mark I", 100, 40, 80, 1f, "no missile launcher", 0);
    Spaceship two = new Spaceship(1, "Feisar", 110, 42, 100, 1.2f, "no missile launcher", 500);
    Spaceship three = new Spaceship(2, "Excalibur", 130, 46, 110, 1.5f, "no missile launcher", 1000);
    Spaceship four = new Spaceship(3, "Murray Leinster", 133, 45, 115, 1.7f, "no missile launcher", 1550);
    Spaceship five = new Spaceship(4, "Avalon", 140, 49, 115, 1.8f, "no missile launcher", 2269);
    Spaceship six = new Spaceship(5, "Gratzner ", 180, 54, 130, 2.0f, "no missile launcher", 3500);
    Spaceship seven = new Spaceship(6, "Milano", 180, 55, 135, 2.5f, "no missile launcher", 4206);
    Spaceship eight = new Spaceship(7, "Vonnegut", 190, 60, 130, 2.7f, "no missile launcher", 5490);
    Spaceship nine = new Spaceship(8, "Orion", 210, 62, 135, 3.1f, "30", 6695);
    Spaceship ten = new Spaceship(9, "Hyperion", 225, 65, 135, 3.2f, "100", 9999);
    Spaceship eleven = new Spaceship(10, "Nauvoo", 250, 66, 140, 3.6f, "200", 12099);
    Spaceship twelve = new Spaceship(11, "SDF-3 Pioneer", 260, 67, 140, 3.6f, "250", 15990);
    Spaceship thirteen = new Spaceship(12, "Valkyrie", 500, 70, 150, 3.8f, "500", 18800);
    Spaceship fourteen = new Spaceship(13, "Titan", 560, 75, 200, 4.5f, "500 (heath and ammo generated over time)", 20000);

    public TextMeshProUGUI nametxt,missileAmmotxt,price, ssCoins;
    public Slider health, speed, fireRange, damageRate;
    private CamSetup cs;
    private int index,money, shipPrice;
    public Button startButton;
    public GameObject choosePanel,shipDetailsPanel,buyButton, CantBuyPanel, globalVolume;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        PlayerPrefs.SetInt("tempScore", -1);

        if(PlayerPrefs.HasKey("twoPrice"))
        {
            two.Price = 0;
        }
        if(PlayerPrefs.HasKey("threePrice"))
        {
            three.Price = 0;
        }
        if (PlayerPrefs.HasKey("fourPrice"))
        {
            four.Price = 0;
        }
        if (PlayerPrefs.HasKey("fivePrice"))
        {
            five.Price = 0;
        }
        if (PlayerPrefs.HasKey("sixPrice"))
        {
            six.Price = 0;
        }
        if (PlayerPrefs.HasKey("sevenPrice"))
        {
            seven.Price = 0;
        }
        if (PlayerPrefs.HasKey("eightPrice"))
        {
            eight.Price = 0;
        }
        if (PlayerPrefs.HasKey("ninePrice"))
        {
            nine.Price = 0;
        }
        if (PlayerPrefs.HasKey("tenPrice"))
        {
            ten.Price = 0;
        }
        if (PlayerPrefs.HasKey("elevenPrice"))
        {
            eleven.Price = 0;
        }
        if (PlayerPrefs.HasKey("twelvePrice"))
        {
            twelve.Price = 0;
        }
        if (PlayerPrefs.HasKey("thirteenPrice"))
        {
            thirteen.Price = 0;
        }
        if (PlayerPrefs.HasKey("fourteenPrice"))
        {
            fourteen.Price = 0;
        }



        if (!PlayerPrefs.HasKey("ShipdetailsSet"))
        {
            PlayerPrefs.SetInt("ShipdetailsSet", 0);
        }
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
        money = PlayerPrefs.GetInt("Money");
        ssCoins.text = money.ToString();
        cs = Camera.main.GetComponent<CamSetup>();
        if(cs==null)
        {
            print("cs is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        index = cs.GetCurrentRef();
        switch (index)
        {
            case 0:
                nametxt.text = one.Name;
                health.value = Mathf.Lerp(health.value, one.Health, 10 * Time.deltaTime); 
                speed.value = Mathf.Lerp(speed.value, one.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, one.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, one.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = one.MissileAmmo;
                price.text = one.Price.ToString();
                break;
            case 1:
                nametxt.text = two.Name;
                health.value = Mathf.Lerp(health.value, two.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, two.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, two.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value,two.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = two.MissileAmmo;
                price.text = two.Price.ToString();
                break;
            case 2:
                nametxt.text = three.Name;
                health.value = Mathf.Lerp(health.value, three.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, three.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, three.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, three.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = three.MissileAmmo;
                price.text = three.Price.ToString();
                break;
            case 3:
                nametxt.text = four.Name;
                health.value = Mathf.Lerp(health.value, four.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, four.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, four.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, four.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = four.MissileAmmo;
                price.text = four.Price.ToString();
                break;
            case 4:
                nametxt.text = five.Name;
                health.value = Mathf.Lerp(health.value, five.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, five.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, five.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, five.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = five.MissileAmmo;
                price.text = five.Price.ToString();
                break;
            case 5:
                nametxt.text = six.Name;
                health.value = Mathf.Lerp(health.value, six.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, six.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, six.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, six.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = six.MissileAmmo;
                price.text = six.Price.ToString();
                break;
            case 6:
                nametxt.text = seven.Name;
                health.value = Mathf.Lerp(health.value, seven.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, seven.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, seven.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, seven.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = seven.MissileAmmo;
                price.text = seven.Price.ToString();
                break;
            case 7:
                nametxt.text = eight.Name;
                health.value = Mathf.Lerp(health.value, eight.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, eight.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, eight.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, eight.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = eight.MissileAmmo;
                price.text = eight.Price.ToString();
                break;
            case 8:
                nametxt.text = nine.Name;
                health.value = Mathf.Lerp(health.value, nine.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, nine.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, nine.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, nine.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = nine.MissileAmmo;
                price.text = nine.Price.ToString();
                break;
            case 9:
                nametxt.text = ten.Name;
                health.value = Mathf.Lerp(health.value, ten.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, ten.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, ten.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, ten.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = ten.MissileAmmo;
                price.text = ten.Price.ToString();
                break;
            case 10:
                nametxt.text = eleven.Name;
                health.value = Mathf.Lerp(health.value, eleven.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, eleven.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, eleven.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, eleven.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = eleven.MissileAmmo;
                price.text = eleven.Price.ToString();
                break;
            case 11:
                nametxt.text = twelve.Name;
                health.value = Mathf.Lerp(health.value, twelve.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, twelve.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, twelve.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, twelve.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = twelve.MissileAmmo;
                price.text = twelve.Price.ToString();
                break;
            case 12:
                nametxt.text = thirteen.Name;
                health.value = Mathf.Lerp(health.value, thirteen.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, thirteen.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, thirteen.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, thirteen.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = thirteen.MissileAmmo;
                price.text = thirteen.Price.ToString();
                break;
            case 13:
                nametxt.text = fourteen.Name;
                health.value = Mathf.Lerp(health.value, fourteen.Health, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, fourteen.Speed, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, fourteen.FireRange, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, fourteen.DamageRate, 10 * Time.deltaTime);
                missileAmmotxt.text = fourteen.MissileAmmo;
                price.text = fourteen.Price.ToString();
                break;
            default:
                nametxt.text = "none";
                health.value = Mathf.Lerp(health.value, 0, 10 * Time.deltaTime);
                speed.value = Mathf.Lerp(speed.value, 0, 10 * Time.deltaTime);
                fireRange.value = Mathf.Lerp(fireRange.value, 0, 10 * Time.deltaTime);
                damageRate.value = Mathf.Lerp(damageRate.value, 0, 10 * Time.deltaTime);
                missileAmmotxt.text = one.MissileAmmo;
                price.text = one.Price.ToString();
                break;
        }
        shipPrice = int.Parse(price.text);
        if (shipPrice==0)
        {
            buyButton.SetActive(false);
            startButton.interactable = true;
        }
        else
        {
            buyButton.SetActive(true);
            startButton.interactable = false;
        }
    }

    public void Buy()
    {
        if(shipPrice<=money)
        {
            money -= shipPrice;
            ssCoins.text = money.ToString();
            PlayerPrefs.SetInt("Money", money);
            switch (index)
            {
                case 1: two.Price =0; PlayerPrefs.SetInt("twoPrice", 0); break;
                case 2: three.Price = 0; PlayerPrefs.SetInt("threePrice", 0); break;
                case 3: four.Price = 0; PlayerPrefs.SetInt("fourPrice", 0); break;
                case 4: five.Price = 0; PlayerPrefs.SetInt("fivePrice", 0); break;
                case 5: six.Price = 0; PlayerPrefs.SetInt("sixPrice", 0); break;
                case 6: seven.Price = 0; PlayerPrefs.SetInt("sevenPrice", 0); break;
                case 7: eight.Price = 0; PlayerPrefs.SetInt("eightPrice", 0); break;
                case 8: nine.Price = 0; PlayerPrefs.SetInt("ninePrice", 0); break;
                case 9: ten.Price = 0; PlayerPrefs.SetInt("tenPrice", 0); break;
                case 10: eleven.Price = 0; PlayerPrefs.SetInt("elevenPrice", 0); break;
                case 11: twelve.Price = 0; PlayerPrefs.SetInt("twelvePrice", 0); break;
                case 12: thirteen.Price = 0;PlayerPrefs.SetInt("thirteenPrice", 0); break;
                case 13: fourteen.Price = 0; PlayerPrefs.SetInt("fourteenPrice", 0); break;
                default: return;
            }
        }
        else
        {
            choosePanel.SetActive(false);
            shipDetailsPanel.SetActive(false);
            CantBuyPanel.SetActive(true);
        }
    }

    public void closeCantBuyPanel()
    {
        CantBuyPanel.SetActive(false);
        choosePanel.SetActive(true);
        shipDetailsPanel.SetActive(true);
    }    
}
