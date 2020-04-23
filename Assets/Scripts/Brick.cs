using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject breakEffect;
    public GameObject dropDownObject;

    private GameController gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            GameObject explosion = Instantiate(breakEffect, gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);

            if(Random.Range(0f, 1f) < 10.333333)
            {
                GameObject ddObject = Instantiate(dropDownObject, gameObject.transform.position, Quaternion.identity);
            }
            
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constantMax);

            gc.AppendScore();
        }
    }
}
