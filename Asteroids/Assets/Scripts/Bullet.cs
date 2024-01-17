using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100.0f; // Velocidad ajustada de la bala

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        // Opcional: Destruir la bala después de un tiempo
        // Destroy(gameObject, lifespan);
        IgnoreBorderCollisions();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject); // Destruye el asteroide

            // Añade puntos al score si ScoreManager está disponible
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1); // Añade 1 a la puntuación
            }

            Destroy(gameObject); // Destruye la bala
        }
        // Aquí puedes añadir más lógica de colisión si es necesario
    }

    void IgnoreBorderCollisions()
    {
        IgnoreCollisionsWithTag("Border");
        IgnoreCollisionsWithTag("LeftBorder");
        IgnoreCollisionsWithTag("RightBorder");
    }

    void IgnoreCollisionsWithTag(string tag)
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag(tag);
        foreach (var border in borders)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), border.GetComponent<Collider2D>());
        }
    }
}
