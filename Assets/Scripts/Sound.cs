using UnityEngine;

// Volvemos la clase serializable para que aparezca en el Inspector 
[System.Serializable]
public class Sound
{
    // Nombre del sonido
    public string name;

    // Clip del sonido
    public AudioClip clip;

    // Agregamos un rango a volume y a pitch para poder controlarlos desde el Inspector a través de un slider
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    // Atributo que controla si la música se repetira cíclicamente
    public bool loop;

    // Ocultar la fuente de sonido del inspector
    [HideInInspector]
    public AudioSource source;
}
