using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    [SerializeField] GameObject baseLiquid;
    public enum CupState
    {
        NONE,
        IN_MIXER,
        HAS_BASE,
        IN_WORKING_STATION,
        ON_DISPLAY,

    }
    private GameMGR.Drink drink;
    private CupState state = CupState.NONE;
    private GameMGR GM;

    public GameMGR.Drink Drink { get => drink; set => drink = value; }
    public CupState State { get => state; set => state = value; }

    public void init(GameMGR gameMGR)
    {
        drink = new GameMGR.Drink();
        GM = gameMGR; 
    }

    public void OnClick()
    {
        switch (state) 
        {
            case CupState.HAS_BASE:
                GetCupOut();
                break;
            case CupState.IN_WORKING_STATION:
                GM.serveDrink();
                break;
            default:
                break;

        }
    }

    private void GetCupOut()
    {
        GM.PutCupInWorkStation();
        state = CupState.IN_WORKING_STATION;
    }

    public void fillCupWithBase(GameMGR.DrinkBase drinkBase)
    {
        this.drink.drinkBase = drinkBase;
        baseLiquid.SetActive(true);
        state = CupState.HAS_BASE;
    }

    internal void putAddOn(GameObject addOnPrefab, GameMGR.AddOn addOn)
    {
        if(addOn != GameMGR.AddOn.NONE)
        {
            Instantiate(addOnPrefab, transform);
            this.drink.addOn = addOn;
        }
    }

}
