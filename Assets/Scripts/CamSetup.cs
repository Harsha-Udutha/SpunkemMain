using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamSetup : MonoBehaviour
{
    public Transform[] CamRefs;
    public float speed = 20f;
    private int min, max;
    [SerializeField] private int currentref;
    private SpaceshipContainer sc;
    private Vector3 newpos;

    public GameObject ChoosePanel, shipDetailsPanel, loadPanel;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        sc = GameObject.Find("container").GetComponent<SpaceshipContainer>();
        if(!PlayerPrefs.HasKey("DefaultCamPos"))
        {
            PlayerPrefs.SetInt("DefaultCamPos", 0);
        }
        min = 0;
        max = CamRefs.Length;
        currentref = PlayerPrefs.GetInt("DefaultCamPos");
        newpos= CamRefs[currentref].transform.position;
        transform.position = newpos;
        sc.UpdateRotation(currentref);

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, newpos, speed * Time.deltaTime);
    }

    public void ChangeCamPosToRight()
    {
        currentref++;
        if(currentref<max)
        {
            newpos = CamRefs[currentref].position;
            sc.UpdateRotation(currentref);
        }
        else
        {
            transform.position= CamRefs[min].position;
            newpos= CamRefs[min].position;
            currentref = min;
            sc.UpdateRotation(currentref);
        }
    }

    public void ChangeCamPosToLeft()
    {
        currentref--;
        if (currentref >= min)
        {
            newpos = CamRefs[currentref].position;
            sc.UpdateRotation(currentref);
        }
        else
        {
            transform.position = CamRefs[13].position;
            newpos = CamRefs[13].position;
            currentref = 13;
            sc.UpdateRotation(currentref);
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("PlayerSpaceship", currentref);
        PlayerPrefs.SetInt("DefaultCamPos", currentref);
        ChoosePanel.SetActive(false);
        shipDetailsPanel.SetActive(false);
        loadPanel.SetActive(true);
        StartCoroutine(LoadScreen());
    }

    IEnumerator LoadScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }

    public int GetCurrentRef()
    {
        return currentref;
    }
}
