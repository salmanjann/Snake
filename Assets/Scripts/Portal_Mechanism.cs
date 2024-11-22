using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Mechanism : MonoBehaviour
{
    public Transform portal1;
    public Transform portal2;

    public Collider2D grid;
    public Bounds bound;// store the area between collider

    public Snake_Mechanics SM;

    // Start is called before the first frame update
    void Start()
    {
        bound = grid.bounds;

        RandomizePortal1();
        RandomizePortal2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Portal")
        {
            Teleport();
        }
        if (other.tag == "Portal2")
        {
            Teleport2();
        }
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
        SM.snakeHead.position = portal2.position;
    }

    private void Teleport2()
    {
        SM.snakeHead.position = portal1.position;
    }

}
