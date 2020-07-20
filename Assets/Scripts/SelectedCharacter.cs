using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    public int selected_char = 2;
    //private CharacterSelection characterScript = GameObject.Find("player").GetComponent<CharacterSelection>();

    void Start()
    {
        selected_char = 2;
        //characterScript.SelectCharacter(selected_char);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
