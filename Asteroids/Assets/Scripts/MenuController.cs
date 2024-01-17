using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static string menuToDisplay = "StartMenu";

    public GameObject startMenu;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;

    private void Start()
    {
        ShowMenu(menuToDisplay);
    }

    public void ShowMenu(string menuName)
    {
        menuToDisplay = menuName;

        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        victoryMenu.SetActive(false);

        if (menuName == "StartMenu")
        {
            startMenu.SetActive(true);
        }
        else if (menuName == "GameOverMenu")
        {
            gameOverMenu.SetActive(true);
        }
        else if (menuName == "VictoryMenu")
        {
            victoryMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        menuToDisplay = "StartMenu";
    }

    public void BackToMainMenu()
    {
        ShowMenu("StartMenu");
    }

    public void ShowEndGameMenu(bool isVictory)
    {
        if (isVictory)
        {
            ShowMenu("VictoryMenu");
        }
        else
        {
            ShowMenu("GameOverMenu");
        }
    }

    // Aquí puedes agregar más funciones según sea necesario
}
