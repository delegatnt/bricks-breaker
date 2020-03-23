using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;    
    public GameObject LeftBorder;
    public GameObject RightBorder;
    public Ball ball;
    public GameObject startPoint;

    private PolygonCollider2D collider;
    private AudioSource platformSound;

    private GameController gc;

    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        platformSound = GetComponent<AudioSource>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (gc.isGameOver) return;

        float leftBorderPosition = LeftBorder.transform.position.x;
        float rightBorderPosition = RightBorder.transform.position.x;
        float platformWidth = collider.bounds.size.x;

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > leftBorderPosition + platformWidth / 2 + 0.2)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
                if(!ball.move)
                {
                    ball.GetComponent<Rigidbody2D>().transform.position = startPoint.transform.position;
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < rightBorderPosition - platformWidth/2 - 0.2)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
                if (!ball.move)
                {
                    ball.GetComponent<Rigidbody2D>().transform.position = startPoint.transform.position;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ball")
        {
            platformSound.Play(0);
        }
    }
}
