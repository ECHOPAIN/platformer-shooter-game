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

        int spawnPointsIterator = 0;
        for (int i = 0; i < playerAmount; i++)
        {
            if (i == 0)
            {
                //spawn a humanPlayer
                Instantiate(humanPlayer, spawnPoints[spawnPointsIterator].position, Quaternion.identity);
            }
            else
            {
                //spawn a aiPlayer
                Instantiate(aiPlayer, spawnPoints[spawnPointsIterator].position, Quaternion.identity);
            }
            spawnPointsIterator++;
            if(spawnPointsIterator  >= spawnPoints.Length)
            {
                spawnPointsIterator = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
