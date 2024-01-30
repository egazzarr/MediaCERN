using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour

{
    public GameObject circlePrefab; // The prefab of the ball

    private List<GameObject> spawnedCircles = new List<GameObject>(); // Store references to the spawned circles

    public float radius = 1f; // Radius of the ball
    void Start()

    {
        
        // Get the screen dimensions
        float screenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        float screenHeight = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        
        // Calculate the width and height of each cell in the grid
        float cellWidth = screenWidth / 4f; // Assuming 4 columns
        float cellHeight = screenHeight / 4f; // Assuming 4 rows

        // Loop to place 20 balls in the grid
        for (int i = 0; i < 2; i++)
        {
            // Calculate the row and column indices
            int row = i / 5;
            int col = i % 5;

            // Calculate the position of the ball in the grid
            float xPos = cellWidth * (col + 0.5f) - screenWidth / 2f;
            float yPos = cellHeight * (row + 0.5f) - screenHeight / 2f;

            // Instantiate the ball at the calculated position
            GameObject spawnedCircle = Instantiate(circlePrefab, new Vector2(xPos, yPos), Quaternion.identity);
            spawnedCircles.Add(spawnedCircle); // Store the reference to the spawned circle

        }
    }

            // Function to change the color of spawned circles
    public void ChangeColorOfCircles(Color color)
    {
        foreach (GameObject circle in spawnedCircles)
        {
            circle.GetComponent<SpriteRenderer>().color = color;
        }
    }
    }
