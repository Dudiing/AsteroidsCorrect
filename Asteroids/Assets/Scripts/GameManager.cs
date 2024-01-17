using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    private int score = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto mantiene el objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruye cualquier duplicado
        }
    }

    public void StartGame()
    {
        // Puedes poner aquí cualquier lógica de inicialización necesaria al empezar el juego
        score = 0; // Resetear la puntuación al empezar
    }

    public void AddPoints(int points)
    {
        score += points;
        // Aquí puedes agregar lógica para actualizar la interfaz de usuario, si es necesario
    }

    public int GetScore()
    {
        return score;
    }

    // Aquí puedes agregar más métodos según sea necesario para tu juego
}
