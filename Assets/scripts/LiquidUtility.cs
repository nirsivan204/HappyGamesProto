//using com.zibra.liquid.Solver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidUtility: MonoBehaviour
{
    //static Color orange = new Color(255, 126, 0,255);
    [SerializeField] Color[] colorsPerBase;

    public Color[] ColorsPerBase { get => colorsPerBase; set => colorsPerBase = value; }

/*    public void changeColorOfLiquid(ZibraLiquid liquid , GameMGR.DrinkBase drinkBase)
    {
        liquid.materialParameters.Color = colorsPerBase[(int)drinkBase];
    }

    public void changeDensityOfLiquid(ZibraLiquid liquid, float density)
    {
        liquid.solverParameters.ParticleDensity = density;
    }

    public void ChangeMaxNumParticles(ZibraLiquid liquid, int amount)
    {
        liquid.MaxNumParticles = amount;
    }

    public void ChangeParticlesPerSecond(ZibraLiquid liquid, int amount)
    {
        ((com.zibra.liquid.Manipulators.ZibraLiquidEmitter)liquid.GetManipulatorList()[0]).ParticlesPerSec = amount;
    }*/

}
