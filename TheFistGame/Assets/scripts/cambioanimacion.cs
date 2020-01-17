using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioanimacion : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animacion;
    int mayor = 8;
    int menor = 0;

    void Start()
    {
        animacion = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s")){
            int aux = animacion.GetInteger("posicion");
            if(aux>mayor-1){
                animacion.SetInteger("posicion",0);
            }
            else{
                animacion.SetInteger("posicion",aux+1);
            }
            //Debug.Log( animacion.GetInteger("posicion"));
        }
        else if(Input.GetKeyDown("a")){
            int aux = animacion.GetInteger("posicion");
            if(aux<menor+1){
                animacion.SetInteger("posicion",8);
            }
            else{
                animacion.SetInteger("posicion",aux-1);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

    }
}
