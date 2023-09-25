using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private Projectile laserPrefab;
    private bool laserActive = false;
    private GameManager gm;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
        
    }

    private void MovePlayer()
    {
        if (gm.gameover)
        {
            return;
        }
        rbody.MovePosition(transform.position + speed * Time.deltaTime * direction);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = new Vector3(context.ReadValue<float>(), 0, 0);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (gm.gameover)
        {
            return;
        }
        if (context.started && !laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            gm.Gameover();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            gm.TakeDamage();
        }
    }

    private void LaserDestroyed()
    {
        laserActive = false;
    }
}
