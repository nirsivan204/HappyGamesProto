using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    public enum CupState
    {
        None,
        inMixer,
        HasBase,

    }
    private GameMGR.Drink drink;
    private CupState state = CupState.None;

    public GameMGR.Drink Drink { get => drink; set => drink = value; }

    public void OnClick()
    {
        switch (state) 
        {
/*            case CupState.None:
                putCupInMixer();
                break;*/
            case CupState.HasBase:
                GetCupOut();
                break;
            default:
                break;

        }
        if (state == CupState.None)
        {
            putCupInMixer();
        }

    }

    private void GetCupOut()
    {
        throw new NotImplementedException();
    }

    private void putCupInMixer()
    {
        throw new NotImplementedException();
    }

    public void fillCupWithBase(GameMGR.DrinkBase drinkBase)
    {
        this.drink.drinkBase = drinkBase;
    }
}
