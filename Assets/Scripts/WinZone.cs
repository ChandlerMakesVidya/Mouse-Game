﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.cheeseCount <= 0 && other.CompareTag("Player"))
        {
            GameManager.GM.gameState = GameManager.GameState.Win;
        }
    }
}
