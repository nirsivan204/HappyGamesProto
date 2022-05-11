using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    bool isMixing = false;
    [SerializeField] MusicMGR MM;

    public void OnClick()
    {
        if (isMixing)
        {
            stopMixing();
        }
        else
        {
            startMixing();
        }
    }

    private void startMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerRun, true);
        isMixing = true;
    }

    private void stopMixing()
    {
        MM.Play_Sound(MusicMGR.SoundTypes.mixerEnd);
        isMixing = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
