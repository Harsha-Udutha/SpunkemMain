using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField]
    private float FireRange = 100;
    private Vector3 currentEnemyPos;
    private GameManager _gm;
    public GameObject Missile;
    public Transform LeftLaunch, rightLaunch, missilelook;
    private bool shootLeft = true, shootRight = false;
    public int maxammo=10, ammoCount;
    private bool canLaunch=true;
    private UIManager _uim;

    private void Start()
    {
        _uim = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gm.ShowLaunchButton();
        ammoCount = maxammo;
        _uim.SetAmmo(ammoCount);
        _gm.ShowAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(ammoCount==0)
        {
            canLaunch = false;
        }

        if(ammoCount>maxammo)
        {
            ammoCount = maxammo;
        }

        if(Application.platform==RuntimePlatform.Android)
        {
            return;
        }
        else
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Input.GetMouseButtonDown(1)&& Physics.Raycast(rayOrigin, out hitInfo, FireRange) && canLaunch)
            {
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    currentEnemyPos = hitInfo.transform.position;
                    if (shootLeft)
                    {
                        shootLeft = false;
                        shootRight = true;
                        GameObject PlayerMissile = Instantiate(Missile, LeftLaunch.position, Quaternion.identity) as GameObject;
                        ammoCount--;
                        _uim.SetAmmo(ammoCount);
                    }
                    else
                    {
                        shootLeft = true;
                        shootRight = false;
                        GameObject PlayerMissile = Instantiate(Missile, rightLaunch.position, Quaternion.identity) as GameObject;
                        ammoCount--;
                        _uim.SetAmmo(ammoCount);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1)&& canLaunch)
            {
                currentEnemyPos = missilelook.position;
                if (shootLeft)
                {
                    shootLeft = false;
                    shootRight = true;
                    GameObject PlayerMissile = Instantiate(Missile, LeftLaunch.position, Quaternion.identity) as GameObject;
                    ammoCount--;
                    _uim.SetAmmo(ammoCount);
                }
                else
                {
                    shootLeft = true;
                    shootRight = false;
                    GameObject PlayerMissile = Instantiate(Missile, rightLaunch.position, Quaternion.identity) as GameObject;
                    ammoCount--;
                    _uim.SetAmmo(ammoCount);
                }
            }
        }
    }

    public Vector3 GetEnemycurrentPos()
    {
        return currentEnemyPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name=="Sphere")//change the name later
        {
            Destroy(other.gameObject);
            ammoCount += 2;
            if(canLaunch==false)
            {
                canLaunch = true;
            }
        }
    }

    public void Launch()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, FireRange) && hitInfo.transform.CompareTag("Enemy") && canLaunch)
        {           
            currentEnemyPos = hitInfo.transform.position;
            if (shootLeft)
            {
                shootLeft = false;
                shootRight = true;
                GameObject PlayerMissile = Instantiate(Missile, LeftLaunch.position, Quaternion.identity) as GameObject;
                ammoCount--;
                _uim.SetAmmo(ammoCount);
            }
            else
            {
                shootLeft = true;
                shootRight = false;
                GameObject PlayerMissile = Instantiate(Missile, rightLaunch.position, Quaternion.identity) as GameObject;
                ammoCount--;
                _uim.SetAmmo(ammoCount);
            }    
        }
        else if(canLaunch)
        {
            currentEnemyPos = missilelook.position;
            if (shootLeft)
            {
                shootLeft = false;
                shootRight = true;
                GameObject PlayerMissile = Instantiate(Missile, LeftLaunch.position, Quaternion.identity) as GameObject;
                ammoCount--;
                _uim.SetAmmo(ammoCount);
            }
            else
            {
                shootLeft = true;
                shootRight = false;
                GameObject PlayerMissile = Instantiate(Missile, rightLaunch.position, Quaternion.identity) as GameObject;
                ammoCount--;
                _uim.SetAmmo(ammoCount);
            }
        }
    }

    public void AmmoGen()
    {
        ammoCount += Random.Range(5, 20);
        if (ammoCount > maxammo)
        {
            ammoCount = maxammo;
        }
        _uim.SetAmmo(ammoCount);
    }
}
