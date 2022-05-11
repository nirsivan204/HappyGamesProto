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
    private GameMGR.DrinkBase baseInMixer = GameMGR.DrinkBase.None;
    private bool isMixReady = false;
    private bool isCupInMixer = false;
    private Cup cupInMixer;
    [SerializeField] GameObject insideMixerSpot;

    public GameObject CupPlace { get => cupPlace; set => cupPlace = value; }
    public bool IsCupInMixer { get => isCupInMixer; set => isCupInMixer = value; }
    public Cup CupInMixer { get => cupInMixer; set => cupInMixer = value; }

    public void putInsideMixer(GameObject ingrediant, GameMGR.DrinkBase type )
    {
        Instantiate(ingrediant, insideMixerSpot.transform);
        baseInMixer = type;
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
            }
            else
            {
                startMixing();
            }
        }
    }

    private void startMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerRun, true);
        isMixing = true;
        mixTotalTime = 0;
    }

    private void stopMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerEnd);
        isMixing = false;

    }


    // Update is called once per frame
    void Update()
    {
        if(isMixing && baseInMixer!= GameMGR.DrinkBase.None)
        {
            mixTotalTime += Time.deltaTime;
            if(mixTotalTime > timeToMix)
            {
                isMixReady = true;
            }
        }
    }
}
