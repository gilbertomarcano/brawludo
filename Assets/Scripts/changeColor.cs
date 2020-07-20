using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    private bool isColor=false;
    private Renderer characterColor;
    // Start is called before the first frame update
    void Start()
    {
        characterColor = GetComponent<Renderer>();
        
    }
    
    private void setColor()
    {
        if(isColor)
        {
            characterColor.material.SetColor("_Color",Color.white);
            isColor=false;
        }else
        {
            characterColor.material.SetColor("_Color",Color.red);
            isColor=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            setColor();
        }
    }
}
