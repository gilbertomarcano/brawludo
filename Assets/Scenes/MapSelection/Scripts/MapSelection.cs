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
    [SerializeField] private Image mapSplash;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMapSelectionUI();
    }

    private void UpdateMapSelectionUI()
    {
        mapSplash.sprite = mapList[mapSelectedIndex].splash;
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
        SceneManager.LoadScene(sceneName: "FightScene");
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneName:"CharacterSelection");
    }

    [System.Serializable]
    public class MapSelectObject
    {
        public Sprite splash;
    }
}
