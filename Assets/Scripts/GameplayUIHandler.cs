using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] List<TMP_Text> texts;
    [SerializeField] List<RawImage> images;
    [SerializeField] GameObject goObject;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = gm.GetLives().ToString();
        if(gm.GetLives() <= 2)
        {
            images[1].enabled = false;
        }
        if (gm.GetLives() <= 1)
        {
            images[0].enabled = false;
        }
        if (gm.gameover)
        {
            goObject.SetActive(true);
            texts[3].text = "GAMEOVER\nFinal Score: " + gm.GetScore();
        }

        texts[1].text = "Score " + gm.GetScore();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
