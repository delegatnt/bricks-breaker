using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float speed = 500f;
    public GameObject StartPoint;
    public bool move = false;

    private AudioSource fallSound;
    private GameController gc;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        fallSound = GetComponent<AudioSource>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !move && !gc.isGameOver)
        {
            move = true;
            rigidbody.AddForce(Vector2.up * speed);
        }
    }

    public void Reset()
    {
        if (this.rigidbody)
        {
            move = false;
            rigidbody.velocity = Vector2.zero;
            rigidbody.position = StartPoint.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            this.Reset();

            fallSound.Play(0);

            gc.RemoveLive();
        }
    }
}
