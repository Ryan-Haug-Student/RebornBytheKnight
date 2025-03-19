using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    [Header("Enemies and Costs")]
    public GameObject[] enemys = new GameObject[4];
    public int[] costs = new int[4];

    [Header("Level Stats")]
    public int availbleCreds = 50;
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
        Invoke("StartLevel", 4);
    }

    private void StartLevel()
    {
        stage = PlayerController.instance.stage;
        availbleCreds = Mathf.RoundToInt(availbleCreds * stage / 4f); // Use 3f to force floating-point division

        canSpawn = true;
        enemyType = Random.Range(0, enemys.Length);
    }

    private void Update()
    {
        if (!levelOver && canSpawn && !isWaveActive)
            StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        isWaveActive = true;
        canSpawn = false;
        print("Started wave");
        quantity = Random.Range(1, 4 * (stage / 3));

        for (int s = 0; s < quantity; s++)
        {
            // Randomize enemyType for each spawn
            enemyType = Random.Range(0, enemys.Length);
            Transform spawnLoc = spwanPos[Random.Range(0, spwanPos.Length)];

            if (costs[enemyType] <= availbleCreds)
            {
                Instantiate(enemys[enemyType], spawnLoc.position, spawnLoc.rotation);
                availbleCreds -= costs[enemyType];
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
                        break;
                    }
                }

                if (!spawned)
                {
                    print("Not enough credits left, level over");
                    PlayerController.instance.stageOver = true;
                    break;
                }
            }

            float ttns = Random.Range(0.3f, 3 / stage * .5f);
            yield return new WaitForSeconds(ttns);
        }

        yield return new WaitForSeconds(ttnw);
        canSpawn = true;
        isWaveActive = false;
    }
}