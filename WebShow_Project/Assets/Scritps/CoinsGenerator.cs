using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxCountCoinGeneration;
    public float minDelayGeneration;
    public float maxDelayGeneration;
    private float delayGeneration;
    public bool startGeneration;
    public Pool poolCoin;
    public bool enableGeneration = true;

    public float maxRandX;
    public float minRandX;

    public float maxRandY;
    public float minRandY;
    // Update is called once per frame
    private void Start()
    {
        if (startGeneration)
        {
            SpawnCoins();
        }
        delayGeneration = Random.Range(minDelayGeneration, maxDelayGeneration);
    }
    void Update()
    {
        if(enableGeneration)
            CheckDelayGenerationCoin();
    }
    public void CheckDelayGenerationCoin()
    {
        if (delayGeneration > 0)
        {
            delayGeneration = delayGeneration - Time.deltaTime;
        }
        else
        {
            delayGeneration = Random.Range(minDelayGeneration, maxDelayGeneration);
            GenerationCoin();
        }
    }
    void SpawnCoins()
    {
        for (int i = 0; i < maxCountCoinGeneration; i++)
        {
            GenerationCoin();
        }
    }
    void GenerationCoin()
    {
        float x = Random.Range(minRandX, maxRandX);
        float y = Random.Range(minRandY, maxRandY);

        GameObject go = poolCoin.GetObject();
        if (go == null) return;
        go.transform.position = new Vector3(x, y, 0);
    }

}
