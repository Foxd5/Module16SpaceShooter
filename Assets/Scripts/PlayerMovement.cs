using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.5f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Get the screen bounds based on the camera's view
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get the width and height of the object (the ship)
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // Half the width
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // Half the height
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        //Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            moveY = speed;
            //pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            moveY = -speed;
           // pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            moveX = speed;
           // pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            moveX = -speed;
           // pos.x -= speed * Time.deltaTime;
        }

        rb.velocity = new Vector2(moveX, moveY);

        rb.position = ClampPosition(rb.position);
       
        //transform.position = ClampPosition(pos);
    }

    // function to clamp(or keep) the ship within the bounds of the screen
    private Vector2 ClampPosition(Vector2 pos)
    {
        //the +1 and -1 are artbitrary values. this makes it so the ship is clamped
        //within the bounds of the UI so it doesnt overlap with things like healthbar and ammo/lives
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + objectHeight + 1, screenBounds.y - objectHeight - 1);
        return pos;
    }
}

