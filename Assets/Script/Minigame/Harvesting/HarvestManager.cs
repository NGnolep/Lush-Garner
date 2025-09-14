using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public int amountPlant = 3;
    public TimerMechanics timerMechanics;

    public void OnPlantHarvested()
    {
        amountPlant--;

        if (amountPlant <= 0)
        {
            MinigameInfo.minigameSuccess = true;
            timerMechanics.MinigameOutcome("Harvesting");
        }
    }
}
