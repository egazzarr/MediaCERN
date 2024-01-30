using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private int blueNodeCount = 0;
    private GameObject lineManager;
    private LineRenderer lineRenderer;
    public float speed = 5f; 

    void Start()
    {
        // Find or create a LineManager in the scene
        lineManager = GameObject.Find("LineManager");

        if (lineManager == null)
        {
            lineManager = new GameObject("LineManager");
            lineRenderer = lineManager.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.material.color = Color.blue;
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer = lineManager.GetComponent<LineRenderer>();
        }
    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical"); 

        Vector2 pos = transform.position; 

        pos.x += h * speed * Time.deltaTime; 
        pos.y += v * speed * Time.deltaTime; 


        transform.position = pos; 
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "node"
        if (collision.gameObject.CompareTag("node"))
        {
            // Change the color of the collided object to blue
            Renderer nodeRenderer = collision.gameObject.GetComponent<Renderer>();
            if (nodeRenderer != null)
            {
                nodeRenderer.material.color = Color.blue;
                blueNodeCount++; // Increment the counter
                Debug.Log("Number of blue nodes: " + blueNodeCount);

                // Unify nearby blue nodes
                UnifyBlueNodes(collision.gameObject.transform.position);

                // Draw lines between colored spheres
                DrawLinesBetweenSpheres();
            }
        }
    }

    private void UnifyBlueNodes(Vector2 currentPosition)
    {
        // Use Physics2D.OverlapCircleAll to find nearby blue nodes
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentPosition, 2.0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("node"))
            {
                Renderer nodeRenderer = collider.GetComponent<Renderer>();
                if (nodeRenderer != null && nodeRenderer.material.color == Color.blue)
                {
                    // Do something to unify the blue nodes (e.g., connect them with a line)
                    Debug.Log("Unifying blue nodes!");
                }
            }
        }
    }

    void DrawLinesBetweenSpheres()
    {
        // Enable the line renderer
        lineRenderer.enabled = true;

        // Get all GameObjects with the "node" tag
        GameObject[] coloredSpheres = GameObject.FindGameObjectsWithTag("node");

        // Filter only the spheres that have turned blue
        coloredSpheres = System.Array.FindAll(coloredSpheres, obj => obj.GetComponent<Renderer>().material.color == Color.blue);

        // Set the positions of the line renderer in 2D space
        lineRenderer.positionCount = coloredSpheres.Length * (coloredSpheres.Length - 1);

        int lineRendererIndex = 0;

        for (int i = 0; i < coloredSpheres.Length; i++)
        {
            for (int j = i + 1; j < coloredSpheres.Length; j++)
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