using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform laserBeamHolder;
    public LineRenderer laserline;
    public Transform laserEndpoint;
    public GameObject hitMarkerPrefab;
    private PlayerCombatController pcc;
    public GameObject blastPrefab;
    public int ScoreToPlayer = 15;
    private float Health;
    public float damageToPlayer = 1;
    private bool canFire = false;
    private AudioSource _laserBeamAS;
    private bool laserBeamPlayin = false;
    private GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        pcc = GameObject.FindWithTag("Player").GetComponent<PlayerCombatController>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        laserline.useWorldSpace = true;
        Health = 200;
        StartCoroutine(FireRoutine());
        _laserBeamAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire&&Time.timeScale==1)
        {
            laserline.enabled = true;
            if(!laserBeamPlayin)
            {
                _laserBeamAS.Play();
                laserBeamPlayin = true;
            }
            Fire();
        }
        else
        {
            laserline.enabled = false;
            _laserBeamAS.Stop();
            laserBeamPlayin = false;
        }
        if(_gm.IsGameOver())
        {
            _laserBeamAS.Stop();
        }
    }
    private void Fire()
    {
        if (Physics.Raycast(laserBeamHolder.position, laserBeamHolder.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 500))
        {
            laserline.SetPosition(0, laserBeamHolder.position);
            laserline.SetPosition(1, hitInfo.point);
            if (hitInfo.transform.CompareTag("Player"))
            {
                GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                hitInfo.transform.GetComponent<PlayerCombatController>().PlayerDamage(damageToPlayer);
            }
            if(hitInfo.transform.CompareTag("Astroid"))
            {
                GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            }
        }
        else
        {
            laserline.SetPosition(0, laserBeamHolder.position);
            laserline.SetPosition(1, laserEndpoint.position);
        }
        if (Health < 0)
        {
            EnemyDeath();
        }
    }

    IEnumerator FireRoutine()
    {
        while(!_gm.IsGameOver())
        {
            yield return new WaitForSeconds(Random.Range(2,5));
            canFire = true;
            yield return new WaitForSeconds(Random.Range(3,9));
            canFire = false;
        }
    }
    public void EnemyDamage(float damageRate)
    {
        Health -= damageRate;
    }

    private void EnemyDeath()
    {
        pcc.PlayerScore(ScoreToPlayer);
        GameObject BlastPrefab = Instantiate(blastPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
