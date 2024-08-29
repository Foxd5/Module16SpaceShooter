using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This was largely done with help from youtube videos on how to get an enemy to move randomly.
//It was just to add functionality to the alien ships so they would move on screen, it should be changed
//later on to have enemies have much better movement
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  
    public float moveTime = 2f;   
    public float waitTime = 1f;   

    private Vector3 randomDirection;
    private float moveTimer;
    private float waitTimer;
    private bool isWaiting;

    private Vector2 screenBounds;     
    private float objectWidth;        // half-width of the enemy ship
    private float objectHeight;       // half-height of the enemy ship

    void Start()
    {
        SetRandomDirection();
        moveTimer = moveTime;
        waitTimer = waitTime;

        // calculates the screen bounds so ship doesn't wander off screen
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // get the bounds from the MeshRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        objectWidth = spriteRenderer.bounds.extents.x;  // half the width of the mesh (remember, extents are always half)
        objectHeight = spriteRenderer.bounds.extents.y; // half the height of the mesh
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveEnemy();
        }
        else
        {
            Wait();
        }

        // checks if the enemy is hitting the screen boundary and change direction if needed
        CheckBoundsAndChangeDirection();
    }

    void MoveEnemy()
    {
        // move the enemy in the current random direction
        transform.position += randomDirection * moveSpeed * Time.deltaTime;
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }

    void Wait()
    {
        // wait for a short time before moving again
        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0)
        {
            SetRandomDirection();
            isWaiting = false;
            moveTimer = moveTime;
        }
    }

    void SetRandomDirection()
    {
        // set a new random direction for movement
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void CheckBoundsAndChangeDirection()
    {
        Vector3 pos = transform.position;

        // if the enemy ship hits the screen boundary, reverse its direction on that axis
        if (pos.x >= screenBounds.x - objectWidth || pos.x <= -screenBounds.x + objectWidth)
        {
            // reverse the X direction if hitting the left or right screen bounds
            randomDirection.x = -randomDirection.x;
        }

        if (pos.y >= screenBounds.y - objectHeight || pos.y <= -screenBounds.y + objectHeight)
        {
            // reverse the Y direction if hitting the top or bottom screen bounds
            randomDirection.y = -randomDirection.y;
        }

        // clamp the position to prevent the enemy ship from moving off-screen
        // +1.5 and -1.5 are arbitrary values so it doesnt overlap with UI elements. its different from player because the enemy
        //ships can be smaller
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + objectHeight + 1.5f, screenBounds.y - objectHeight - 1.5f);
        transform.position = pos;
    }
}