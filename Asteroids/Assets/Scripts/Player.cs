using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool thrusting;
    public Rigidbody2D rb;
    public float thrustPower = 10.0f;
    public float turnDirection = 0.0f;
    public GameObject prefabToSpawn;
    public float spawnDistance = 1.0f;
    public float bulletSpeed = 500.0f;
    public int lives = 3; // Número inicial de vidas
    public GameObject[] lifeSprites; // Array de sprites de vidas en el UI

    void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootPrefab();
        }
    }

    void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustPower);
        }

        if (turnDirection != 0)
        {
            rb.AddTorque(turnDirection);
        }
    }

    void ShootPrefab()
    {
        if (prefabToSpawn)
        {
            Vector2 direction = transform.up;
            Vector2 spawnPosition = (Vector2)transform.position + direction * spawnDistance;

            GameObject bullet = Instantiate(prefabToSpawn, spawnPosition, transform.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            if (rbBullet)
            {
                rbBullet.AddForce(direction * bulletSpeed);
            }
            Destroy(bullet, 2.0f); // Destruye la bala después de 2 segundos para evitar el uso excesivo de memoria
        }
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--; // Reducir una vida
            UpdateLifeUI();

            if (lives <= 0)
            {
                // Lógica para manejar el fin del juego
                HandleGameOver();
            }
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeSprites.Length; i++)
        {
            lifeSprites[i].SetActive(i < lives);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            LoseLife();
            Destroy(collision.gameObject); // Destruye el asteroide al colisionar
        }
    }

    private void HandleGameOver()
    {
        // Determina si se muestra el VictoryMenu o el GameOverMenu
        bool isVictory = ScoreManager.Instance.GetScore() >= ScoreManager.Instance.GetBestScore();

        // Configura el menú a mostrar después de cargar la escena
        MenuController.menuToDisplay = isVictory ? "VictoryMenu" : "GameOverMenu";

        // Cambia a la escena del menú
        SceneManager.LoadScene("Menu");
    }
}
