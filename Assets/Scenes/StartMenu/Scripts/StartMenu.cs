using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Se coloca un header de referencia para que aparezca en el Inspector del Game Object Controller al que se le asigne este script
    [Header("Reference")]
    [SerializeField] private RectTransform menuContainer;

    public static string actualSceneName;
    public static string previousSceneName;

    private Vector3[] menuPositions;

    // Start is called before the first frame update
    void Start()
    {
        actualSceneName = SceneManager.GetActiveScene().name;
    }

    // Botón de jugar
    public void Play()
    {
        Load("CharacterSelection");

    }

    public void Credits()
    {
        Load("Credits");
    }

    public void CharacterView()
    {
        Load("CharacterView");
    }

    public void Settings()
    {
        Load("Settings");
    }

    static public void Load(string sceneName)
    {
        previousSceneName = actualSceneName;
        actualSceneName = sceneName;
        SceneManager.LoadScene(sceneName:actualSceneName);
    }

    public void BackSettings()
    {
        actualSceneName = previousSceneName;
        previousSceneName = "Settings";
        SceneManager.LoadScene(sceneName:actualSceneName);
    }
}
