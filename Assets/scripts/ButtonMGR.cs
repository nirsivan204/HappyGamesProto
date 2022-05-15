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
    [SerializeField] GameObject order;
    private SpriteGroups currentGroup;
    public enum SpriteGroups
    {
        INGREDIANTS,
        ADDONS,
    }


    public void ButtonPressed(int id)
    {
        if(currentGroup == SpriteGroups.INGREDIANTS)
        {
            ingreadiantButtonPressed(id);
        }
        else
        {
            if (currentGroup == SpriteGroups.ADDONS)
            {
                addOnButtonPressed(id);
            }
        }
    }


    private void ingreadiantButtonPressed(int id)
    {
        GM.putInsideMixer(id);
    }

    private void addOnButtonPressed(int id)
    {
        GM.putAddOnInCup((GameMGR.AddOn)id);
    }

    public void showManu(bool show)
    {
        ingrediantsManu.SetActive(show);
    }

   
    public void TapToStartClicked()
    {
        
        GM.StartLevel();
        tapToStartScene.SetActive(false);
        showManu(true);

    }

    public void UpdateButtons(SpriteGroups group, int[] spritesIdx)
    {
        if(group == SpriteGroups.INGREDIANTS)
        {
            for (int i = 0; i < 3; i++) 
            {
                buttonsImgs[i].sprite = ingrediantsSpriteDictionary[spritesIdx[i]];
            }
            currentGroup = SpriteGroups.INGREDIANTS;
        }
        else
        {
            if (group == SpriteGroups.ADDONS)
            {
                for (int i = 0; i < 3; i++)
                {
                    buttonsImgs[i].sprite = addOnSpriteDictionary[spritesIdx[i]];
                }
                currentGroup = SpriteGroups.ADDONS;
            }
        }
    }
}
