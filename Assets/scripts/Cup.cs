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
        None,
        inMixer,
        HasBase,

    }
    private GameMGR.Drink drink;
    private CupState state = CupState.None;
    private GameMGR GM;

    public GameMGR.Drink Drink { get => drink; set => drink = value; }

    public void init(GameMGR gameMGR)
    {
        GM = gameMGR; 
    }

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
    }

    private void GetCupOut()
    {
        GM.PutCupInWorkStation();
    }

    public void fillCupWithBase(GameMGR.DrinkBase drinkBase)
    {
        this.drink.drinkBase = drinkBase;
        baseLiquid.SetActive(true);
        state = CupState.HasBase;
    }
}
