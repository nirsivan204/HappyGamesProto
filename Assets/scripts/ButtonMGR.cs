using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
    [SerializeField] GameObject playScene;
    [SerializeField] RawImage Image;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip[] cup1videos;
    [SerializeField] VideoClip[] cup2videos;
    [SerializeField] VideoClip[] cup3videos;
    [SerializeField] GameObject debugManu;
    bool isDebugMode = false;

    public enum SpriteGroups
    {
        INGREDIANTS,
        ADDONS,
    }

    public void toggleDebugManu()
    {
        debugManu.SetActive(!isDebugMode);
        isDebugMode = !isDebugMode;
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
        GM.putAddOnInCup(id);
    }

    public void showManu(bool show)
    {
        ingrediantsManu.SetActive(show);
    }

    public void showTapToPlay()
    {
        tapToStartScene.SetActive(true);

    }

    public void PlayClicked()
    {
        GM.StartLevel();
        playScene.SetActive(false);

    }


    public void TapToStartClicked()
    {
        
        tapToStartScene.SetActive(false);
        GM.startMakingShake();
        showManu(true);

    }

    public void playVideo(Drink.DrinkBase baseInMixer, CupsDispanser.CupType cupType)
    {
        if(baseInMixer != Drink.DrinkBase.NONE)
        {
            Image.gameObject.SetActive(true);
            switch (cupType)
            {
                case CupsDispanser.CupType.A:
                    videoPlayer.clip = cup1videos[(int)baseInMixer];
                    break;
                case CupsDispanser.CupType.B:
                    videoPlayer.clip = cup2videos[(int)baseInMixer];
                    break;
                case CupsDispanser.CupType.C:
                    videoPlayer.clip = cup3videos[(int)baseInMixer];
                    break;
            }
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        GM.Mixer.stopMixing();
        Image.gameObject.SetActive(false);
        GM.Mixer.finishMixerActivity();
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
