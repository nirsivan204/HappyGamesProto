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
    int shakesServed = 0;
    [SerializeField] GameObject[] ingrediantsPrefabsDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    [SerializeField] GameObject[] addOnPrefabsDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    [SerializeField] GameObject workingStation;
    [SerializeField] ButtonMGR buttonMGR;
    Cup cupInProgress;
    [SerializeField] MusicMGR musicMGR;
    [SerializeField] GameObject requestPosition;
    Cup cupInDisplay = null;
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


    [Serializable]
    public class Drink
    {
        public DrinkBase drinkBase = DrinkBase.NONE;
        public AddOn addOn = AddOn.NONE;
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

        if(shakesServed < requests.Count)
        {
            Drink required = requests[shakesServed];
            shakesServed++;
            if (drinkInProgress.isEqual(required))
            {
                print("success");
                musicMGR.Play_Sound(MusicMGR.SoundTypes.SUCCESS);
                Destroy(cupInProgress.gameObject);
            }
            else
            {
                print("fail");
                musicMGR.Play_Sound(MusicMGR.SoundTypes.FAIL);
                Destroy(cupInProgress.gameObject);
            }
            buttonMGR.showIngrediants();
            isCupInProgress = false;
            if (shakesServed == requests.Count)
            {
                EndLevel();
            }
            else
            {
                ShowRequest(requests[shakesServed]);
            }
        }
    }
    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        ShowRequest(requests[0]);
    }

    public void EndLevel()
    {

    }

    void ShowRequest(Drink request)
    {
        if(cupInDisplay != null)
        {
            Destroy(cupInDisplay.gameObject);
        }
        cupInDisplay = Instantiate(NewDrinkPrefab, requestPosition.transform).GetComponent<Cup>();
        cupInDisplay.init(this);
        cupInDisplay.fillCupWithBase(request.drinkBase);
        cupInDisplay.putAddOn(addOnPrefabsDictionary[(int)request.addOn],request.addOn);
        cupInDisplay.State = Cup.CupState.ON_DISPLAY;
    }
    
}
