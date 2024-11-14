using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake_Mechanics : MonoBehaviour
{
    private Transform snakeHead;
    private Vector2 direction = Vector2.right; // (0,1)
    public float speed = 0.1f;
    public Collider2D grid;
    public Bounds bound;// store the area between collider

    public Transform food;

    // Start is called before the first frame update
    void Start()
    {
       snakeHead =  this.GetComponent<Transform>();
       Time.fixedDeltaTime = speed; // 0 is the default frame rendering time and if we increase the time, the time for rendering will increase hence laggy and slow expereience

       bound = grid.bounds; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            direction = Vector2.up; // (0,1)
        }
        else if(Input.GetKey(KeyCode.A)){
            direction = Vector2.left; // (-1,0)
        }
        else if(Input.GetKey(KeyCode.S)){
            direction = Vector2.down; // (0,-1)
        }
        else if(Input.GetKey(KeyCode.D)){
            direction = Vector2.right; // (0,1)
        }
    }

    // Movement related cheezain Fixed Update mein karein
    private void FixedUpdate() {
        // continous movement, if we don't want that, then add/subtract 1 when key pressed
        snakeHead.position = new Vector2(snakeHead.position.x + direction.x, snakeHead.position.y + direction.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Obstacle"){
            snakeHead.position = Vector2.zero;
        }
        if(other.tag == "Food"){
            randomizeFood();
            Debug.Log("Food Eaten");
        }
    }

    private void randomizeFood(){
        float x = Mathf.Round(Random.Range(bound.min.x, bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(bound.min.y, bound.max.y));

        food.position = new Vector2(x,y);
    }

    // food prefab is tagged as obstacle because we'll be adding a prefab to snake body which will be obstacle for snake itself, and we've just one food instance tagged food, which we'll use as actuall prefab
}
