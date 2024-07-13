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
        double totalError = (double)SoundManager.total_error;
        Debug.Log(totalError);

        if(totalError < 3.0)
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
            if(totalError<3.0)
            {
                cheerbar.size = 1.0f;
            }

            // In good zone
            else if(totalError<4.0)
            {
                cheerbar.size = 0.8f;
            }

            // In normal zone
            else if(totalError<5.0)
            {
                cheerbar.size = 0.6f;
            }

            // In bad zone
            else if(totalError<6.0)
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
