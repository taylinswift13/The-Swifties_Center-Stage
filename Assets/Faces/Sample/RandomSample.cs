using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSample : MonoBehaviour
{
    public Image cbody;
    public Image cface;
    public Image chair;
    public Image ckit;
    public Sprite[] body;
    public Sprite[] face;
    public Sprite[] hair;
    public Sprite[] kit;
    public Color[] background;
    private Camera cam;


    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        RandomizeCharacter();
        StartCoroutine(UpdateFaceSpriteRandomly());
    }

    public void RandomizeCharacter()
    {
        //		cbody.sprite = body[0];
        cbody.sprite = body[Random.Range(0, body.Length)];
        cface.sprite = face[Random.Range(0, face.Length)];
        chair.sprite = hair[Random.Range(0, hair.Length)];
        ckit.sprite = kit[Random.Range(0, kit.Length)];
    }
    private void Update()
    {
        //update face sprite every random second
    }

    private IEnumerator UpdateFaceSpriteRandomly()
    {
        while (true)
        {
            float randomInterval = Random.Range(1.0f, 5.0f); // Change the range as needed
            yield return new WaitForSeconds(randomInterval);
            cface.sprite = face[Random.Range(0, face.Length)];
        }
    }

}
