using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MapSelection : MonoBehaviour
{
    // Índice del mapa seleccionado
    private int mapSelectedIndex;

    // Lista de mapas disponibles
    [Header("List of maps")]
    [SerializeField] private List<MapSelectObject> mapList = new List<MapSelectObject>();
    
    // Referencias del UI de la selección de mapas
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private Image mapSplash;
    [SerializeField] private TextMeshProUGUI playerOneName;
    [SerializeField] private TextMeshProUGUI playerTwoName;

    // Start is called before the first frame update
    void Start()
    {
        playerOneName.text = CharacterSelection.p1Name.text;
        playerTwoName.text = CharacterSelection.p2Name.text;

        UpdateMapSelectionUI();
    }

    private void UpdateMapSelectionUI()
    {
        mapSplash.sprite = mapList[mapSelectedIndex].splash;
        mapName.text = mapList[mapSelectedIndex].mapName;
    }

    // Funciones de los botones
    public void LeftArrow()
    {
        mapSelectedIndex--;
        if (mapSelectedIndex < 0)
            mapSelectedIndex = mapList.Count - 1;
        
        UpdateMapSelectionUI();
    }

    public void RightArrow()
    {
        mapSelectedIndex++;
        if (mapSelectedIndex  == mapList.Count)
            mapSelectedIndex = 0;
        
        UpdateMapSelectionUI();
    }

    public void Confirm()
    {
        Debug.Log(string.Format("Map {0}: {1} was selected", mapSelectedIndex, mapList[mapSelectedIndex].mapName));
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneName:"CharacterSelection");
    }

    [System.Serializable]
    public class MapSelectObject
    {
        public Sprite splash;
        public string mapName;
    }
}
