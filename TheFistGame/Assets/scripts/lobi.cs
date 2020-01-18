using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lobi : MonoBehaviour
{
    private int size = 7;
    private GameObject[] objetos= new GameObject[7];
    private int contador = 0;
    public GameObject objeto0;
    public GameObject objeto1;
    public GameObject objeto2;
    public GameObject objeto3;
    public GameObject objeto4;
    public GameObject objeto5;
    public GameObject objeto6;

    public void QuitGame()
    {
        Application.Quit();

    }
    
    public void Siguiente()
    {
        contador = contador +1;
        if(contador == size)
        {
            contador=0;
        }
        Cambiar(contador);
    }
    public void Anterior()
    {
        contador = contador -1;
        if(contador < 0)
        {
            contador=size-1;
        }
        Cambiar(contador);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void Cambiar(int visible)
    {
        int i;
        for(i=0;i<size; i++)
        {
            if(visible==i)
            {
                objetos[i].SetActive(true);
            }               
            else
            {
                objetos[i].SetActive(false);
            }               
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        objetos[0]=objeto0;
        objetos[1]=objeto1;
        objetos[2]=objeto2;
        objetos[3]=objeto3;
        objetos[4]=objeto4;
        objetos[5]=objeto5;
        objetos[6]=objeto6;
        Cambiar(contador);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
