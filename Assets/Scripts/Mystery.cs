using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystery : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float cycleTime;
    [SerializeField] private int[] score;

    private Vector3 leftDestination = new Vector3(-6, 4.2f, 0);
    private Vector3 rightDestination = new Vector3(6, 4.2f, 0);

    private int direction = -1;
    private bool spawned;

    private Rigidbody2D rbody;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        rbody.MovePosition(leftDestination);
        Despawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            return;
        }

        if (direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= rightDestination.x)
        {
            Despawn();
        }
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftDestination.x)
        {
            Despawn();
        }
    }

    private void Spawn()
    {
        direction *= -1;

        if (direction == 1)
        {
            transform.position = leftDestination;
        }
        else
        {
            transform.position = rightDestination;
        }

        spawned = true;
    }

    private void Despawn()
    {
        spawned = false;

        if (direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        Invoke(nameof(Spawn), cycleTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            gm.AddScore(score[Random.Range(0, 5)]);
            Despawn();
        }
    }
}
