using com.zibra.liquid.Solver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    [SerializeField] ZibraLiquid liquid;
    LiquidUtility LU;
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
    private CupsDispanser.CupType cupType;

    public GameMGR.Drink Drink { get => drink; set => drink = value; }
    public CupState State { get => state; set => state = value; }

    public void init(GameMGR gameMGR, LiquidUtility LU, int cupType)
    {
        drink = new GameMGR.Drink();
        GM = gameMGR;
        this.LU = LU;
        this.cupType = (CupsDispanser.CupType)cupType;
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
        state = CupState.HAS_BASE;
        liquid.gameObject.SetActive(true);
        LU.changeColorOfLiquid(liquid,drinkBase);
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
