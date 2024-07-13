using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Object for scrollbar
    public Scrollbar cheerbar;
    // double Total_error;
    
    void Start()
    {
    }

    int nCheckActionCount = 0;
    void Update()
    {
        float totalError = (float)SoundManager.total_error;

        if( cheerbar.size >= 1.0f && totalError <= 0.1f )
        {
            SceneManager.LoadScene("EndScene");
        }

        if(nCheckActionCount < 100)
        {
            nCheckActionCount++;
        }
        else
        {
            // In super good zone
            if(totalError<0.5)
            {
                cheerbar.size = 1.0f;
            }
            // In good zone
            else if(totalError<1.0)
            {
                cheerbar.size = 0.8f;
            }
            // In normal zone
            else if(totalError<2.0)
            {
                cheerbar.size = 0.6f;
            }

            // In bad zone
            else if(totalError<4.0)
            {
                cheerbar.size = 0.4f;
            }

            // In super bad zone
            else
            {
                cheerbar.size = 0.2f;
            }

            // Reset check count
            nCheckActionCount = 0;
        }        
    }
    
}
