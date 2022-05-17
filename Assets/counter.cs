using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class counter : MonoBehaviour
{
    public TMP_Text scoreval;
    public TMP_Text mainscore;
    
    int levelScore;
    int totalScore;
    [SerializeField] int growthRate;
    private bool isGameOver;
    void Update()
    {
        if (isGameOver)
        {
            Counter();
        }         
    
    }
    public void init(int totalScore, int levelScore)
    {
        this.totalScore = totalScore;
        this.levelScore = levelScore;
    }

    public void Counter()
    {
        if (levelScore > totalScore)
        {
            totalScore += growthRate;
            print(totalScore);
            scoreval.text = totalScore.ToString("0");
            mainscore.text = totalScore.ToString("0");
        }
    }

    public void On2()
    {
        isGameOver = true;
    }
}
