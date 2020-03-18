using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    // Indíces del personaje seleccionado por cada jugador
    private int playerOneSelectedCharacterIndex;
    private int playerTwoSelectedCharacterIndex;
    private Color desiredColor;

    // Temporales (???)
    static public TextMeshProUGUI p1Name;
    static public TextMeshProUGUI p2Name;
    public bool isReadyPlayerOne;
    public bool isReadyPlayerTwo;

    // Lista de personajes disponibles
    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>(); 

    // Referencias del UI por cada jugador
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI playerOneCharacterName;
    [SerializeField] private TextMeshProUGUI playerTwoCharacterName;

    [SerializeField] private Image playerOneCharacterSplash;
    [SerializeField] private Image playerTwoCharacterSplash;

    [SerializeField] private Image playerOneBackground;
    [SerializeField] private Image playerTwoBackground;

    // Start es llamada antes de la primera actualización de cuadros
    private void Start()
    {
        UpdatePlayerOneCharacterSelectionUI();
        UpdatePlayerTwoCharacterSelectionUI();
        
    }

    private void Update()
    {
        if (isReadyPlayerOne && isReadyPlayerTwo)
        {
            SceneManager.LoadScene(sceneName:"MapSelection");
        }
            
    }

    // Actualiza la información mostrada al jugador uno dependiendo de su selección
    private void UpdatePlayerOneCharacterSelectionUI()
    {
        playerOneCharacterSplash.sprite = characterList[playerOneSelectedCharacterIndex].splash;
        playerOneCharacterName.text = characterList[playerOneSelectedCharacterIndex].characterName;
        playerOneBackground = characterList[playerOneSelectedCharacterIndex].backgroundColor;

        p1Name = playerOneCharacterName;

    }

    // Actualiza la información mostrada al jugador dos dependiendo de su selección
    private void UpdatePlayerTwoCharacterSelectionUI()
    {
        playerTwoCharacterSplash.sprite = characterList[playerTwoSelectedCharacterIndex].splash;
        playerTwoCharacterName.text = characterList[playerTwoSelectedCharacterIndex].characterName;
        playerTwoBackground = characterList[playerTwoSelectedCharacterIndex].backgroundColor;

        p2Name = playerTwoCharacterName;
    }

    // Funciones de los botones del jugador uno
    public void PlayerOneLeftArrow()
    {
        playerOneSelectedCharacterIndex--;
        if (playerOneSelectedCharacterIndex < 0)
            playerOneSelectedCharacterIndex = characterList.Count - 1;
        
        UpdatePlayerOneCharacterSelectionUI();
    }

    public void PlayerOneRightArrow()
    {
        playerOneSelectedCharacterIndex++;
        if (playerOneSelectedCharacterIndex == characterList.Count)
            playerOneSelectedCharacterIndex = 0;

        UpdatePlayerOneCharacterSelectionUI();
    }

    public void PlayerOneConfirm()
    {
        if (!isReadyPlayerOne)
            isReadyPlayerOne = true;
        else
            isReadyPlayerOne = false;

        Debug.Log(string.Format("Player one selected {0}: {1}",
            playerOneSelectedCharacterIndex,
            characterList[playerOneSelectedCharacterIndex].characterName)
        );
        Debug.Log(isReadyPlayerOne);
    }

    // Funciones de los botones del jugador dos
    public void PlayerTwoLeftArrow()
    {
        playerTwoSelectedCharacterIndex--;
        if (playerTwoSelectedCharacterIndex < 0)
            playerTwoSelectedCharacterIndex = characterList.Count - 1;
        
        UpdatePlayerTwoCharacterSelectionUI();
    }

    public void PlayerTwoRightArrow()
    {
        playerTwoSelectedCharacterIndex++;
        if (playerTwoSelectedCharacterIndex == characterList.Count)
            playerTwoSelectedCharacterIndex = 0;

        UpdatePlayerTwoCharacterSelectionUI();
    }

    public void PlayerTwoConfirm()
    {
        if (!isReadyPlayerTwo)
            isReadyPlayerTwo = true;
        else
            isReadyPlayerTwo = false;
        Debug.Log(string.Format("Player two selected {0}: {1}",
            playerTwoSelectedCharacterIndex,
            characterList[playerTwoSelectedCharacterIndex].characterName)
        );
        Debug.Log(isReadyPlayerTwo);
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneName:"StartMenu");
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Image backgroundColor;

    }
}
