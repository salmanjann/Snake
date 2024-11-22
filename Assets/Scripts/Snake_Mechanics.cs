using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;


// Size iniital
// Snake died, pause and replay
// 
public class Snake_Mechanics : MonoBehaviour
{
    public Transform snakeHead;
    private Vector2 direction = Vector2.right; // (0,1)
    public float speed = 0.1f;
    public Collider2D grid;
    public Bounds bound;// store the area between collider

    public Transform foodBlack;
    public Transform foodWhite;

    private List<Transform> snakeBody = new List<Transform>();
    public Transform snakeBodyPart;

    public int size = 5;

    private int score = 0;

    public GameplayUI gameplayUIRef;
    private bool isGameOver = false;
    public PortalHandler portalHandlerRef;
    private void Awake() {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        snakeHead = this.GetComponent<Transform>();
        speed = Mathf.Abs(PlayerPrefs.GetInt("SnakeSpeed") - 4) / 10.0f;

        Time.fixedDeltaTime = speed; // 0 is the default frame rendering time and if we increase the time, the time for rendering will increase hence laggy and slow expereience

        bound = grid.bounds;

        snakeBody.Add(snakeHead);

        for (int i = 0; i < size; i++)
        {
            GrowSnake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (direction != Vector2.down)
                direction = Vector2.up; // (0,1)
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (direction != Vector2.right)
                direction = Vector2.left; // (-1,0)
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (direction != Vector2.up)
                direction = Vector2.down; // (0,-1)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (direction != Vector2.left)
                direction = Vector2.right; // (0,1)
        }
    }

    // Movement related cheezain Fixed Update mein karein
    private void FixedUpdate()
    {
        if (!isGameOver && !GameplayUI.isPaused)
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i].position = snakeBody[i - 1].position;
            }

            // continous movement, if we don't want that, then add/subtract 1 when key pressed
            snakeHead.position = new Vector2(snakeHead.position.x + direction.x, snakeHead.position.y + direction.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            snakeHead.position = Vector2.zero;
            for (int i = 1; i < snakeBody.Count; i++)
            {
                Destroy(snakeBody[i].gameObject);
            }
            snakeBody.Clear();
            snakeBody.Add(snakeHead);
            isGameOver = true;
            gameplayUIRef.GameoverPanel();
        }
        if (other.tag == "Food")
        {
            ++score;
            gameplayUIRef.UpdateScore(score);
            RandomizeFood();
            GrowSnake();
        }
        if (other.tag == "Portal")
        {
            portalHandlerRef.Teleport();
        }
        if (other.tag == "Portal2")
        {
            portalHandlerRef.Teleport2();
        }
    }

    private void RandomizeFood()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        x -= 0.5f;
        y += 0.5f;

        foodBlack.position = new Vector2(x, y);
        foodWhite.position = new Vector2(x, y);
    }

    private void GrowSnake()
    {
        // Instantiate food prefab, attach it to snake list and move it along the snake
        Transform temp = Instantiate(snakeBodyPart);
        temp.position = snakeBody[snakeBody.Count - 1].position;
        snakeBody.Add(temp);

    }



    // food prefab is tagged as obstacle because we'll be adding a prefab to snake body which will be obstacle for snake itself, and we've just one food instance tagged food, which we'll use as actuall prefab
}
