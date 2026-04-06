using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

//using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int currentLevel = 1;

    public GameObject enemyBig;

    public Text levelText;

    public bool noEnemies = true;

    public float timer = 0.0f;

    public float textTime = 3.0f;

    public string finalLevel;



    public List<GameObject> pickups = new List<GameObject>();

    public float releaseTimer = 0.0f;

    public float minReleasTime, maxReleasTime;

    public float pickupTime;

    public bool pickupOut = false;



    public bool pickupActive = false;

    float pickupTimer = 0.0f;

    public GameObject powerBar;

    public GameObject player;




    public bool fromGame = false;

    public Text highScore_1, highScore_2, highScore_3, highScore_4, highScore_5, highScore_6,
        highScore_7, highScore_8, highScore_9, highScore_10;

    public Text level_1, level_2, level_3, level_4, level_5, level_6, level_7, level_8,
        level_9, level_10;

    public bool checkForNewHighScore = false;

    public int finalScore;

    public int highlightText = 0;




    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        pickupTime = Random.Range(minReleasTime, maxReleasTime);

        if (PlayerPrefs.HasKey("HighScore_1"))
        {
            Debug.Log("The key " + "HighScore_1" + " exists");
        }
        else
        {
            Debug.Log("The key " + "HighScore_1" + " does not exist");

            PlayerPrefs.SetInt("HighScore_1", 0);

            PlayerPrefs.SetString("GameLevel_1", "no level");

            PlayerPrefs.SetInt("HighScore_2", 0);

            PlayerPrefs.SetString("GameLevel_2", "no level");

            PlayerPrefs.SetInt("HighScore_3", 0);

            PlayerPrefs.SetString("GameLevel_3", "no level");

            PlayerPrefs.SetInt("HighScore_4", 0);

            PlayerPrefs.SetString("GameLevel_4", "no level");

            PlayerPrefs.SetInt("HighScore_5", 0);

            PlayerPrefs.SetString("GameLevel_5", "no level");

            PlayerPrefs.SetInt("HighScore_6", 0);

            PlayerPrefs.SetString("GameLevel_6", "no level");

            PlayerPrefs.SetInt("HighScore_7", 0);

            PlayerPrefs.SetString("GameLevel_7", "no level");

            PlayerPrefs.SetInt("HighScore_8", 0);

            PlayerPrefs.SetString("GameLevel_8", "no level");

            PlayerPrefs.SetInt("HighScore_9", 0);

            PlayerPrefs.SetString("GameLevel_9", "no level");

            PlayerPrefs.SetInt("HighScore_10", 0);

            PlayerPrefs.SetString("GameLevel_10", "no level");

            PlayerPrefs.Save();
        }
    }

    public void PickupReset()
    {
        pickupOut = false;

        releaseTimer = 0.0f;

        pickupTime = Random.Range(minReleasTime, maxReleasTime);
    }    

    public void PickupCaptured()
    {
        pickupActive = true;

        pickupOut = false;

        releaseTimer = 0.0f;

        pickupTime = Random.Range(minReleasTime, maxReleasTime);

    }

    public void ResetGame()
    {
        currentLevel = 1;

        timer = 0.0f;

        releaseTimer = 0.0f;

        pickupOut = false;

        pickupActive = false;

        checkForNewHighScore = false;

        highlightText = 0;

        fromGame = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "s2_menu")
        {
            ResetGame();
        }



        if (SceneManager.GetActiveScene().name == "s4_game")
        {
            if (levelText == null)
            {
                levelText = GameObject.Find("Text-Level Display").GetComponent<Text>();
            }

            if (GameObject.FindWithTag("EnemyBig") == null)
            {
                levelText.text = "level: " + currentLevel;

                timer += Time.deltaTime;

                if (timer >= textTime)
                {
                    timer = 0.0f;

                    for (int i = 0; i < currentLevel; i++)
                    {
                        GameObject tempEnemy = Instantiate(enemyBig, transform.position,
                            Quaternion.identity);

                        int temp = i * 3;

                        tempEnemy.GetComponent<SpriteRenderer>().sortingOrder = temp;

                        tempEnemy.transform.Find("numbers").GetComponent<SpriteRenderer>().
                            sortingOrder = (temp + 1);

                    }

                    finalLevel = "level " + currentLevel;

                    currentLevel++;

                    levelText.text = "";
                }
            }

            if (pickupOut == false)
            {
                releaseTimer += Time.deltaTime;

                if (releaseTimer >= pickupTime)
                {
                    releaseTimer = 0.0f;

                    pickupTime = Random.Range(minReleasTime, maxReleasTime);

                    pickupOut = true;

                    Instantiate(pickups[Random.Range(0, pickups.Count)]);
                }
            }

            if (powerBar == null)
            {
                powerBar = GameObject.Find("Image - Power Bar");
            }

            if (player == null)
            {
                player = GameObject.Find("player");

                ResetGame();
            }

            if (pickupActive)
            {
                pickupTimer += Time.deltaTime;

                powerBar.transform.localScale = new Vector3(Mathf.Lerp(1.0f, 0.0f,
                    pickupTimer / 3f), 1, 1);

                if (powerBar.transform.localScale.x <= 0.0f)
                {
                    pickupActive = false;

                    pickupTimer = 0.0f;

                    player.GetComponent<player>().TurnOffPickup();
                }
            }

        }

        if (SceneManager.GetActiveScene().name == "s5_scores")
        {
            if (highScore_1 == null)
            {
                highScore_1 = GameObject.Find("Text-Score (1)").GetComponent<Text>();
                highScore_2 = GameObject.Find("Text-Score (2)").GetComponent<Text>();
                highScore_3 = GameObject.Find("Text-Score (3)").GetComponent<Text>();
                highScore_4 = GameObject.Find("Text-Score (4)").GetComponent<Text>();
                highScore_5 = GameObject.Find("Text-Score (5)").GetComponent<Text>();
                highScore_6 = GameObject.Find("Text-Score (6)").GetComponent<Text>();
                highScore_7 = GameObject.Find("Text-Score (7)").GetComponent<Text>();
                highScore_8 = GameObject.Find("Text-Score (8)").GetComponent<Text>();
                highScore_9 = GameObject.Find("Text-Score (9)").GetComponent<Text>();
                highScore_10 = GameObject.Find("Text-Score (10)").GetComponent<Text>();
            }

            if (level_1 == null)
            {
                level_1 = GameObject.Find("Text-Level (1)").GetComponent<Text>();
                level_2 = GameObject.Find("Text-Level (2)").GetComponent<Text>();
                level_3 = GameObject.Find("Text-Level (3)").GetComponent<Text>();
                level_4 = GameObject.Find("Text-Level (4)").GetComponent<Text>();
                level_5 = GameObject.Find("Text-Level (5)").GetComponent<Text>();
                level_6 = GameObject.Find("Text-Level (6)").GetComponent<Text>();
                level_7 = GameObject.Find("Text-Level (7)").GetComponent<Text>();
                level_8 = GameObject.Find("Text-Level (8)").GetComponent<Text>();
                level_9 = GameObject.Find("Text-Level (9)").GetComponent<Text>();
                level_10 = GameObject.Find("Text-Level (10)").GetComponent<Text>();
            }

            if (fromGame == true)
            {
                if (checkForNewHighScore == false)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        if (finalScore > PlayerPrefs.GetInt("HighScore_" + i))
                        { 
                            for (int x = 10; x > i; x--)
                            {
                                int value = PlayerPrefs.GetInt("HighScore_" + (x - 1));

                                PlayerPrefs.SetInt("HighScore_" + x, value);

                                string value2 = PlayerPrefs.GetString("GameLevel_" + (x - 1));

                                PlayerPrefs.SetString("GameLevel_" + x, value2);
                            }

                            PlayerPrefs.SetInt("HighScore_" + i, finalScore);
                            PlayerPrefs.SetString("GameLevel_" + i, finalLevel);

                            highlightText = i;

                            break;
                        }
                    }

                    checkForNewHighScore = true;

                    switch (highlightText)
                    {
                        case 1:

                            highScore_1.color = Color.red;
                            level_1.color = Color.red;
                            GameObject.Find("Text-Rank (1)").GetComponent<Text>().color = Color.red;

                            break;

                        case 2:

                            highScore_2.color = Color.red;
                            level_2.color = Color.red;
                            GameObject.Find("Text-Rank (2)").GetComponent<Text>().color = Color.red;

                            break;

                        case 3:

                            highScore_3.color = Color.red;
                            level_3.color = Color.red;
                            GameObject.Find("Text-Rank (3)").GetComponent<Text>().color = Color.red;

                            break;

                        case 4:

                            highScore_4.color = Color.red;
                            level_4.color = Color.red;
                            GameObject.Find("Text-Rank (4)").GetComponent<Text>().color = Color.red;

                            break;

                        case 5:

                            highScore_5.color = Color.red;
                            level_5.color = Color.red;
                            GameObject.Find("Text-Rank (5)").GetComponent<Text>().color = Color.red;

                            break;

                        case 6:

                            highScore_6.color = Color.red;
                            level_6.color = Color.red;
                            GameObject.Find("Text-Rank (6)").GetComponent<Text>().color = Color.red;

                            break;

                        case 7:

                            highScore_7.color = Color.red;
                            level_7.color = Color.red;
                            GameObject.Find("Text-Rank (7)").GetComponent<Text>().color = Color.red;

                            break;

                        case 8:

                            highScore_8.color = Color.red;
                            level_8.color = Color.red;
                            GameObject.Find("Text-Rank (8)").GetComponent<Text>().color = Color.red;

                            break;

                        case 9:

                            highScore_9.color = Color.red;
                            level_9.color = Color.red;
                            GameObject.Find("Text-Rank (9)").GetComponent<Text>().color = Color.red;

                            break;

                        case 10:

                            highScore_10.color = Color.red;
                            level_10.color = Color.red;
                            GameObject.Find("Text-Rank (10)").GetComponent<Text>().color = Color.red;

                            break;

                        default:
                            break;


                    }


                }
            }

            highScore_1.text = PlayerPrefs.GetInt("HighScore_1").ToString();
            level_1.text = PlayerPrefs.GetString("GameLevel_1");

            highScore_2.text = PlayerPrefs.GetInt("HighScore_2").ToString();
            level_2.text = PlayerPrefs.GetString("GameLevel_2");

            highScore_3.text = PlayerPrefs.GetInt("HighScore_3").ToString();
            level_3.text = PlayerPrefs.GetString("GameLevel_3");

            highScore_4.text = PlayerPrefs.GetInt("HighScore_4").ToString();
            level_4.text = PlayerPrefs.GetString("GameLevel_4");

            highScore_5.text = PlayerPrefs.GetInt("HighScore_5").ToString();
            level_5.text = PlayerPrefs.GetString("GameLevel_5");

            highScore_6.text = PlayerPrefs.GetInt("HighScore_6").ToString();
            level_6.text = PlayerPrefs.GetString("GameLevel_6");

            highScore_7.text = PlayerPrefs.GetInt("HighScore_7").ToString();
            level_7.text = PlayerPrefs.GetString("GameLevel_7");

            highScore_8.text = PlayerPrefs.GetInt("HighScore_8").ToString();
            level_8.text = PlayerPrefs.GetString("GameLevel_8");

            highScore_9.text = PlayerPrefs.GetInt("HighScore_9").ToString();
            level_9.text = PlayerPrefs.GetString("GameLevel_9");

            highScore_10.text = PlayerPrefs.GetInt("HighScore_10").ToString();
            level_10.text = PlayerPrefs.GetString("GameLevel_10");

        }

    }
}
