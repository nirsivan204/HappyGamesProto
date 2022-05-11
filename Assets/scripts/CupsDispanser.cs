using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsDispanser : MonoBehaviour, IClikable
{
    // Start is called before the first frame update
    [SerializeField] GameMGR GM;

    public void OnClick()
    {
        GM.getNewCup();
    }

}
