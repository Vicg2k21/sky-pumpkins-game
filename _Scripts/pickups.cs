using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public float minSpeed, maxSpeed;

    public float pickupSpeed;

    float tempY;

    public float leftOfScreen, rightOfScreen;

    public float maxBottom, maxTop;

    public GameObject particle;

    public player player;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

        gameManager = GameObject.FindWithTag("GameManager").gameObject;

        tempY = Random.Range(maxBottom, maxTop);

        transform.position = new Vector2(rightOfScreen, tempY);

        pickupSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.left * pickupSpeed) * Time.deltaTime);

        if (transform.position.x < leftOfScreen)
        {
            gameManager.GetComponent<gameManager>().PickupReset();

            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            if (gameObject.tag != "LoseALife")
            {
                Instantiate(particle, transform.position, particle.transform.rotation);

                gameManager.GetComponent<gameManager>().PickupReset();  

                Destroy(collision.gameObject);

                Destroy(gameObject);
            }
            else
            {
                Instantiate(particle, transform.position, particle.transform.rotation);

                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            player.Pickup(gameObject.tag);

            Destroy(gameObject);
        }
    }
}
