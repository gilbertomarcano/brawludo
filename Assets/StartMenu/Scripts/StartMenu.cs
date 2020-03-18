using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private RectTransform menuContainer;

    private Vector3[] menuPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneName:"CharacterSelection");
    }

    public void CharacterView()
    {
        SceneManager.LoadScene(sceneName:"CharacterView");
    }
}
