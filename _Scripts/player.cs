using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class player : MonoBehaviour
{
    public GameObject selectedObject;

    Vector3 offset;

    float tempX;

    float tempY;

    public float maxMoveLeft, maxMoveRight;

    public float maxMoveTop, maxMoveBottom;

    
    
    public float fireRate;

    float originalFireRate;

    public float fireTime = 0.0f;

    public GameObject playerBullet;

    public Transform fire1, fire2, fire3, fire4, fire5, fire6;

    public int firePower = 1;


    public int playerScore = 0;

    public Text scoreText;

    public int playerLives = 3;

    public Text livesText;


    public string activePickup;

    public GameObject gameManager;

    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "score: " + playerScore;

        livesText.text = "lives: " + playerLives;

        fire1 = transform.Find("FirePoint1").gameObject.transform;
        fire2 = transform.Find("FirePoint2").gameObject.transform;
        fire3 = transform.Find("FirePoint3").gameObject.transform;
        fire4 = transform.Find("FirePoint4").gameObject.transform;
        fire5 = transform.Find("FirePoint5").gameObject.transform;
        fire6 = transform.Find("FirePoint6").gameObject.transform;

        originalFireRate = fireRate;

        shield = transform.Find("playerShield").gameObject;

        gameManager = GameObject.FindWithTag("GameManager").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject != null)
            {
                if (targetObject.tag == "Player")
                {
                    selectedObject = targetObject.transform.gameObject;

                    offset = selectedObject.transform.position - mousePosition;
                }    
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;

            tempX = selectedObject.transform.position.x;

            tempY = selectedObject.transform.position.y;

            if (tempX < maxMoveLeft)
            {
                tempX = maxMoveLeft;
            }

            if (tempX > maxMoveRight)
            {
                tempX = maxMoveRight;
            }

            if (tempY > maxMoveTop)
            {
                tempY = maxMoveTop;
            }

            if (tempY < maxMoveBottom)
            {
                tempY = maxMoveBottom;
            }
            
            transform.position = new Vector2(tempX, tempY);
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }

        if (Time.time >= fireTime)
        {
            fireTime = Time.time + fireRate;

            if (firePower == 1)
            {
                Instantiate(playerBullet, fire1.transform.position, fire1.transform.rotation);

                Instantiate(playerBullet, fire2.transform.position, fire2.transform.rotation);
            }

            if (firePower == 2)
            {
                Instantiate(playerBullet, fire1.transform.position, fire1.transform.rotation);

                Instantiate(playerBullet, fire2.transform.position, fire2.transform.rotation);

                Instantiate(playerBullet, fire3.transform.position, fire3.transform.rotation);

                Instantiate(playerBullet, fire4.transform.position, fire4.transform.rotation);
            }

            if (firePower == 3)
            {
                Instantiate(playerBullet, fire1.transform.position, fire1.transform.rotation);

                Instantiate(playerBullet, fire2.transform.position, fire2.transform.rotation);

                Instantiate(playerBullet, fire3.transform.position, fire3.transform.rotation);

                Instantiate(playerBullet, fire4.transform.position, fire4.transform.rotation);

                Instantiate(playerBullet, fire5.transform.position, fire5.transform.rotation);

                Instantiate(playerBullet, fire6.transform.position, fire6.transform.rotation);
            }

        }

        if (playerLives <= 0)
        {
            gameManager.GetComponent<gameManager>().finalScore = playerScore;

            gameManager.GetComponent<gameManager>().fromGame = true;

            SceneManager.LoadScene("s5_scores");
        }





    }

    public void AddScore(int points)
    {
        playerScore += points;

        scoreText.text = "score: " + playerScore;
    }

    public void SubtractLife()
    {
        playerLives -= 1;

        livesText.text = "lives: " + playerLives;
    }

    public void Pickup(string pickupName)
    {
        activePickup = pickupName;

        if (pickupName == "FirePower5")
        {
            firePower = 3;

            fireRate /= 3.0f;

            gameManager.GetComponent<gameManager>().PickupCaptured();

        }

        if (pickupName == "FirePower3")
        {
            firePower = 2;

            fireRate /= 2.0f;

            gameManager.GetComponent<gameManager>().PickupCaptured();
        }

        if (pickupName == "LoseALife")
        {
            SubtractLife();

            gameManager.GetComponent<gameManager>().PickupReset();
        }

        if (pickupName == "Shield")
        {
            shield.SetActive(true);

            gameManager.GetComponent<gameManager>().PickupCaptured();
        }

        if (pickupName == "SmartBomb")
        {
            GameObject[] Bigs;

            Bigs = GameObject.FindGameObjectsWithTag("EnemyBig");

            foreach (GameObject Big in Bigs)
            {
                Big.GetComponent<enemy>().Death();
            }

            gameManager.GetComponent<gameManager>().PickupReset();
        }
    }

    public void TurnOffPickup()
    {
        if (activePickup == "FirePower5" || activePickup == "FirePower3")
        {
            firePower = 1;

            fireRate = originalFireRate;
        }

        if (activePickup == "Shield")
        {
            shield.SetActive(false);
        }
    }

}
