using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasMGR : MonoBehaviour
{
    CamerasStates state = CamerasStates.INGREDIANTS;
    [SerializeField] Animator camerasAnimator;
    public enum CamerasStates
    {
        INGREDIANTS,
        CUTTINGBOARD,
        MIXER,
    }
    public void ChangeState(CamerasStates state)
    {
        this.state = state;
        camerasAnimator.SetInteger("State",(int)state);
    }

}
