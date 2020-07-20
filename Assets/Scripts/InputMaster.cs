using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{
    Vector2 i_movement;

    bool jump = false;
    bool crouch = false;

    private MovementController2D movementController;
    private InputDevice player2;
    private FightSceneManager fightSceneManager;

    void Awake()
    {
        
        // Se obtiene el script para poder aplicar los inputs en él
        movementController = GetComponent<MovementController2D>();

        // Revisamos si el objeto es el Jugador 2 para elegir su control
        if(gameObject.name == "Player2")
        {
            // Obtenemos su PlayerInput para poder modificarlo
            PlayerInput player2 = gameObject.GetComponent<PlayerInput>();

            // Si seleccionó teclado en la pestaña de Selección de personaje, activa el esquema Keyboard, lo mismo con Gamepad
            if (CharacterSelection.GetPlayerTwoController() == 0)
            {
                player2.SwitchCurrentControlScheme("Keyboard");
            }
            else if(CharacterSelection.GetPlayerTwoController() == 1)
            {
                player2.SwitchCurrentControlScheme("Gamepad");
            }
        }
    }

    private void Start()
    {
        // Obtenemos el script de la escena para poder pausar y resumir
        fightSceneManager = GameObject.Find("FightSceneManager").GetComponent<FightSceneManager>();
    }



    void FixedUpdate()
    {
        // Se envía al método Move() de movementController la velocidad de movimiento con los boleanos que indican si desea agacharse y saltar este frame
        movementController.Move(i_movement.x * Time.fixedDeltaTime, crouch, jump);

        // Al terminar el frame se asigna el valor de jump como falso para reiniciarlo
        jump = false;
    }

    // Estos métodos que empiezan con "On" responden directamente al nuevo Input System de Unity.
    private void OnCrouch(InputValue value)
    {
        // Si está presionando la tecla que activa los eventos de OnCrouch, retorna un 1
        // Con esto podemos saber si desea agacharse o no y podemos enviar el bool a Move()
        if(value.Get<float>() > 0)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
    }

    private void OnJump(InputValue value)
    {
        // Si está presionando la tecla que activa los eventos de OnJump, retorna un 1
        // Con esto podemos saber si desea saltar o no y podemos enviar el bool a Move()
        if (value.Get<float>() > 0)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

    }

    // Es un Composite que hice, devuelve valores entre -1 y 1, con esto podemos saber si el movimiento es a la izquierda, derecha, puntos intermedios o si es 0.
    private void OnMoveHorizontal(InputValue value)
    {
        i_movement.x = value.Get<float>();
    }


    // Al presionar el botón de golpear llama al método de PunchAttack()
    public void OnPunch()
    {
        movementController.PunchAttack();
    }

    // Al presionar el botón de kick llama al método de KickAttack()
    private void OnKick(InputValue value)
    {
        movementController.KickAttack();
    }

    private void OnPause(InputValue value)
    {
        if (FightSceneManager.GameIsPaused)
        {
            fightSceneManager.Resume();
        }
        else
        {
            fightSceneManager.Pause();
        }

    }

}

