using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField] private int score;
    private GameManager gm;

    public System.Action killed;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5.5)
        {
            gm.Gameover();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            killed.Invoke();
            gm.AddScore(score);
            Destroy(this.gameObject);
        }
    }
}
