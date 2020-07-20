using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListManager : MonoBehaviour
{
    // Se crea un arreglo de objetos, que será la lista de caracteres
    public GameObject[] characterList;
    // Este es el index el caracter seleccionado
    private int index;

    private void Awake()
    {
        // Si eres el jugador 1, se te asigna el index del caracter seleccionado para el jugador 1
        if (gameObject.name == "CharacterList1")
        {
            index = CharacterSelection.GetPlayerOneSelectedIndex();
        }
        // Si eres el jugador 2, se te asigna el index del caracter seleccionado para el jugador 2
        else if (gameObject.name == "CharacterList2")
        {
            index = CharacterSelection.GetPlayerTwoSelectedIndex();
        }

        Debug.Log(gameObject.name + index.ToString());
    }
    private void Start()
    {
        // Se crea un arreglo del tamaño de la cantidad de objetos que derivan de este, es decir, la cantidad de Characters
        characterList = new GameObject[transform.childCount];

        // Se obtiene cada objeto
        for(int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // Se desactiva cada Character
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // Si solo hay un Character en la lista, se deja activado
        if (characterList[0])
          characterList[0].SetActive(true);

        // Se activa el Character en el index seleccionado
        SelectCharacter(index);
    }

    // Este método es de prueba, sirve para cambiar al Character anterior activando el anterior y desactivando el actual.
    public void PreviousCharacter()
    {
        Debug.Log("test PreviousCharacter");
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(true);
    }

    // Este método selecciona el Character
    public void SelectCharacter(int charIndex)
    {
        // Desactiva todos los Character en el arreglo
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // Y activa el Character del index seleccionado
        characterList[charIndex].SetActive(true);
    }

    // Retorna el Character seleccionado como objeto, nos sirve en otras funciones para poder trabajar con componentes de este.
    public GameObject SelectedCharacterObject()
    {
        return transform.GetChild(index).gameObject;
    }

}
