using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Definimos los Stats del jugador

    public float maxHealth = 100f;          // Vida máxima 
    float currentHealth;                    // Vida actual
    public float runspeed = 40f;            // Velocidad de movimiento 
    
    public float criticalModifier = 1.5f;   // Modificador de golpes críticos

    public float punchRange = 0.5f;         // Rango de golpe
    public float punchForce = 1000f;        // Fuerza de golpe
    public float punchRate = 0.5f;          // Golpes por segundo permitidos
    public float nextPunchTime = 0f;        // Es una variable usada para saber cuando se puede hacer el siguiente golpe. Es usada en InputMaster()
    public float punchVectorX = 0;          // Esta variable se utiliza para sumar la componente X de fuerza en el vector
    public float punchDamage = 10;          // Daño del golpe

    public float kickRange = 0.5f;          // Rango de patada
    public float kickForce = 1000f;         // Fuerza de patada
    public float kickRate = 0.6f;           // Patadas por segundo permitidas
    public float nextKickTime = 0f;         // Es una variable usada para saber cuando se puede hacer la siguiente patada. Es usada en InputMaster()
    public float kickVectorX = 0;           // Esta variable se utiliza para sumar la componente X de fuerza en el vector
    public float kickDamage = 20;           // Daño de la patada


    private FightSceneManager fightSceneManager;
    private AudioManager audio;
    public GameObject[] objects;
    private Renderer characterRenderer;
    private Animator anim;

    void Awake()
    {
        // Se asigna la vida actual con la vida máxima
        currentHealth = maxHealth;
        fightSceneManager = GameObject.Find("FightSceneManager").GetComponent<FightSceneManager>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();


        // Se obtiene el renderer correspondiente de cada personaje
        if (gameObject.name == "Player1")
        {
            Debug.Log(GameObject.Find("CharacterList1").transform.GetChild(CharacterSelection.GetPlayerOneSelectedIndex()).gameObject.GetComponentInChildren<Renderer>().gameObject.name);
            characterRenderer = GameObject.Find("CharacterList1").transform.GetChild(CharacterSelection.GetPlayerOneSelectedIndex()).gameObject.GetComponentInChildren<Renderer>();
        }
        else
        {
            Debug.Log(CharacterSelection.GetPlayerTwoSelectedIndex());
            characterRenderer = GameObject.Find("CharacterList2").transform.GetChild(CharacterSelection.GetPlayerTwoSelectedIndex()).gameObject.GetComponentInChildren<Renderer>();
        }

    }

    void Start()
    {
        anim = GetComponentInChildren<CharacterListManager>().SelectedCharacterObject().GetComponent<Animator>();
    }


    // Este método sirve para recibir daño. Toma el valor de daño y lo aplica al personaje
    public void TakeDamage(float damage)
    {
        // Reproducimos el sonido del golpe 
        audio.PlaySound("Hit");

        // Se le resta el daño a la vida actual
        currentHealth -= damage;
        Debug.Log("Vida actual: " + currentHealth.ToString());

        if (gameObject.name == "Player1")
        {
            HealthBar.UpdatePlayerOneHealthBar(damage);
            Debug.Log("Player 1 recibio daño " + damage.ToString());
        }
        else if (gameObject.name == "Player2")
            HealthBar.UpdatePlayerTwoHealthBar(damage);

        // COLOCAR ANIMACION DE DAÑO AQUI ABAJO

        // Si la vida llega a 0 entra al método Die()
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Reprducimos el sonido de daño
            audio.PlaySound("Hurt");
        }

        // Aplicamos una subrutina para cambiar el color del personaje a rojo por 0.3 segundos
        StartCoroutine(damageAnimation());

    }
    IEnumerator damageAnimation()
    {
        characterRenderer.material.SetColor("_Color", Color.red);
        // Esperamos por 0.3 segundos
        yield return new WaitForSeconds(0.3f);
        characterRenderer.material.SetColor("_Color", Color.white);
    }

    void Die()
    {
        // Reproducimos el sonido de muerte
        audio.PlaySound("Dead");


        // Animación de muerte
        anim.SetTrigger("Dead");

        // Si uno de los jugadores pierde, se pasa el nombre del otro como ganador al método FinishGame
        if (gameObject.name == "Player1")
            //fightSceneManager.FinishGame("Player2");
            fightSceneManager.FinishGame(GameObject.Find("Player2").GetComponentInChildren<CharacterListManager>().SelectedCharacterObject().name);
        else if (gameObject.name == "Player2")
            //fightSceneManager.FinishGame("Player1");
            fightSceneManager.FinishGame(GameObject.Find("Player1").GetComponentInChildren<CharacterListManager>().SelectedCharacterObject().name);

    }

}
