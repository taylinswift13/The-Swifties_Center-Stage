using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public Button ReturnButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		  ReturnButton.onClick.AddListener(ReturnToTitle);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
