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

        public Wave()
        {
            waveSchedule = null;
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
    }

    IEnumerator SpawnWave(Wave wave)
    {
        yield return new WaitForSeconds(wave.startWait);
        Spawn[] waveSchedule = wave.waveSchedule;
        for (int i = 0; i < waveSchedule.Length; i++)
        {
            Spawn nextSpawn = waveSchedule[i];
            // We may eventually want to give these probabilities
            int selectedIndex = Random.Range(0, nextSpawn.possibleHazardIndices.Length);
            GameObject nextHazard = hazards[selectedIndex];
            //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            //Quaternion spawnRotation = Quaternion.identity;
            //Instantiate(hazard, spawnPosition, spawnRotation);
            //Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(spawnAngles.x, spawnAngles.y), 0.0f);
            Instantiate(nextHazard, nextSpawn.position, nextSpawn.rotation);
            yield return new WaitForSeconds(nextSpawn.delay);
        }
    }
}