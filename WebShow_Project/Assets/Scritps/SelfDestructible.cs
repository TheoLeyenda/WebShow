using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructible : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeDestruction
    {
        SetActive,
        Destroy,
        None,
    }
    public TypeDestruction typeDestruction = TypeDestruction.SetActive;
    public float delayDestroy;
    private float auxDelayDestroy;
    void Start()
    {
        auxDelayDestroy = delayDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (typeDestruction != TypeDestruction.None)
        {
            CheckDelayDestroy();
        }
    }
    public void CheckDelayDestroy()
    {
        if (delayDestroy > 0)
        {
            delayDestroy = delayDestroy - Time.deltaTime;
        }
        else
        {
            delayDestroy = auxDelayDestroy;
            switch (typeDestruction)
            {
                case TypeDestruction.Destroy:
                    Destroy(gameObject);
                    break;
                case TypeDestruction.SetActive:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
