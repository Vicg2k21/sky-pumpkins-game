using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    public float minSpeed, maxSpeed;

    public float enemySpeed;

    float tempY;

    float tempX;

    public float leftOfScreen, rightOfScreen;

    public float maxPositionTop, maxPositionBottom;

    public GameObject particle;

    public player player;

    public int enemyValue;

    public int hitPoints;

    public SpriteRenderer myNumber;

    //public Sprite number2, number4, number6; *** Maybe not right ***
    public Sprite number1, number2, number3, number4, number5, number6;

    void ResetEnemy()
    {
        tempX = rightOfScreen;

        tempY = Random.Range(maxPositionBottom, maxPositionTop);

        enemySpeed = Random.Range(minSpeed, maxSpeed);
    }


    // Start is called before the first frame update
    void Start()
    {
        ResetEnemy();

        int tempSize = Random.Range(0, 3);

        myNumber = transform.Find("numbers").GetComponent<SpriteRenderer>();

        switch (tempSize)
        {
            case 0:

                transform.localScale = new Vector3(.75f, .75f, 1);

                hitPoints = 2;

                myNumber.sprite = number2;

                enemyValue = 50;

                break;

            case 1:

                transform.localScale = new Vector3(1, 1, 1);

                hitPoints = 4;

                myNumber.sprite = number4;

                enemyValue = 75;

                break;

            case 2:

                transform.localScale = new Vector3(1.5f, 1.5f, 1);

                hitPoints = 6;

                myNumber.sprite = number6;

                enemyValue = 100;

                break;

        }

        transform.position = new Vector2(tempX, tempY);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        tempX -= enemySpeed * Time.deltaTime;

        if (transform.position.x < leftOfScreen)
        {
            ResetEnemy();
        }

        transform.position = new Vector2(tempX, tempY);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);

            Instantiate(particle, transform.position, particle.transform.rotation);

            hitPoints--;

            switch (hitPoints)
            {
                case 5:

                    myNumber.sprite = number5;
                    
                    break;
                case 4:
                    
                    myNumber.sprite = number4;
                    
                    break;
                case 3:
                    
                    myNumber.sprite = number3;
                    
                    break;
                case 2:
                    
                    myNumber.sprite = number2;
                    
                    break;
                case 1:
                    
                    myNumber.sprite = number1;
                    
                    break;
            }

            if (hitPoints <= 0)
            {
                player.AddScore(enemyValue); 

                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            player.SubtractLife();

            Instantiate(particle, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "PlayerShield")
        {
            Instantiate(particle, transform.position, transform.rotation);

            Destroy(gameObject);    
        }
    }

    public void Death()
    {
        player.AddScore(enemyValue);

        Destroy(gameObject);
    }

}
