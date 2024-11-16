using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<NPC> Spawnables;

    [SerializeField]
    List<NPC> Wave = new List<NPC>();

    [SerializeField]
    Survivor survivor;

    [SerializeField]
    GameObject EnemyFolder;

    float randX;

    float randY;

    Vector2 SpawnLocation;

    float nextSpawn = 0;

    int SpawnWaveLimitor = 1;

    [SerializeField]
    int[] SpawnWaveLimitorIncrease;

    enum SpawnState
    {
        Unloaded,
        Loading,
        Loaded,
        Finished,
    }

    [SerializeField]
    SpawnState state = SpawnState.Unloaded;

    private void Awake()
    {
        GameManager.onGameStateChange += GameManagerOnStateChange;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameManagerOnStateChange;
    }

    private void GameManagerOnStateChange(GameManager.GameState Gamestate)
    {
        if(Gamestate == GameManager.GameState.SetUp)
        {
            int Points = 15 + GradIncrease();
            Debug.Log(Points);
            SpawnListSetUp(Points);

            state = SpawnState.Loaded;
        }
    }

    private int GradIncrease()
    {
        return System.Convert.ToInt32(Mathf.Floor(2.0f*Mathf.Sqrt(GameManager.Instance.Round)));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.Loaded)
        {

            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + 1.4f-Mathf.Clamp((GameManager.Instance.Round*.05f), .5f, 999);
                randX = Random.Range(-20, 20);
                randY = Random.Range(0, 10);
                if (Wave[0].tag == "Survivor") randY -= 5;
                SpawnLocation = new Vector2(randX, gameObject.transform.position.y + randY);
                NPC SpawnedBeing = Instantiate(Wave[0], SpawnLocation, Quaternion.identity);
                SpawnedBeing.transform.parent = EnemyFolder.gameObject.transform;
                Wave.Remove(Wave[0]);
            }
            if (Wave.Count == 0) 
            { 
                state = SpawnState.Finished;
                Debug.Log("Wave finished");
                GameManager.Instance.UpdateGameState(GameManager.GameState.End);
            }
            
        }
        
    }

    void SpawnListSetUp(float PointsToWave)
    { 
        state = SpawnState.Loading;
        
        float SpawnWeightTotal = 0;
        float PointsLeft = PointsToWave;

        List<float> Chances = new List<float>();
        for (int i = 0; i < SpawnWaveLimitorIncrease.Length; i++)
            {
            if (GameManager.Instance.Round == SpawnWaveLimitorIncrease[i])
                {
                    SpawnWaveLimitor++;
                    break;
                }
            }


        //Get a total weight
        for (int i = 0; i < SpawnWaveLimitor; i++)
        {
            SpawnWeightTotal += Spawnables[i].SpawnWeight;
            Chances.Add(SpawnWeightTotal);
        }
        
        //Put enemies in next spawn based on spawn weight.
        while (PointsLeft >= 1)
        {
            float Overflow = 0;
            float RanChance = Random.Range(0.0f, 1.0f);
            foreach (Enemy Spawn in Spawnables)
            {
                if (RanChance < (Spawn.SpawnWeight/SpawnWeightTotal)+Overflow)
                {
                    Wave.Add(Spawn);
                    PointsLeft -= Spawn.SpawnPoint;
                    break;
                } else
                {
                    Overflow = Overflow + (Spawn.SpawnWeight / SpawnWeightTotal);
                }
            }

            
        }
        for (int i = 0; i < 3;)
            {
                int ranPos = Random.Range(0, Wave.Count);
                if (Wave[ranPos].tag != "survivor")
                {
                    Wave.Insert(ranPos, survivor);
                    i++;
                }
            }
    }
}
