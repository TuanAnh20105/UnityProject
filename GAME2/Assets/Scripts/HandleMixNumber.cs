﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMixNumber 
{
    public void HandleMix(Number number, PlayerController player)
    {
        if (ManagerNumber.instance.list.Count > 1)
        {
            ManagerNumber.instance.CheckNumber(number);
        }
        else
        {
            player.handlePush.spawnNumber = true;
            player.SetState(PlayerController.State.spawnNumber);
        }
    }
}