using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class Drink
{
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
    public DrinkBase drinkBase = DrinkBase.NONE;
    public AddOn addOn = AddOn.NONE;
    public CupsDispanser.CupType cupType;
    public bool isEqual(Drink required)
    {
        if (required.cupType == this.cupType)
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
