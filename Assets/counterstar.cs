using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class counterstar : MonoBehaviour
{
    [SerializeField] TMP_Text scoreval;
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
