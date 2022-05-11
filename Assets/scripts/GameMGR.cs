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
    [SerializeField] List<Drink> requests;
    [SerializeField] GameObject[] ingrediantsPrefabsDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    public enum DrinkBase
    {
        Carrot,
        Peanut,
        Tomato,
        None
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
            mixer.IsCupInMixer = true;
            mixer.CupInMixer = cup;
        }
    }

    public void putInsideMixer(DrinkBase drinkBase)
    {
        mixer.putInsideMixer(ingrediantsPrefabsDictionary[(int)drinkBase], drinkBase);
    }

    public void serveDrink()
    {
        Drink required = requests[0];
        if(required.drinkBase == drinkInProgress.drinkBase)
        {
            print("success");
        }
        else
        {
            print("fail");
        }
    }

    public void StartLevel()
    {

    }
    
}
