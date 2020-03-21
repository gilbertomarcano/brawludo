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

    // Update is called once per frame
    void Update()
    {
        
    }

    // Botón de jugar
    public void Play()
    {
        previousSceneName = actualSceneName;
        actualSceneName = "CharacterSelection";
        SceneManager.LoadScene(sceneName:"CharacterSelection");

    }

    public void CharacterView()
    {
        previousSceneName = actualSceneName;
        actualSceneName = "CharacterView";
        SceneManager.LoadScene(sceneName:"CharacterView");
    }

    public void Settings()
    {
        previousSceneName = actualSceneName;
        actualSceneName = "Settings";
        SceneManager.LoadScene(sceneName:"Settings");
    }

    public void BackSettings()
    {
        actualSceneName = previousSceneName;
        previousSceneName = "Settings";
        SceneManager.LoadScene(sceneName:actualSceneName);
    }
}
