using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerShips;

    // always use Awake here
    void Awake()
    {
        int index = PlayerPrefs.GetInt("PlayerSpaceship");
        Instantiate(PlayerShips[index]);
    }
}
