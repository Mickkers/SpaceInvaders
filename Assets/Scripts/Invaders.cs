using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float spacing;

    [SerializeField] private Invader[] prefabs;
    [SerializeField] private AnimationCurve speed;
    [SerializeField] private float advanceTrigPadding;
    [SerializeField] private float advanceSpeed;
    [SerializeField] private float missileAttackRate;
    [SerializeField] private Projectile missilePrefab;
    private Vector3 direction = Vector2.right;
    public bool activeGame = true;
    private GameManager gm;
    private float startY = 2.15f;

    public int amntKilled { get; private set; }
    private int totalInvaders => rows * columns;
    private int amntAlive => totalInvaders - amntKilled;
    private float percentKilled => (float)amntKilled / (float)totalInvaders;

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void Awake()
    {
        
        SetInvaders();
    }

    private void SetInvaders()
    {
        for (int i = 0; i < rows; i++)
        {
            float width = spacing * (this.columns - 1);
            float height = spacing * (this.rows - 1);
            Vector2 center = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(center.x, center.y + (i * spacing), 0f);
            for (int j = 0; j < columns; j++)
            {
                Invader invader = Instantiate(this.prefabs[i], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += j * spacing;
                invader.transform.localPosition = position;
            }
        }
    }

    private void ResetInvaders()
    {
        amntKilled = 0;
        startY -= 0.15f;
        direction = Vector2.right;
        transform.position = new Vector3(0, startY, 1);
        SetInvaders();
        gm.ResetBunkers();

    }

    private void InvaderKilled()
    {
        amntKilled++;
    }

    private void MissileAttack()
    {
        if (gm.gameover)
        {
            return;
        }
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (UnityEngine.Random.value < 1f / ((float)amntAlive + 1f))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Update()
    {
        if (activeGame)
        {
            MoveInvaders();

            if (percentKilled == 1)
            {
                ResetInvaders();
            }
        }
        
    }

    private void MoveInvaders()
    {
        transform.position += speed.Evaluate(percentKilled) * Time.deltaTime * direction;

        Vector3 leftEdge = new Vector3(-5, 0, 0);
        Vector3 rightEdge = new Vector3(5, 0, 0);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (direction == Vector3.right && invader.position.x >= rightEdge.x - advanceTrigPadding)
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + advanceTrigPadding)
            {
                AdvanceRow();
            }
        }
        if (percentKilled == 1)
        {
            ResetInvaders();
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1f;

        Vector3 position = transform.position;
        position.y -= advanceSpeed;
        transform.position = position;
    }
}
