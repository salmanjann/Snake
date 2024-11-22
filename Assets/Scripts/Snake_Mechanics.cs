using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Transform food;

    public Transform portal1;
    public Transform portal2;

    public Transform obstacle1;
    public Transform obstacle2;

    private List<Transform> snakeBody = new List<Transform>();
    public Transform snakeBodyPart;

    public int size = 5;

    public GameObject gameOverPanel;
    public GameObject replay;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        snakeHead = this.GetComponent<Transform>();
        // speed = Mathf.Abs(PlayerPrefs.GetInt("SnakeSpeed") - 5) / 10.0f;
        // Debug.Log(speed);
        // speed = 0.02f;
        Time.fixedDeltaTime = speed; // 0 is the default frame rendering time and if we increase the time, the time for rendering will increase hence laggy and slow expereience

        bound = grid.bounds;

        snakeBody.Add(snakeHead);

        for (int i = 0; i < size; i++)
        {
            GrowSnake();
        }

        RandomizeFood();
        RandomizePortal1();
        RandomizePortal2();
        RandomizeObstacle1();
        RandomizeObstacle2();

        InvokeRepeating("RandomizePortal1", 10f, 10f);
        InvokeRepeating("RandomizePortal2", 10f, 10f);
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
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].position = snakeBody[i - 1].position;
        }
        // continous movement, if we don't want that, then add/subtract 1 when key pressed
        snakeHead.position = new Vector2(snakeHead.position.x + direction.x, snakeHead.position.y + direction.y);
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
            GameoverPanel();
        }
        if (other.tag == "Food")
        {
            RandomizeFood();
            GrowSnake();
        }
        if (other.tag == "Portal")
        {
            Teleport();
        }
        if (other.tag == "Portal2")
        {
            Teleport2();
        }
    }

    private void RandomizeFood()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        food.position = new Vector2(x, y);
    }

    private void RandomizeObstacle1()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        obstacle1.position = new Vector2(x, y);
    }

    private void RandomizeObstacle2()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        obstacle2.position = new Vector2(x, y);
    }

    private void RandomizePortal1()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        portal1.position = new Vector2(x, y);
    }

    private void RandomizePortal2()
    {
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        portal2.position = new Vector2(x, y);
    }

    private void Teleport()
    {
        snakeHead.position = portal2.position;
    }

    private void Teleport2()
    {
        snakeHead.position = portal1.position;
    }

    private void GrowSnake()
    {
        // Instantiate food prefab, attach it to snake list and move it along the snake
        Transform temp = Instantiate(snakeBodyPart);
        temp.position = snakeBody[snakeBody.Count - 1].position;
        snakeBody.Add(temp);

    }

    private void GameoverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Level2");       
    }

    // food prefab is tagged as obstacle because we'll be adding a prefab to snake body which will be obstacle for snake itself, and we've just one food instance tagged food, which we'll use as actuall prefab
}
