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
    [SerializeField] GameObject requestShowParent;
    [SerializeField] CamerasMGR cameraMGR;
    Cup cupInDisplay = null;
    private int[] currentIngredients;
    private int[] currentAddons;
    [SerializeField] LiquidUtility LU;
    [SerializeField] Customer[] customers;
    public Mixer Mixer { get => mixer; set => mixer = value; }

    private int totalScore;
    private int levelScore;
    private int totalStars;
    private int levelStars;
    private static int PRICE_OF_SHAKE = 200;

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
            requestShowParent.SetActive(false);
            cameraMGR.ChangeState(CamerasMGR.CamerasStates.MIXER);
        }
    }

    internal void PutCupInWorkStation()
    {
        currentAddons = new int[3] { (int)Drink.AddOn.ICE_CUBES, (int)Drink.AddOn.STRAW, (int)Drink.AddOn.CURRLY_STRAW };
        buttonMGR.UpdateButtons(ButtonMGR.SpriteGroups.ADDONS, currentAddons);
        buttonMGR.showManu(true);
        cupInProgress.transform.parent = workingStation.transform;
        cupInProgress.transform.localPosition = Vector3.zero;
        mixer.takeCupOut();
        mixer.CanBeUsed = false;
        requestShowParent.SetActive(true);
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.ADDON_STATION);
    }

    public void putInsideMixer(int i)
    {
        int drinkBase = currentIngredients[i];
        mixer.putInsideMixer(ingrediantsPrefabsDictionary[currentIngredients[i]], drinkBase);
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.CUPS);
        buttonMGR.showManu(false);
    }


    internal void putAddOnInCup(int i)
    {
        int addOn = currentAddons[i];
        cupInProgress.putAddOn(addOnPrefabsDictionary[currentAddons[i]], addOn);
        buttonMGR.showManu(false);
    }

    public void serveDrink()
    {
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.CHARACTER);
        isCupInProgress = false;
        if (shakesServed < requests.Count)
        {
            Drink required = requests[shakesServed];
            if (drinkInProgress.isEqual(required))
            {
                print("success");
                customers[shakesServed].goodServe();
                musicMGR.Play_Sound(MusicMGR.SoundTypes.SUCCESS);
                Destroy(cupInProgress.gameObject);
                levelStars++;
                levelScore += PRICE_OF_SHAKE;

            }
            else
            {
                print("fail");
                customers[shakesServed].badServe();
                musicMGR.Play_Sound(MusicMGR.SoundTypes.FAIL);
                Destroy(cupInProgress.gameObject);
            }
            shakesServed++;

            IEnumerator finisheCoroutine = endServe();
            StartCoroutine(finisheCoroutine);
        }
    }

    IEnumerator endServe()
    {
        Destroy(cupInDisplay.gameObject);
        requestShowParent.SetActive(false);
        yield return new WaitForSeconds(4);
        if (shakesServed == requests.Count)
        {
            EndLevel();
        }
        else
        {
            IEnumerator newCustomerCoroutine = newCustomer();
            StartCoroutine(newCustomerCoroutine);
        }
    }

    public void StartLevel()
    {
        IEnumerator newCustomerCoroutine = newCustomer();
        StartCoroutine(newCustomerCoroutine);
    }

    public void EndLevel()
    {
        buttonMGR.ShowEndScene(totalScore, levelScore, totalStars, levelStars);
        totalScore += levelScore;
        totalStars += levelStars;
    }

    void ShowRequest()
    {
        if(cupInDisplay != null)
        {
            Destroy(cupInDisplay.gameObject);
        }
        Drink request = requests[shakesServed];
        requestShowParent.SetActive(true);
        cupInDisplay = Instantiate(CupsPrefabDisctionary[(int)request.cupType], requestPosition.transform).GetComponent<Cup>();
        cupInDisplay.init(this, LU, (int)request.cupType);
        cupInDisplay.putAddOn(addOnPrefabsDictionary[(int)request.addOn],(int)request.addOn);
        cupInDisplay.fillCupWithBase((int)request.drinkBase, 20000, 5000);
        cupInDisplay.State = Cup.CupState.ON_DISPLAY;

    }

    IEnumerator newCustomer()
    {
        customers[shakesServed].startMoving();
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.CHARACTER);
        yield return new WaitForSeconds(4);
        ShowRequest();
        yield return new WaitForSeconds(2);
        buttonMGR.showTapToPlay();
    }

    public void startMakingShake()
    {
        cameraMGR.ChangeState(CamerasMGR.CamerasStates.INGREDIANTS);
        currentIngredients = new int[3] { (int)Drink.DrinkBase.CARROT, (int)Drink.DrinkBase.MEAT, (int)Drink.DrinkBase.BANANAS };
        buttonMGR.UpdateButtons(ButtonMGR.SpriteGroups.INGREDIANTS, currentIngredients);
    }

}
