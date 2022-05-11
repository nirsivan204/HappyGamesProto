using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMGR : MonoBehaviour
{
    [SerializeField] GameMGR GM;
    public void ingreadiantButtonPressed(int id)
    {
        GM.putInsideMixer((GameMGR.DrinkBase)id);
    }

}
