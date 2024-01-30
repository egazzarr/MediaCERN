using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    public GameObject UserBall;
    public GameObject[] spheresB;
    public Material collisionMaterial;
    public Material lineMaterial;
    public float mindist = 1f;
    public float speed = 3f;
    public float linewidth = 0.1f;
    private LineRenderer lineRenderer;
    private List<GameObject> coloredSpheres = new List<GameObject>();
 

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = linewidth;
        lineRenderer.endWidth = linewidth;

 
    }

    void Update()
    {
        // UserBall Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 pos = UserBall.transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        UserBall.transform.position = pos;

        // Sphere Collision Logic
        foreach (GameObject sphere in spheresB)
        {
            float distance = Vector2.Distance(UserBall.transform.position, sphere.transform.position);

            if (distance <= mindist && sphere.GetComponent<Renderer>().material != collisionMaterial)
            {
                Debug.Log("Distance is " + distance);
                sphere.GetComponent<Renderer>().material = collisionMaterial;

                // Apply the bouncy material to the collider of the collided sphere
                
                // Track the spheres that have changed color
                if (!coloredSpheres.Contains(sphere))
                {
                    coloredSpheres.Add(sphere);
                }
            }
        }

        // Draw lines between all colored spheres
        DrawLinesBetweenSpheres();
    }

    void DrawLinesBetweenSpheres()
    {
        // Enable the line renderer
        lineRenderer.enabled = true;

        // Set the positions of the line renderer in 2D space
        lineRenderer.positionCount = coloredSpheres.Count * (coloredSpheres.Count - 1);
        
        int lineRendererIndex = 0;

        for (int i = 0; i < coloredSpheres.Count; i++)
        {
            for (int j = i + 1; j < coloredSpheres.Count; j++)
            {
                Vector3 position1 = coloredSpheres[i].transform.position;
                Vector3 position2 = coloredSpheres[j].transform.position;

                // Draw lines between all pairs of colored spheres
                lineRenderer.SetPosition(lineRendererIndex++, new Vector2(position1.x, position1.y));
                lineRenderer.SetPosition(lineRendererIndex++, new Vector2(position2.x, position2.y));
            }
        }
    }
}