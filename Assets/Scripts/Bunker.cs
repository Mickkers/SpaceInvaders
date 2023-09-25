using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private Color[] colors;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update()
    {
        int idx = maxHealth - health;
        spriteRenderer.color = colors[idx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            health--;
            if(health == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetHealth(int value)
    {
        health = value;
    }
}
