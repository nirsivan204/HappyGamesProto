using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    bool isMixing = false;
    [SerializeField] MusicMGR MM;
    [SerializeField] GameObject cupPlace;
    [SerializeField] float timeToMix = 3;
    private float mixTotalTime = 0;
    private GameMGR.DrinkBase baseInMixer = GameMGR.DrinkBase.NONE;
    private bool isMixReady = false;
    private bool MixStarted = false;
    private bool isCupInMixer = false;
    private Cup cupInMixer;
    [SerializeField] GameObject insideMixerSpot;
    GameObject ingInsideMixer = null;

    public GameObject CupPlace { get => cupPlace; set => cupPlace = value; }

    public void putInsideMixer(GameObject ingrediant, GameMGR.DrinkBase type )
    {
        ingInsideMixer = Instantiate(ingrediant, insideMixerSpot.transform);
        baseInMixer = type;
    }

    public void PutCupInMixer(Cup cup)
    {
        cupInMixer = cup;
        isCupInMixer = true;
    }


    public void OnClick()
    {
        if (isMixing)
        {
            stopMixing();
        }
        else
        {
            if (isMixReady && isCupInMixer)
            {
                cupInMixer.fillCupWithBase(baseInMixer);
                MixStarted = false;
                baseInMixer = GameMGR.DrinkBase.NONE;
                isMixReady = false;
                Destroy(ingInsideMixer);
            }
            else
            {
                startMixing();
            }
        }
    }

    public void takeCupOut()
    {
        isCupInMixer = false;
        cupInMixer = null;
    }

    private void startMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerRun, true);
        isMixing = true;
        if (!MixStarted && baseInMixer != GameMGR.DrinkBase.NONE)
        {
            MixStarted = true;
            mixTotalTime = 0;
        }
    }

    private void stopMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerEnd);
        isMixing = false;

    }


    // Update is called once per frame
    void Update()
    {
        if(isMixing && baseInMixer!= GameMGR.DrinkBase.NONE)
        {
            mixTotalTime += Time.deltaTime;
            if(mixTotalTime > timeToMix)
            {
                isMixReady = true;
            }
        }
    }
}
