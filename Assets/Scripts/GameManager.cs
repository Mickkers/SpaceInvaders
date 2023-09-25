using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField] private int lives;
    [SerializeField] private GameObject gpUI;
    [SerializeField] private Invaders invaders;
    [SerializeField] private List<Bunker> bunkers;
    public bool gameover = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckLives();
    }

    private void CheckLives()
    {
        if(lives <= 0)
        {
            Gameover();
        }
    }

    public void Gameover()
    {
        gameover = true;
        lives = 0;
        invaders.activeGame = false;
    }

    public void ResetBunkers()
    {
        foreach (Bunker bunker in bunkers)
        {
            bunker.SetHealth(8);
            bunker.gameObject.SetActive(true);
        }
    }

    public void AddScore(int val)
    {
        score += val;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    public void TakeDamage()
    {
        lives -= 1;
    }
}