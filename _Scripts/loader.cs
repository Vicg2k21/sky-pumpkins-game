using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class loader : MonoBehaviour
{
    public GameObject powerBar;

    float myTime = 0.0f;

    public float powerBarMax;

    public float loadingDuration;

    // Start is called before the first frame update
    void Start()
    {
        if (powerBar == null)
        {
            powerBar = GameObject.Find("Image - PowerBar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        powerBar.transform.localScale = new Vector3(Mathf.Lerp(0.0f,
            powerBarMax, myTime / loadingDuration), 1, 1);

        if (powerBar.transform.localScale.x >= powerBarMax)
        {
            SceneManager.LoadScene("s2_menu");
        }
    }
}
