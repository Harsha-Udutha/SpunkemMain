using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private PlayerCombatController pcc;
    public EnemySpawner espawner;
    int scoreThresehold = 200;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            pcc = GameObject.FindWithTag("Player").GetComponent<PlayerCombatController>();
        }
        catch(System.Exception e)
        {
            print(e.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (pcc.score >= scoreThresehold)
            {
                espawner.maxBound += 1;
                espawner.eSpeed += 5;
                espawner.lSpeed += 5;
                espawner.maxSpawnTime -= .5f;
                scoreThresehold += Random.Range(300, 800);
            }
        }
        catch(System.Exception e)
        {
            print(e.ToString());
        }
    }
}