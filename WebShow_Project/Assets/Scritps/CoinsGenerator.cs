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
    public int countTriedGeneration = 10;
    private int auxCountTriedGeneration;
    public enum TypeGeneration
    {
        RandomPosition,
        RandomTransformList,
    }
    public TypeGeneration typeGeneration;
    public List<IEAviableObject> listPosition;

    public float minValueMinDelayGeneration = 1;
    public float minValueMaxDelayGeneration = 3;
    public float maxRandX;
    public float minRandX;

    public float maxRandY;
    public float minRandY;
    // Update is called once per frame
    private void Start()
    {
        auxCountTriedGeneration = countTriedGeneration;
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
        float x = -1;
        float y = -1;
        
        GameObject go = null;
        switch (typeGeneration) {
            case TypeGeneration.RandomPosition:
                x = Random.Range(minRandX, maxRandX);
                y = Random.Range(minRandY, maxRandY);

                go = poolCoin.GetObject();
                if (go == null) return;
                go.transform.position = new Vector3(x, y, 0);
                break;
            case TypeGeneration.RandomTransformList:
                bool generationDone = false;
                int indexRandom = Random.Range(0, listPosition.Count);
                while (countTriedGeneration > 0 && !generationDone)
                {
                    if (listPosition[indexRandom] != null)
                    {
                        if (listPosition[indexRandom].aviable)
                        {
                            go = poolCoin.GetObject();
                            x = listPosition[indexRandom].transform.position.x;
                            y = listPosition[indexRandom].transform.position.y;
                            if (go == null) return;
                            go.transform.position = new Vector3(x, y, 0);
                            generationDone = true;
                        }
                    }
                    countTriedGeneration--;
                }
                if (!generationDone)
                {
                    for (int i = 0; i < listPosition.Count; i++)
                    {
                        if (listPosition[i] != null)
                        {
                            if (listPosition[i].aviable)
                            {
                                go = poolCoin.GetObject();
                                x = listPosition[i].transform.position.x;
                                y = listPosition[i].transform.position.y;
                                if (go == null) return;
                                go.transform.position = new Vector3(x, y, 0);
                                generationDone = true;
                                i = listPosition.Count;
                            }
                        }
                    }
                }
                if (!generationDone)
                {
                    enableGeneration = false;
                }
                countTriedGeneration = auxCountTriedGeneration;
                break;
        }
    }

}
