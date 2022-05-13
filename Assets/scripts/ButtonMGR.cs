using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMGR : MonoBehaviour
{
    [SerializeField] GameMGR GM;
    [SerializeField] GameObject ingrediantsManu;
    [SerializeField] GameObject addOnManu;
    public void ingreadiantButtonPressed(int id)
    {
        GM.putInsideMixer((GameMGR.DrinkBase)id);
    }

    public void addOnButtonPressed(int id)
    {
        GM.putAddOnInCup((GameMGR.AddOn)id);
    }

    public void showAddOns()
    {
        addOnManu.SetActive(true);
        ingrediantsManu.SetActive(false);
    }

    public void showIngrediants()
    {
        addOnManu.SetActive(false);
        ingrediantsManu.SetActive(true);
    }
}
