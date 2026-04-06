using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour
{
    public float scrollSpeed;

    // X position of background
    public float tempX;

    public float width;

    public SpriteRenderer myRenderer;

    // Awake called before Start
    private void Awake()
    {
        tempX = transform.position.x;

        myRenderer = GetComponent<SpriteRenderer>();

        width = myRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        tempX -= scrollSpeed * Time.deltaTime;

        transform.position = new Vector2(tempX, transform.position.y);

        if (transform.position.x < -width)
        {
            Vector2 groundOffSet = new Vector2(width * 2f, 0);

            transform.position = (Vector2)transform.position + groundOffSet;

            tempX = transform.position.x;
        }
    }
}
