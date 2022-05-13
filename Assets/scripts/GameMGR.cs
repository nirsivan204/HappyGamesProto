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
    [SerializeField] GameObject workingStation;
    Cup cupInProgress;
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

            cupInProgress = Instantiate(NewDrinkPrefab, mixer.CupPlace.transform).GetComponent<Cup>();
            cupInProgress.init(this);
            drinkInProgress = cupInProgress.Drink;
            mixer.PutCupInMixer(cupInProgress);
        }
    }

    internal void PutCupInWorkStation()
    {
        cupInProgress.transform.position = workingStation.transform.position;
        mixer.takeCupOut();
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
