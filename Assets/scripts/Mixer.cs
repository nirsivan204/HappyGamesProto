using com.zibra.liquid.Solver;
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
    [SerializeField] LiquidUtility LU;
    private float mixTotalTime = 0;
    private GameMGR.DrinkBase baseInMixer = GameMGR.DrinkBase.NONE;
    private bool isMixReady = false;
    private bool MixStarted = false;
    private bool isCupInMixer = false;
    private Cup cupInMixer;
    [SerializeField] GameObject insideMixerSpot;
    GameObject ingInsideMixer = null;
    [SerializeField] ZibraLiquid liquid;
    float restingLiquidDensity = 8;
    float spiningLiquidDensity = 2.41f;
    private bool canBeUsed = false;
    public GameObject CupPlace { get => cupPlace; set => cupPlace = value; }
    public bool CanBeUsed { get => canBeUsed; set => canBeUsed = value; }
    [SerializeField] ButtonMGR buttonMGR;

    public void putInsideMixer(GameObject ingrediant, GameMGR.DrinkBase type )
    {
        if (!isMixing)
        {
            ingInsideMixer = Instantiate(ingrediant, insideMixerSpot.transform);
            ingInsideMixer.transform.localPosition = Vector3.zero;
            baseInMixer = type;
        }
    }

    public void PutCupInMixer(Cup cup)
    {
        cupInMixer = cup;
        isCupInMixer = true;
    }


    public void OnClick()
    {
        if (canBeUsed)
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
                    LU.changeDensityOfLiquid(liquid, restingLiquidDensity);
                    Invoke("emptyMixer", 0.5f);
                }
                else
                {
                    startMixing();
                }
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
        //MM.Play_Sound(MusicMGR.SoundTypes.mixerRun, true);
        isMixing = true;
        //liquid.gameObject.SetActive(true);
        buttonMGR.playVideo();
        LU.changeDensityOfLiquid(liquid, spiningLiquidDensity);
        if (!MixStarted && baseInMixer != GameMGR.DrinkBase.NONE)
        {
            MixStarted = true;
            mixTotalTime = 0;
            Destroy(ingInsideMixer);
            LU.changeColorOfLiquid(liquid, baseInMixer);
        }
    }

    private void stopMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerEnd);
        isMixing = false;
        LU.changeDensityOfLiquid(liquid,restingLiquidDensity);
    }

    private void emptyMixer()
    {
        liquid.gameObject.SetActive(false);
        LU.changeDensityOfLiquid(liquid, spiningLiquidDensity);
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
