using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    [Header("Enemies and Costs")]
    public GameObject[] enemys = new GameObject[4];
    public int[] costs = new int[4];

    [Header("Level Stats")]
    public int availbleCreds = 100;
    public bool levelOver = false;
    public int stage = 1;

    [Header("Enemy Spawning")]
    public bool canSpawn = false;
    public int enemyType;
    public int quantity;
    public float ttnw = 5; // time to next wave

    public Transform[] spwanPos = new Transform[7];

    private bool isWaveActive = false;

    private void Start()
    {
        stage = 4;
        availbleCreds = Mathf.RoundToInt(availbleCreds * stage / 3f); // Use 3f to force floating-point division
        Invoke("StartLevel", 4);
    }

    private void StartLevel()
    {
        canSpawn = true;
        enemyType = Random.Range(0, enemys.Length);
        quantity = Random.Range(1, 4 * (stage / 2));
    }

    private void Update()
    {
        if (!levelOver && canSpawn && !isWaveActive)
        {
            StartCoroutine(StartWave());
        }
    }

    private IEnumerator StartWave()
    {
        isWaveActive = true;
        canSpawn = false;
        print("Started wave");

        for (int s = 0; s < quantity; s++)
        {
            // Randomize enemyType for each spawn
            enemyType = Random.Range(0, enemys.Length);
            Transform spawnLoc = spwanPos[Random.Range(0, spwanPos.Length)];
            print("Chose enemy, quantity, and spawn pos");

            if (costs[enemyType] <= availbleCreds)
            {
                Instantiate(enemys[enemyType], spawnLoc.position, spawnLoc.rotation);
                availbleCreds -= costs[enemyType];
                print("Spawned chosen enemy");
            }
            else
            {
                bool spawned = false;
                for (int i = enemys.Length - 1; i >= 0; i--)
                {
                    if (costs[i] <= availbleCreds)
                    {
                        Instantiate(enemys[i], spawnLoc.position, spawnLoc.rotation);
                        availbleCreds -= costs[i];
                        spawned = true;
                        print("Found and spawned affordable enemy");
                        break;
                    }
                }

                if (!spawned)
                {
                    print("Not enough credits left, level over");
                    levelOver = true;
                    break;
                }
            }

            float ttns = Random.Range(0.3f, 4 / stage);
            print("Chose time to next spawn");
            yield return new WaitForSeconds(ttns);
        }

        yield return new WaitForSeconds(ttnw);
        canSpawn = true;
        isWaveActive = false;
    }
}