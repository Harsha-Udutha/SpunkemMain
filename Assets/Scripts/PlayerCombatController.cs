using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerCombatController : MonoBehaviour
{
    //player variables
    [SerializeField] private float Health,maxHealth=100;
    public int score;
    [Tooltip("The Max Distance the Player can shoot")]
    public float FireRange=100;
    public float damage=2;
    //refrance variables
    public GameObject MuzzleFlashPrefab;
    public GameObject hitmarkerPrefab;
    private Image Crosshair;
    private UIManager _uim;
    public GameObject blastPrefab;
    private GameManager _gm;
    private AudioSource[] shootSound;
    int enemiesDestroid;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Health = maxHealth;
        shootSound = GetComponents<AudioSource>();
        _uim = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uim.SetHealthSliderMaxValue(maxHealth);
        _uim.SetHealthValue(Health);
        if (PlayerPrefs.HasKey("tempScore")) 
        {
            score = PlayerPrefs.GetInt("tempScore");
            score++;
        }
        else
        {
            score = 0;
        }
        _uim.UpdatePlayerScore(score);
        StartCoroutine(ScoreIncrement());
        Crosshair = _gm.GetCrosshairImage();
        enemiesDestroid = 0;

        if(PlayerPrefs.HasKey("RewardAds"))
        {
            int c = PlayerPrefs.GetInt("RewardAds");
            c++;
            print(c);
            setHealthAfterReward(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale!=0)fire();
        if (Health <= 0)
        {
            PlayerDestroy();
        }
        if(Time.timeScale==0)
        {
            for (int i = 0; i < shootSound.Length; i++)
            {
                shootSound[i].Stop();
            }
        }
    }

    private void PlayerDestroy()
    {
        Camera.main.transform.parent = null;
        GameObject BlastPrefab = Instantiate(blastPrefab, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("tempScore", score);
        _gm.GameOver(score, enemiesDestroid);
        Destroy(this.gameObject);
    }

    void fire()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, FireRange))
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                Crosshair.color = Color.red;
                if (Application.platform == RuntimePlatform.Android)
                {
                    if (_gm.canFire)
                    {
                        GameObject HitMarker = Instantiate(hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                        if (hitInfo.transform.name == "EnemyShip(Clone)" || hitInfo.transform.name == "EnemyShip2(Clone)")
                        {
                            hitInfo.transform.GetComponent<EnemyShipCombat>().EnemyDamage(damage);
                        }
                        else
                        {
                            hitInfo.transform.GetComponent<LaserBeam>().EnemyDamage(damage);
                        }
                    }
                }
                else if(Input.GetMouseButton(0))
                {
                    GameObject HitMarker = Instantiate(hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                    if(hitInfo.transform.name== "EnemyShip(Clone)"|| hitInfo.transform.name== "EnemyShip2(Clone)")
                    {
                        hitInfo.transform.GetComponent<EnemyShipCombat>().EnemyDamage(damage);
                    }
                    else
                    {
                        hitInfo.transform.GetComponent<LaserBeam>().EnemyDamage(damage);
                    }
                }
            }
            else if(hitInfo.transform.CompareTag("Astroid"))
            {
                if(Application.platform==RuntimePlatform.Android)
                {
                    if(_gm.canFire)
                    {
                        GameObject HitMarker = Instantiate(hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                    }
                }
                else
                {
                    if(Input.GetMouseButton(0))
                    {
                        GameObject HitMarker = Instantiate(hitmarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                    }
                }
            }
        }
        else 
        {
            Crosshair.color = Color.white;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if(_gm.canFire && Time.timeScale == 1) 
            {
                MuzzleFlashPrefab.SetActive(true);

                for (int i = 0; i < shootSound.Length; i++)
                {
                    if (shootSound[i].isPlaying == false)
                    {
                        shootSound[i].Play();
                    }
                }
            }
            else
            {
                MuzzleFlashPrefab.SetActive(false);
                for (int i = 0; i < shootSound.Length; i++)
                {
                    shootSound[i].Stop();
                }
            }
        }
        else
        { 
            if(Input.GetMouseButton(0) &&Time.timeScale==1)
            {
                MuzzleFlashPrefab.SetActive(true);
                for (int i = 0; i < shootSound.Length; i++)
                {
                    if (shootSound[i].isPlaying == false)
                    {
                        shootSound[i].Play();
                    }
                }
            }
            else
            {
                MuzzleFlashPrefab.SetActive(false);
                for (int i = 0; i < shootSound.Length; i++)
                {
                    shootSound[i].Stop();
                }
            }
        }
    }

    public void PlayerDamage(float damage)
    {
        Health -= damage;
        _uim.SetHealthValue(Health);
    }

    public void PlayerScore(int s)
    {
        score += s;
        _uim.UpdatePlayerScore(score);
        enemiesDestroid++;
    }

    IEnumerator ScoreIncrement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            score++;
            _uim.UpdatePlayerScore(score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Astroid"))
        {
            PlayerDestroy();
        }
    }

    public void HealthGen()
    {
        Health += Random.Range(5f, 10f);
        if(Health>maxHealth)
        {
            Health = maxHealth;
        }
        _uim.SetHealthValue(Health);
    }

    public void setHealthAfterReward(int chance)
    {
        float _health;
        switch (chance)
        {
            case 4: _health = maxHealth; break;
            case 3: _health = maxHealth * 0.75f; break;
            case 2: _health = maxHealth * 0.5f; break;
            case 1: _health = maxHealth * 0.25f; break;
            case 0: return;
            default: _health = maxHealth; break;
        }
        Health = _health;
        _uim.SetHealthValue(Health);
    }
}
