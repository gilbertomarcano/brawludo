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

    //static public TextMeshProUGUI p1Name;     VARIABLES GLOBALES
    //static public TextMeshProUGUI p2Name;     CharacterSelection.p1Name desde otro script
    public bool isReadyPlayerOne;
    public bool isReadyPlayerTwo;

    // Lista de personajes disponibles
    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>(); 

    // Referencias del UI por cada jugador
    [Header("UI References")]
    
    [SerializeField] private Image playerOneCharacterSplash;
    [SerializeField] private Image playerTwoCharacterSplash;

    [SerializeField] private TextMeshProUGUI playerOneCharacterName;
    [SerializeField] private TextMeshProUGUI playerTwoCharacterName;

    
    // Todo lo relacionado al hacer click en elegir
    private Button playerOneLeftButton;
    private Button playerOneRightButton;
    private TextMeshProUGUI playerOneButtonConfirmText;
    private Image playerOneReadyMask;
    private TextMeshProUGUI playerOneReadyMessage;
    

    private Button playerTwoLeftButton;
    private Button playerTwoRightButton;
    private TextMeshProUGUI playerTwoButtonConfirmText;
    private Image playerTwoReadyMask;
    private TextMeshProUGUI playerTwoReadyMessage;
    

    // Contenedor de las historias de los personajes
    [SerializeField] private Image contentStory;
    
    private TextMeshProUGUI playerStory;
    private RectTransform playerStoryRectTransform;

    


    // Start es llamada antes de la primera actualización de cuadros
    private void Start()
    {   
        // Establecer todas las referencias a los objetos

        if (StartMenu.actualSceneName == "CharacterSelection")
        {
            // Referencias del jugador número uno
            playerOneLeftButton = GameObject.Find("PlayerOneLeftButton").GetComponent<Button>();
            playerOneRightButton = GameObject.Find("PlayerOneRightButton").GetComponent<Button>();
            playerOneButtonConfirmText = GameObject.Find("PlayerOneButtonConfirmText").GetComponent<TextMeshProUGUI>();
            playerOneReadyMask = GameObject.Find("PlayerOneReadyMask").GetComponent<Image>();
            playerOneReadyMessage = GameObject.Find("PlayerOneReadyText").GetComponent<TextMeshProUGUI>();

            // Referencias del jugador número dos
            playerTwoLeftButton = GameObject.Find("PlayerTwoLeftButton").GetComponent<Button>();
            playerTwoRightButton = GameObject.Find("PlayerTwoRightButton").GetComponent<Button>();
            playerTwoButtonConfirmText = GameObject.Find("PlayerTwoButtonConfirmText").GetComponent<TextMeshProUGUI>();
            playerTwoReadyMask = GameObject.Find("PlayerTwoReadyMask").GetComponent<Image>();
            playerTwoReadyMessage = GameObject.Find("PlayerTwoReadyText").GetComponent<TextMeshProUGUI>();

            // Deshabilitar la pantalla 'ready' de ambos jugadores
            playerOneReadyMask.enabled = false;
            playerTwoReadyMask.enabled = false;
            playerOneReadyMessage.enabled = false;
            playerTwoReadyMessage.enabled = false;
        }
        else if (StartMenu.actualSceneName == "CharacterView")
        {
            // Si estamos en la escena que carga la historia de los personajes, conseguir el Rect Transform del contenido
            playerStory = GameObject.Find("PlayerStory").GetComponent<TextMeshProUGUI>();
            playerStoryRectTransform = GameObject.Find("ContentStory").GetComponent<RectTransform>();
        }
        

        


        

        // Actualizar el estado de ambos personajes
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
        
        // Si estamos en una escena donde esté la historia del personaje
        if (playerStory != null) 
        {
            // Cambiar la historia por la del personaje correspondiente
            playerStory.text = characterList[playerOneSelectedCharacterIndex].story;
            // Resetear la posición del texto
            playerStoryRectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }

    // Actualiza la información mostrada al jugador dos dependiendo de su selección
    private void UpdatePlayerTwoCharacterSelectionUI()
    {
        playerTwoCharacterSplash.sprite = characterList[playerTwoSelectedCharacterIndex].splash;
        playerTwoCharacterName.text = characterList[playerTwoSelectedCharacterIndex].characterName;
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


    // Botón de confirmación de el jugador número uno
    public void PlayerOneConfirm()
    {
        if (!isReadyPlayerOne)
        {
            isReadyPlayerOne = true;
            playerOneButtonConfirmText.text = "Cambiar";
            // Mostrar máscara de confirmación
            playerOneReadyMask.enabled = true;
            playerOneReadyMessage.enabled = true;
            // Deshabilitar botones
            playerOneLeftButton.enabled = false;
            playerOneRightButton.enabled = false;
        }
        else
        {
            Debug.Log("Now is not ready");
            isReadyPlayerOne = false;
            playerOneButtonConfirmText.text = "Elegir";
            // Ocultar máscara de confirmación
            playerOneReadyMask.enabled = false;
            playerOneReadyMessage.enabled = false;
            // Habilitar botones
            playerOneLeftButton.enabled = true;
            playerOneRightButton.enabled = true;
        }
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

    // Botón de confirmación de el jugador número dos
    public void PlayerTwoConfirm()
    {
        if (!isReadyPlayerTwo)
        {
            isReadyPlayerTwo = true;
            playerTwoButtonConfirmText.text = "Cambiar";
            // Mostrar máscara de confirmación
            playerTwoReadyMask.enabled = true;
            playerTwoReadyMessage.enabled = true;
            // Deshabilitar botones
            playerTwoLeftButton.enabled = false;
            playerTwoRightButton.enabled = false;
        }
        else
        {
            isReadyPlayerTwo = false;
            playerTwoButtonConfirmText.text = "Elegir";
            // Ocultar máscara de confirmación
            playerTwoReadyMask.enabled = false;
            playerTwoReadyMessage.enabled = false;
            // Habilitar botones
            playerTwoLeftButton.enabled = true;
            playerTwoRightButton.enabled = true;
        }
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
        public string story;

    }
}
