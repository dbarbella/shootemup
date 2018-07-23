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
    public float waveWait;

    public Text scoreText;
    private int score;

    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;

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
        Debug.Log(wave);
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

            if (gameOver)
            {
                restartText.text = "Press 'r' to restart";
                restart = true;
                break;
            }
        }
    }

    public void Start()
    {
        Spawn[] testWaveSchedule = { new Spawn(new int[] { 0, 1, 2 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(0, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(-4, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(4, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 3 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 4 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 5 }, new Vector3(0, 0, 16), Quaternion.identity, 1)};
        Wave testWave = new Wave(testWaveSchedule, 12, 1);

        Spawn[] veeWaveSchedule = { new Spawn(new int[] { 0, 1, 2 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(-2, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(2, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(-4, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(4, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(6, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(-6, 0, 16), Quaternion.identity, 1)};
        Wave veeWave = new Wave(veeWaveSchedule, 6, 1);

        //Wave[] 
        // This code is horrible, but it's just for testing.
        allWaveTypes[0] = testWave;
        allWaveTypes[1] = veeWave;

        StartCoroutine(SpawnWaves());
    }
}