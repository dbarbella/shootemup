using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waves : MonoBehaviour
{

    public GameObject[] hazards;
    Random rnd = new Random();

    public class Wave
    {
        public Spawn[] waveSchedule;
        public float startWait;
        public int complexity;

        public Wave(Spawn[] newWaveSchedule, int newComplexity)
        {
            waveSchedule = newWaveSchedule;
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

    public void Start()
    {
        Spawn[] testWaveSchedule = { new Spawn(new int[] { 0, 1, 2 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(0, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(-4, 0, 16), Quaternion.identity, 0),
                                     new Spawn(new int[] { 0, 1, 2 }, new Vector3(4, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 3 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 4 }, new Vector3(0, 0, 16), Quaternion.identity, 1),
                                     new Spawn(new int[] { 5 }, new Vector3(0, 0, 16), Quaternion.identity, 1)};
        Wave testWave = new Wave(testWaveSchedule, 1);
        StartCoroutine(SpawnWave(testWave));
    }
}