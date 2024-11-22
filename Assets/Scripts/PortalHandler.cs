using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    public Transform portal1White;
    public Transform portal2White;
    public Transform portal1Black;
    public Transform portal2Black;

    public Transform obstacle1White;
    public Transform obstacle1Black;
    public Transform obstacle2White;
    public Transform obstacle2Black;
    public Snake_Mechanics snakeMechanicsRef;
    public SnakeMechanicsBlack snakeMechanicsRefBlack;
    // Start is called before the first frame update
    void Start()
    {   
        RandomizePortal1();
        RandomizePortal2();
        RandomizeObstacle1();
        RandomizeObstacle2();

        InvokeRepeating("RandomizePortal1", 0.0f, 10.0f);
        InvokeRepeating("RandomizePortal2", 0.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void RandomizeObstacle1()
    {
        float x = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.x, snakeMechanicsRef.bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.y, snakeMechanicsRef.bound.max.y));

        x -= 0.5f;
        y += 0.5f;


        obstacle1White.position = new Vector2(x, y);
        obstacle1Black.position = new Vector2(x, y);
    }

    private void RandomizeObstacle2()
    {
        float x = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.x, snakeMechanicsRef.bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.y, snakeMechanicsRef.bound.max.y));

        x -= 0.5f;
        y += 0.5f;

        obstacle2White.position = new Vector2(x, y);
        obstacle2Black.position = new Vector2(x, y);

    }

    private void RandomizePortal1()
    {
        float x = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.x, snakeMechanicsRef.bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.y, snakeMechanicsRef.bound.max.y));

        x -= 0.5f;
        y += 0.5f;

        portal1White.position = new Vector2(x, y);
        portal1Black.position = new Vector2(x, y);

    }

    private void RandomizePortal2()
    {
        float x = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.x, snakeMechanicsRef.bound.max.x)); // round to perfectly align with snake axes
        float y = Mathf.Round(Random.Range(snakeMechanicsRef.bound.min.y, snakeMechanicsRef.bound.max.y));

        x -= 0.5f;
        y += 0.5f;

        portal2White.position = new Vector2(x, y);
        portal2Black.position = new Vector2(x, y);

    }

    public void Teleport()
    {
        snakeMechanicsRef.snakeHead.position = portal2White.position;
        snakeMechanicsRefBlack.snakeHead.position = portal2White.position;
    }

    public void Teleport2()
    {
        snakeMechanicsRef.snakeHead.position = portal1White.position;
        snakeMechanicsRefBlack.snakeHead.position = portal1White.position;
    }
}
