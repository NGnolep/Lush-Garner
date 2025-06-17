using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpawner : MonoBehaviour
{
    public GameObject[] insectPrefabs;
    public Transform target;
    public int insectCount = 5;
    private Bounds spawnBounds;
    private float spawnInterval = 0.5f;
    public int insectAlive = 5;
    public TimerMechanics timerMechanics;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spawnBounds = sr.bounds;
        insectAlive = insectCount;
        StartCoroutine(SpawnInsectsOverTime());
    }

    void Update()
    {
        if (insectAlive == 0)
        {
            MinigameInfo.minigameSuccess = true;
            timerMechanics.MinigameOutcome("InsectDefend");
        }
    }
    IEnumerator SpawnInsectsOverTime()
    {
        for (int i = 0; i < insectCount; i++)
        {
            SpawnRandomInsect();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomInsect()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(spawnBounds.min.x, spawnBounds.max.x),
            Random.Range(spawnBounds.min.y, spawnBounds.max.y)
        );

        int index = Random.Range(0, insectPrefabs.Length);
        GameObject insect = Instantiate(insectPrefabs[index], spawnPos, Quaternion.identity);
        InsectAI ai = insect.GetComponent<InsectAI>();
        ai.SetTarget(target);
        ai.SetSpawner(this);
    }
}
