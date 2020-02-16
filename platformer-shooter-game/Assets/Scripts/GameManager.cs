using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public GameObject humanPlayer;
    [SerializeField] public GameObject aiPlayer;
    [Range(1, 5)] [SerializeField] public int playerAmount;
    [SerializeField] public bool team;
    [SerializeField] Transform[] spawnPoints;

    private int spawnPointsIterator = 0;

    public static GameManager instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < playerAmount; i++)
        {
            Transform spawnPoint = GetNextSpawnPoint();
            if (i == 0)
            {
                //spawn a humanPlayer
                Instantiate(humanPlayer, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                //spawn a aiPlayer
                Instantiate(aiPlayer, spawnPoint.position, Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetNextSpawnPoint()
    {
        Transform res = spawnPoints[spawnPointsIterator];
        spawnPointsIterator++;
        if (spawnPointsIterator >= spawnPoints.Length)
        {
            spawnPointsIterator = 0;
        }
        return res;
    }
}
