using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class counterstar : MonoBehaviour
{
    [SerializeField] TMP_Text scoreval;
    [SerializeField] TMP_Text headlineText;
    private int levelScore;
    private int totalScore;
    [SerializeField] int growthRate;
    [SerializeField] Image[] starsImgs;
    private bool isGameOver;

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            Counter();
        }    
    }

    public void init(int totalScore, int levelScore)
    {
        this.totalScore = totalScore;
        this.levelScore = levelScore;
        for (int i = 0; i < levelScore; i++) 
        {
            starsImgs[i].enabled = true;
        }
        string headline = "";
        switch (levelScore)
        {
            case 0:
                headline = "LAME";
                break;
            case 1:
                headline = "NICE";
                break;
            case 2:
                headline = "GOOD";
                break;
            case 3:
                headline = "PERFECT";
                break;

        }
        headlineText.text = headline;
    }

    public void Counter()
    {
        if (levelScore > totalScore)
        {
            totalScore += growthRate;
            print(totalScore);
            scoreval.text = totalScore.ToString();
           
        }
    }

    public void On()
    {
        isGameOver = true;
    }
}
