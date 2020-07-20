using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Un arreglo con los diferentes sonidos del juego
    public Sound[] sounds;

    // Unica instancia del AudioManager para asegurarnos que solo exista un GameObject al transicionar entre Scenes
    public static AudioManager instance;

    // Start es llamado al comenzar
    void Start()
    {
        // Se reproduce la música principal de fondo justo al arrancar el juego
        PlayBackgroundMusic("Menu");

        
    }

    // Awake es llamado justo antes de llamarse a Start
    void Awake()
    {
        // Nos aseguramos que solo exista una instancia del AudioManager
        if (instance == null)
        {
            // Si no existe, asignar la instancia al GameObject actual que llamó al método
            instance = this;
        }
        else
        {
            // De existir, destruir el Gameobject actual para conservar la instancia anterior
            Destroy(gameObject);
            return;
        }

        // Método que nos asegura conservar el mismo AudioManager al transicionar entre Scenes
        DontDestroyOnLoad(gameObject);

        // Recorremos el arreglo de sonidos y establecemos los parametros correspondientes de la fuente en cada uno de ellos
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name) { }
    // Método para reproducir cualquier sonido que se encuentre en el juego basado en su nombre
    public void PlayBackgroundMusic(string name)
    {
        // Encontramos el sonido que queremos reproducir utilizando una busqueda de arreglos con el nombre
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) 
        {
            // De no encontrarse un sonido, alertar con el depurador
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Reproducir fuente de sonido
        sound.source.Play();
    }

    public void PlaySound(string name)
    {
        // Encontramos el sonido que queremos reproducir utilizando una busqueda de arreglos con el nombre
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            // De no encontrarse un sonido, alertar con el depurador
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Reproducir fuente de sonido
        sound.source.volume = 0.3f;
        sound.source.Play();
    }

    public void PlayButtonClick()
    {
        // Encontramos el sonido que queremos reproducir utilizando una busqueda de arreglos con el nombre
        Sound sound = Array.Find(sounds, s => s.name == "ButtonClick");

        if (sound == null)
        {
            // De no encontrarse un sonido, alertar con el depurador
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Reproducir fuente de sonido
        sound.source.volume = 1f;
        sound.source.Play();
    }


    public void StopMusic(String name)
    {
        //Encontramos el sonido que queremos detener utilizando una busqueda de arreglos con el nombre
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            // De no encontrarse un sonido, alertar con el depurador
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //Reproducir fuente de sonido
        sound.source.Stop();
    }



    public void SetVolume(string name, float volume)
    {
        // Encontramos el sonido que queremos reproducir utilizando una busqueda de arreglos con el nombre
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) 
        {
            // De no encontrarse un sonido, alertar con el depurador
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Reproducir fuente de sonido
        sound.source.volume = volume;
    }


}
