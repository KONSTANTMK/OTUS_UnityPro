using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public class MoneySaveLoader : ISaveLoader
    {
        void ISaveLoader.SaveGame(GameContext gameContext)
        {
            var moneyStorage = gameContext.MoneyStorage;
            PlayerPrefs.SetInt("Lesson/Money", moneyStorage.Money);
            Debug.Log($"Money saved: {moneyStorage.Money}");
        }

        void ISaveLoader.LoadGame(GameContext gameContext)
        {
            var moneyStorage = gameContext.MoneyStorage;
            var value = PlayerPrefs.GetInt("Lesson/Money");
            moneyStorage.SetupMoney(value);
            Debug.Log($"Money loaded: {value}");
        }
    }
}