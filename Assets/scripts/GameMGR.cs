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
    [SerializeField] GameObject[] addOnPrefabsDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    [SerializeField] GameObject workingStation;
    [SerializeField] ButtonMGR buttonMGR;
    Cup cupInProgress;
    public enum DrinkBase
    {
        CARROT,
        PEANUT,
        TOMATO,
        NONE
    }

    public enum AddOn
    {
        ICE_CUBES,
        STRAW,
        NONE
    }



    public class Drink
    {
        public DrinkBase drinkBase;
        public AddOn addOn;
        public bool isEqual(Drink required)
        {
            if (required.drinkBase == this.drinkBase)
            {
                if(required.addOn == this.addOn)
                {
                    return true;
                }
            }
            return false;
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
        buttonMGR.showAddOns();
        cupInProgress.transform.position = workingStation.transform.position;
        mixer.takeCupOut();
    }

    public void putInsideMixer(DrinkBase drinkBase)
    {
        mixer.putInsideMixer(ingrediantsPrefabsDictionary[(int)drinkBase], drinkBase);
    }


    internal void putAddOnInCup(AddOn addOn)
    {
        cupInProgress.putAddOn(addOnPrefabsDictionary[(int)addOn], addOn);
    }

    public void serveDrink()
    {
        Drink required = requests[0];
        if(drinkInProgress.isEqual(required))
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
