using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance {
        get 
        {
            if(_instance == null)
                _instance = new GameManager();

            return _instance;
        } 
    }

    private GameData gameData;

    public Action<float> onMainCurrencyChanged;
    public Action<int> onEnergySpended;

    private GameManager()
    {
        gameData = new GameData();
        gameData.maxEnergyAmount = 10;
        gameData.energyAmount = gameData.maxEnergyAmount;
    }

    public int GetMaxEnergy()
    {
        return gameData.maxEnergyAmount;
    }

    public void ChangeMainCurrency(float delta)
    {
        gameData.mainCurrency += delta;

        onMainCurrencyChanged?.Invoke(gameData.mainCurrency);
    }

    public void SpendEnergy(int delta)
    {
        if (gameData.energyAmount == 0 || gameData.energyAmount - delta < 0) return;
        gameData.energyAmount -= delta;

        onEnergySpended?.Invoke(gameData.energyAmount);
    }

    public bool HasEnergy()
    {
        return gameData.energyAmount > 0;
    }
}
