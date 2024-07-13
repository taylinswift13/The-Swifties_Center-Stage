using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public Button gameStartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		  gameStartButton.onClick.AddListener(GameSceneLoad);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameSceneLoad()
    {
      SceneManager.LoadScene("SampleScene");
    }
}
