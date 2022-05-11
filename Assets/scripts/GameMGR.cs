using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMGR : MonoBehaviour
{
    bool isCupInProgress = false;
    private Drink drinkInProgress;
    [SerializeField] GameObject NewDrinkPrefab;
    [SerializeField] Mixer mixer;
    public enum DrinkBase
    {
        None,
        Carrot,
        Peanut,
        Tomato,
    }

    public struct Drink
    {
        public DrinkBase drinkBase;

        public Drink(DrinkBase drinkBase)
        {
            this.drinkBase = drinkBase;
        }
    }

    public void getNewCup()
    {
        if (!isCupInProgress)
        {
            isCupInProgress = true;

            Cup cup = Instantiate(NewDrinkPrefab, mixer.CupPlace.transform).GetComponent<Cup>(); ;
            drinkInProgress = cup.Drink;
        }
    }

    public void StartLevel()
    {

    }
    
}
