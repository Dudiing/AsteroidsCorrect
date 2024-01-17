using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array para almacenar tus prefabs de asteroides
    public float spawnInterval = 2.0f; // Intervalo en segundos para generar asteroides
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f); // Escala mínima
    public Vector3 maxScale = new Vector3(0.3f, 0.3f, 0.3f); // Escala máxima
    public float minSpeed = 1.0f; // Velocidad mínima del asteroide
    public float maxSpeed = 5.0f; // Velocidad máxima del asteroide
    public float minAngularVelocity = -200.0f; // Velocidad angular mínima (velocidad de rotación)
    public float maxAngularVelocity = 200.0f; // Velocidad angular máxima (velocidad de rotación)

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        InvokeRepeating("SpawnAsteroid", spawnInterval, spawnInterval);
    }

    void SpawnAsteroid()
    {
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        Vector2 spawnPosition = RandomEdgePosition();
        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        newAsteroid.transform.localScale = GetRandomScale();
        Rigidbody2D rb = newAsteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = GetDirectionToCenter(spawnPosition) * Random.Range(minSpeed, maxSpeed);
        rb.angularVelocity = Random.Range(minAngularVelocity, maxAngularVelocity);
        IgnoreBorderCollisions(newAsteroid);

        // Añade el script de colisión a cada asteroide instanciado
        newAsteroid.AddComponent<AsteroidCollision>();
    }

    Vector3 GetRandomScale()
    {
        return new Vector3(
            Random.Range(minScale.x, maxScale.x),
            Random.Range(minScale.y, maxScale.y),
            Random.Range(minScale.z, maxScale.z)
        );
    }

    Vector2 GetDirectionToCenter(Vector2 spawnPosition)
    {
        return (Vector2.zero - spawnPosition).normalized;
    }

    Vector2 RandomEdgePosition()
    {
        Vector2 position;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0:
                position = new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight);
                break;
            case 1:
                position = new Vector2(screenWidth, Random.Range(-screenHeight, screenHeight));
                break;
            case 2:
                position = new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight);
                break;
            default:
                position = new Vector2(-screenWidth, Random.Range(-screenHeight, screenHeight));
                break;
        }

        return position;
    }

    void IgnoreBorderCollisions(GameObject asteroid)
    {
        IgnoreCollisionsWithTag(asteroid, "Border");
        IgnoreCollisionsWithTag(asteroid, "LeftBorder");
        IgnoreCollisionsWithTag(asteroid, "RightBorder");
    }

    void IgnoreCollisionsWithTag(GameObject obj, string tag)
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag(tag);
        foreach (var border in borders)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), border.GetComponent<Collider2D>());
        }
    }

    // Clase interna para manejar colisiones de asteroides
    private class AsteroidCollision : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.LoseLife();
                }
                Destroy(gameObject);
            }
        }
    }
}
