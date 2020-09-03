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
    public bool enableSubstractDelay = true;
    public float delaySubstractDelay = 0.2f;
    private float auxDelaySubstractDelay;

    public float minValueMinDelayGeneration = 1;
    public float minValueMaxDelayGeneration = 3;
    public float maxRandX;
    public float minRandX;

    public float maxRandY;
    public float minRandY;
    // Update is called once per frame
    private void Start()
    {
        auxDelaySubstractDelay = delaySubstractDelay;
        if (startGeneration)
        {
            SpawnCoins();
        }
        delayGeneration = Random.Range(minDelayGeneration, maxDelayGeneration);
    }
    void Update()
    {
        if (enableGeneration)
        {
            CheckDelayGenerationCoin();
            if(enableSubstractDelay)
                CheckSubstractRandomDelay();
        }
    }
    public void CheckSubstractRandomDelay()
    {
        if (delaySubstractDelay > 0)
        {
            delaySubstractDelay = delaySubstractDelay - Time.deltaTime;
        }
        else
        {
            delaySubstractDelay = auxDelaySubstractDelay;
            SubstractRandomDelay();
        }
    }

    public void SubstractRandomDelay()
    {
        if (minDelayGeneration > minValueMinDelayGeneration)
        {
            minDelayGeneration = minDelayGeneration - Time.deltaTime;
        }
        if (maxDelayGeneration > minValueMaxDelayGeneration)
        {
            maxDelayGeneration = maxDelayGeneration - Time.deltaTime;
        }
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
