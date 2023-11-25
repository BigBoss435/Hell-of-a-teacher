using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter : MonoBehaviour
{
    public int currentRage = 0;
    public int rageCap = 100;
    public int oldRageCap = 100;
    
    public void AddRage(int amount)
    {
        currentRage += amount;
        if (currentRage >= rageCap)
        {
            currentRage = rageCap;
            oldRageCap = rageCap;
            IncreaseRageCap();
        }
    }

    private void Update()
    {
        if (currentRage > 0)
        {
            DropRage();
        }
    }

    public void DropRage()
    {
        if (currentRage > 0)
        {
            currentRage -= (int)Time.deltaTime;
            if (currentRage <= 0)
            {
                currentRage = 0;
            }
        }
    }
    
    public void IncreaseRageCap()
    {
        rageCap *= 2;
    }
}
