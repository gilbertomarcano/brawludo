using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FightSceneManager : MonoBehaviour
{
    GameObject winnerScreen;
    GameObject textVictory;
    GameObject pauseMenu;
    AudioManager audio;

    static public bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Instanciamos la ventana de ganador y la deshabilitamos para volver a habilitarla al terminar la partida
        winnerScreen = GameObject.Find("WinnerScreen");
        winnerScreen.SetActive(false);

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audio.StopMusic("Menu");
        audio.PlayBackgroundMusic("FightMusic");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnContinue()
    {
        SceneManager.LoadScene(sceneName: "StartMenu");
        audio.StopMusic("FightMusic");
        audio.PlayBackgroundMusic("Menu");

    }

    public void FinishGame(string name)
    {
        winnerScreen.SetActive(true);
        textVictory = GameObject.Find("TextPlayerWinner");
        textVictory.GetComponent<TextMeshProUGUI>().text = "GANADOR: " + name;

        // Desactivamos los inputs de ambos jugadores
        GameObject.Find("Player1").GetComponent<PlayerInput>().DeactivateInput();
        GameObject.Find("Player2").GetComponent<PlayerInput>().DeactivateInput();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenu.SetActive(true);
    }

    static public void Quit()
    {
        Application.Quit();
    }

}
