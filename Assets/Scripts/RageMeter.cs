using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter : MonoBehaviour
{
    public int currentRage = 0;
    public int rageCap = 100;
    public int ragePerKill = 10;
    public int ragePerHit = 1;
    public int rageDropPerSecond = 1;
    public float rageDropDelay = 1f;
    
    
    public void AddRage(int amount)
    {
        currentRage += amount;
        if (currentRage >= rageCap)
        {
            currentRage = rageCap;
            IncreaseRageCap();
        }
    }
    
    public void DropRage()
    {
        if (currentRage > 0)
        {
            currentRage -= rageDropPerSecond;
            if (currentRage <= 0)
            {
                currentRage = 0;
            }
        }
    }
    
    public void IncreaseRageCap()
    {
        rageCap *= 10;
    }
}
