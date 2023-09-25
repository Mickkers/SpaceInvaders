using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;

    public System.Action destroyed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
        if(this.gameObject.transform.position.y > 10 || this.gameObject.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }

    }
}
