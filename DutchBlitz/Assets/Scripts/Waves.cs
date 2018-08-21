using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Waves : MonoBehaviour
{

    public GameObject[] hazards;
    Random rnd = new Random();

    public float spawnWait;
    public float startWait;

    private bool gameOver;
    private bool restart;

    public Vector3 randomSpawnValues;
    public Vector2 randomSpawnAngles;

    private GameController gameController;

    public int startDifficulty;
    public int difficultyRamp;

    private Wave[] allWaveTypes = new Wave[2];

    public class Wave
    {
        public Spawn[] waveSchedule;
        public float startWait;
        public float totalTime;
        public int complexity;

        public Wave(Spawn[] newWaveSchedule, float newTotalTime, int newComplexity)
        {
            waveSchedule = newWaveSchedule;
            totalTime = newTotalTime;
            complexity = newComplexity;
        }
    }

    public class Spawn
    {
        public int[] possibleHazardIndices;
        // For now, let's get this working with fixed spawn positions and worry about randomness later.
        public Vector3 position;
        public Quaternion rotation;
        // This is the delay after the hazard is spawned.
        public float delay;

        public Spawn(int[] possibleHIs, Vector3 newPosition, Quaternion newRotation, float newDelay)
        {
            possibleHazardIndices = possibleHIs;
            position = newPosition;
            rotation = newRotation;
            delay = newDelay;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        yield return new WaitForSeconds(wave.startWait);
        Spawn[] waveSchedule = wave.waveSchedule;
        for (int i = 0; i < waveSchedule.Length; i++)
        {
            Spawn nextSpawn = waveSchedule[i];
            // We may eventually want to give these probabilities
            int selectedIndex = nextSpawn.possibleHazardIndices[Random.Range(0, nextSpawn.possibleHazardIndices.Length)];
            GameObject nextHazard = hazards[selectedIndex];
            //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            //Quaternion spawnRotation = Quaternion.identity;
            //Instantiate(hazard, spawnPosition, spawnRotation);
            //Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(spawnAngles.x, spawnAngles.y), 0.0f);
            Instantiate(nextHazard, nextSpawn.position, nextSpawn.rotation);
            yield return new WaitForSeconds(nextSpawn.delay);
        }
    }

    // Spawns a random wave
    IEnumerator SpawnRandomWave(int numHazards, float hazardWait, int[] hazardsToSpawn)
    {
        for (int i = 0; i < numHazards; i++)
        {
            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-randomSpawnValues.x, randomSpawnValues.x), randomSpawnValues.y, randomSpawnValues.z);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(randomSpawnAngles.x, randomSpawnAngles.y), 0.0f);
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(hazardWait);
        }
    }

    // Spawns waves indefinitely
    // Maybe this should choose between random waves and pattered waves?
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            /*
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //Quaternion spawnRotation = Quaternion.identity;
                //Instantiate(hazard, spawnPosition, spawnRotation);
                Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(spawnAngles.x, spawnAngles.y), 0.0f);
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            */
            Wave selectedWave = allWaveTypes[Random.Range(0, allWaveTypes.Length)];
            StartCoroutine(SpawnWave(selectedWave));
            yield return new WaitForSeconds(selectedWave.totalTime);

            if (gameController.gameOver)
            {
                gameController.restartText.text = "Press 'r' to restart";
                gameController.restart = true;
                break;
            }
        }
    }

    // We need to figure out some logic here such that the amount of time we wait scales with the length
    // of the wave.
    IEnumerator SpawnRampingWaves()
    {
        yield return new WaitForSeconds(startWait);
        int difficulty = startDifficulty;
        while (true)
        {
            StartCoroutine(SpawnRandomWaveOfDifficulty(difficulty, 0.5f, new int[] { 0, 1}));
            // This should stop being a magic number and start being something that cares about the difficulty.
            // We could also cramp every wave into this number of seconds, I guess.
            yield return new WaitForSeconds(8);

            difficulty += difficultyRamp;

            if (gameController.gameOver)
            {
                gameController.restartText.text = "Press 'r' to restart";
                gameController.restart = true;
                break;
            }
        }
    }

    // The intent is that this spawns a random wave of a particular difficulty
    IEnumerator SpawnRandomWaveOfDifficulty(int difficulty, float hazardWait, int[] hazardsToSpawn)
    {
        // We want to generate hazards until we've reached the specified difficulty
        int diffGenerated = 0;
        int maxHazard = Mathf.Min(difficulty/5, hazards.Length);
        while (diffGenerated < difficulty)
        {
            int nextHazIndex = Random.Range(0, maxHazard);
            GameObject hazard = hazards[nextHazIndex];
            diffGenerated += nextHazIndex + 1;
            Vector3 spawnPosition = new Vector3(Random.Range(-randomSpawnValues.x, randomSpawnValues.x), randomSpawnValues.y, randomSpawnValues.z);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(randomSpawnAngles.x, randomSpawnAngles.y), 0.0f);
            Instantiate(hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(hazardWait);
        }
    }

    IEnumerator SpawnRandomWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            StartCoroutine(SpawnRandomWave(10, 0.5f, new int[] { 0, 1, 2, 3, 4, 5 }));
            yield return new WaitForSeconds(8);

            if (gameController.gameOver)
            {
                gameController.restartText.text = "Press 'r' to restart";
                gameController.restart = true;
                break;
            }
        }
    }

    public void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

        //StartCoroutine(SpawnWaves());
        //StartCoroutine(SpawnRandomWaves());
        StartCoroutine(SpawnRampingWaves());
    }
}