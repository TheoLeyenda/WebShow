using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentCoin;
    public int countGeneralCoin;
    public int numberPlayerInventory;
    void Start()
    {
        name = "InventoryPlayer" + numberPlayerInventory;
    }
}
