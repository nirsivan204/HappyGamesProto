using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMGR : MonoBehaviour
{
    [SerializeField] GameMGR GM;
    [SerializeField] GameObject ingrediantsManu;
    [SerializeField] GameObject tapToStartScene;
    [SerializeField] Sprite[] ingrediantsSpriteDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    [SerializeField] Sprite[] addOnSpriteDictionary; // should be done with dictionary, but cannot be serialized and needed to be simplified for the prototype
    [SerializeField] Image[] buttonsImgs;
    public enum SpriteGroups
    {
        INGREDIANTS,
        ADDONS,
    }


    public void ingreadiantButtonPressed(int id)
    {
        GM.putInsideMixer(id);
    }

    public void addOnButtonPressed(int id)
    {
        GM.putAddOnInCup((GameMGR.AddOn)id);
    }

    public void showAddOns()
    {
        ingrediantsManu.SetActive(false);
    }

    public void showIngrediants()
    {
        tapToStartScene.SetActive(false);
        ingrediantsManu.SetActive(true);
    }

    public void TapToStartClicked()
    {
        GM.StartLevel();
        showIngrediants();
    }

    public void UpdateButtons(SpriteGroups group, int[] meshes)
    {
        if(group == SpriteGroups.INGREDIANTS)
        {
            for (int i = 0; i < 3; i++) 
            {
                buttonsImgs[i].sprite = ingrediantsSpriteDictionary[meshes[i]];
            }
        }
        else
        {
            if (group == SpriteGroups.ADDONS)
            {
                for (int i = 0; i < 3; i++)
                {
                    buttonsImgs[i].sprite = ingrediantsSpriteDictionary[meshes[i]];
                }
            }
        }
    }
}
