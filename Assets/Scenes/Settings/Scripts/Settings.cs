using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        // Obtener las resoluciones de la pantalla
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Crear una lista de opciones
        List<string> options = new List<string>();


        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Agregar solo aquellas resoluciones cuyo valor de altura sea mayor o igual a 400px
            if (resolutions[i].height >= 400)
            {
                // Crear una string con las caractersticas de la resolución y añadirla a la lista de opciones
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                // Establecer la resolución actual del monitor
                if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        // Añadir las opciones al dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetBackgroundVolume(float volume)
    {
        AudioManager.instance.SetVolume("Menu", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Back()
    {
        StartMenu.actualSceneName = StartMenu.previousSceneName;
        StartMenu.previousSceneName = "Settings";
        SceneManager.LoadScene(StartMenu.actualSceneName);
    }
}

//public class Settings : MonoBehaviour
//{
//    private AudioManager audio;
//    public Slider slider;

//    // Start is called before the first frame update
//    void Start()
//    {
//        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
//        slider = GameObject.Find("Slider").GetComponent<Slider>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void Back()
//    {
//        StartMenu.actualSceneName = StartMenu.previousSceneName;
//        StartMenu.previousSceneName = "Settings";
//        SceneManager.LoadScene(StartMenu.actualSceneName);
//    }

//    public void VolumeValueUpdated()
//    {
//        audio.SetVolume("Menu", slider.value);
//    }
//}
