using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMGR : MonoBehaviour
{
    bool isCupInProgress = false;
    private Drink drinkInProgress;
    [SerializeField] GameObject[] CupsPrefabDisctionary;
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
    [SerializeField] CamerasMGR cameraMGR;
    Cup cupInDisplay = null;
    private int[] currentIngredients;
    private int[] currentAddons;
    [SerializeField] LiquidUtility LU;
    [SerializeField] private Animator horse;


    public enum DrinkBase
    {
        CARROT,
        BANANAS,
        MEAT,
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
        public CupsDispanser.CupType cupType;
        public bool isEqual(Drink required)
        {
            if(required.cupType == this.cupType)
            {
                if (required.drinkBase == this.drinkBase)
                {
                    if (required.addOn == this.addOn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

    public void getNewCup(int cupType)
    {
        if (!isCupInProgress)
        {
            isCupInProgress = true;

            cupInProgress = Instantiate(CupsPrefabDisctionary[cupType], mixer.CupPlace.transform).GetComponent<Cup>();
            cupInProgress.init(this, LU, cupType);
            drinkInProgress = cupInProgress.Drink;
            mixer.PutCupInMixer(cupInProgress);
            mixer.CanBeUsed = true;
            cameraMGR.ChangeState(CamerasMGR.CamerasStates.MIXER);
        }
    }

    internal void PutCupInWorkStation()
    {
        currentAddons = new int[3] { (int)AddOn.ICE_CUBES, (int)AddOn.ICE_CUBES, (int)AddOn.ICE_CUBES };
        buttonMGR.UpdateButtons(ButtonMGR.SpriteGroups.ADDONS, currentAddons);
        buttonMGR.showManu(true);
        cupInProgress.transform.position = workingStation.transform.position;
        mixer.takeCupOut();
        mixer.CanBeUsed = false;
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.ADDON_STATION);
    }

    public void putInsideMixer(int i)
    {
        DrinkBase drinkBase = (DrinkBase) currentIngredients[i];
        mixer.putInsideMixer(ingrediantsPrefabsDictionary[currentIngredients[i]], drinkBase);
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.CUPS);
        buttonMGR.showManu(false);
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
                horse.SetBool("Success1" , true);
                musicMGR.Play_Sound(MusicMGR.SoundTypes.SUCCESS);
                Destroy(cupInProgress.gameObject);
                
            }
            else
            {
                print("fail");
                horse.SetBool("Failure1" , true);
                musicMGR.Play_Sound(MusicMGR.SoundTypes.FAIL);
                Destroy(cupInProgress.gameObject);
            }
            cameraMGR.ChangeState(CamerasMGR.CamerasStates.CHARACTER);
            buttonMGR.showManu(true);
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

    public void StartLevel()
    {
        print("start");
        ShowRequest(requests[0]);
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.INGREDIANTS);
        currentIngredients = new int[3] { (int)DrinkBase.CARROT, (int)DrinkBase.MEAT, (int)DrinkBase.BANANAS };
        buttonMGR.UpdateButtons(ButtonMGR.SpriteGroups.INGREDIANTS, currentIngredients);
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
        cupInDisplay = Instantiate(CupsPrefabDisctionary[(int)request.cupType], requestPosition.transform).GetComponent<Cup>();
        cupInDisplay.init(this, LU, (int)request.cupType);
        cupInDisplay.fillCupWithBase(request.drinkBase);
        cupInDisplay.putAddOn(addOnPrefabsDictionary[(int)request.addOn],request.addOn);
        cupInDisplay.State = Cup.CupState.ON_DISPLAY;
    }
    
}
