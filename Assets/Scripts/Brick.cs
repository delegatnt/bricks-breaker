using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject breakEffect;
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
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constantMax);

            gc.AppendScore();
        }
    }
}
