using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class counterstar : MonoBehaviour
{
    public TMP_Text scoreval;
   
  //  public TMP_Text endscoreval;
//    public TMP_Text incscoretext;
 //   public TMP_Text gameovertext;
    
    public int scores;
    public int endScores;
  //  public int scorevalue; 
    public int growthRate;

    public bool gameover;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover == true)
        {
            Counter();
        }
        
      
      // endscoreval.text = endScores.ToString("0");
      /*  
        if (Input.GetKeyDown("space"))
        {
            scores += scorevalue;    
        }
              
        if (Input.GetKeyDown("esc"))
        {
            gameover = true;
        }

        if (gameover == true)
        {
            GameOver();
        }
*/
                    
    
    }
    
    
    public void Counter()
    {
        if (endScores != scores && scores > endScores)
        {
            endScores += growthRate;
            print(endScores);
            scoreval.text = endScores.ToString("0");
           
        }
    }

    public void On2()
    {
        gameover = true;
    }
}
