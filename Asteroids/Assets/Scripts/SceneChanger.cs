using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToGame()
    {
        // Esta línea carga la escena llamada "Game"
        SceneManager.LoadScene("Game");
    }
}
