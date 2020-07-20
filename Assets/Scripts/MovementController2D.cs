using System;
using UnityEngine;
using UnityEngine.Events;

public class MovementController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Fuerza que se le aplica al jugador cuando salta.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Cantidad de velocidad se le aplica al maxSpeed caundo el jugador está agachado
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // Cuanto se suaviza el movimiento
	[SerializeField] private bool m_AirControl = false;                         // Capacidad del jugador de moverse en el aire
	[SerializeField] private LayerMask m_WhatIsGround;                          // Una mask que determina que es ground para el jugador
	[SerializeField] private Transform m_GroundCheck;                           // Una posición marcando donde revisar si el jugador está en la tierra
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // Un collider que se desactivará cuando se estpe agachado
	[SerializeField] private PlayerStats stats;                                 // Stats del jugador
	[SerializeField] private Transform punchPoint;								// Posición desde donde sale el golpe
	[SerializeField] private Transform kickPoint;                               // Posición desde donde sale la patada
	[SerializeField] public LayerMask playerLayer;

	const float k_GroundedRadius = .2f; // Radio del círculo que parte del punto m_WhatIsGround para identificar si está en el piso
	private bool m_Grounded;            // Si el jugador está en el piso será true
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // Determina si el jugador está mirando hacia la derecha
	private Vector3 m_Velocity = Vector3.zero;
	private Animator anim;
	private GameObject player;
	private Transform playerTransform;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		//Aquí cargamos los componentes necesarios
		
		playerTransform = GetComponent<Transform>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		// Si eres el jugador 2, inicias viendo al lado izquierdo
		if(gameObject.name == "Player2")
		{
			Flip();
		}

	}

	private void Start()
	{
		player = GetComponentInChildren<CharacterListManager>().SelectedCharacterObject();
		Debug.Log(player.name);
		anim = player.GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// El jugador está en el piso si un circeclast en la posición del groundcheck toca algo que esté designado como tierra
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		// Se recorre el arreglo de colliders que tienen contacto con el circlecast del groundcheck
		for (int i = 0; i < colliders.Length; i++)
		{
			// Confirmamos si el collider no es el mismo del jugador
			if (colliders[i].gameObject != gameObject)
			{
				// Colocamos como verdadero el booleano de m_Grounded ya que se consiguió un collider diferente al jugador
				m_Grounded = true;
				// Dejamos de reproducir la animación de salto
				anim.SetBool("isJumping", false);

			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		m_wasCrouching = crouch;
		// Revisamos si el jugador está agachado o de pie
		if (!crouch)
		{
			// Si está de pie, se deja de reproducir la animación de estar agachado
			anim.SetBool("isCrouching", false);
		}

		// Con esto solo se puede controlar al jugador si está en el piso o si está activado el control aéreo
		if (m_Grounded || m_AirControl)
		{
			// En el caso de que se estpe agachando
			if (crouch)
			{

				// Se reduce la velocidad por el multiplicador
				move *= m_CrouchSpeed;

				// Se desactiva uno de los colliders cuando se agacha
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;

				// Se reproduce la animación de estar agachado
				anim.SetBool("isCrouching", true);
			}
			else
			{
				// Se activa el collider cuando no se esté agachando
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

			}

			// Se mueve el jugador encontrando la velocidad deseada
			// Se aplica su modificador de velocidad
			move *= stats.runspeed;
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			// Y la suavizamos con el método SmoothDamp y se la aplicamos al jugador
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Si el input es moverse a la derecha y el jugador está viendo hacia la izquierda...
			if (move > 0)
			{
				// ... Se voltea el jugador y se reproduce la animación
				anim.SetBool("isRunning", true);

				if (!m_FacingRight)
				{
					Flip();
				}
				
			}
			// Ahora si el jugador intenta moverse a la izquierda y está viendo a la derecha...
			else if (move < 0)
			{
				// Se voltea el jugador y se reproduce la animación
				anim.SetBool("isRunning", true);

				if (m_FacingRight)
				{
					Flip();
				}
			}
			else if (move == 0)
			{
				// Se detiene la animación
				anim.SetBool("isRunning", false);
			}

		}
		// Solo se entra en este método si el jugador desea saltar Y se encuentra en el piso
		if (m_Grounded && jump)
		{
			// Como el jugador está saltando, hacemos falso el booleano que indica si está tocando tierra
			m_Grounded = false;
			// Se le suma una fuerza vertical al jugador
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			// Se reproducen las animaciones de salto
			anim.SetTrigger("takeOf");
			anim.SetBool("isJumping", true);
			
		}
	}

	private void Flip()
	{
		// Se cambia la dirección en la que se dice que está viendo el jugador. 
		// NOTA: esta instrucción solo cambia el indicador.
		m_FacingRight = !m_FacingRight;

		// Multiplicamos la componente X de la escala local del jugador por -1 para invertir la dirección en la que mira el sprite
		Vector3 theScale = playerTransform.localScale;
		theScale.x *= -1;
		playerTransform.localScale = theScale;
	}

	public void PunchAttack()
	{
		// Obtenemos los colliders que se encuentren en la capa seleccionada, en este caso la capa Jugadores
		// Se obtienen haciendo un Overlap con un círculo desde el punchPoint con el tamaño de punchRange
		Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(punchPoint.position, stats.punchRange, playerLayer);

		// El punchVectorX es el empuje que le aplica al otro jugador caundo lo golpea.
		// Se multiplica la fuerza con el Sign (-1 o 1) del componente X de la escala del personaje para saber si el golpe va hacia la izquierda o derecha
		stats.punchVectorX = stats.punchForce * Math.Sign(playerTransform.localScale.x);


		// Si el tiempo actual es mayor o igual que el tiempo en el que se permite el siguiente golpe...
		if (Time.time >= stats.nextPunchTime)
		{
			// Creamos una variable de tipo Random para obtener un número al azar
			// Este número determina si es un golpe crítico

			System.Random rnd = new System.Random();

			// Si el numero generado al azar es igual a 0, es un golpe crítico
			// Hay un chance de 20% de que se obtenga este resultado
			// Si se consigue un crítico ApplyPunch obtiene como segundo parámetro True	
			// rnd.next(0,5) genera un numero entre 0 y 4

			// Las comprobaciones de if es para comprobar que animación se va a reproducir
			if (rnd.Next(0, 5) == 0 && !m_wasCrouching)
			{
				//Golpe crítico
				ApplyPunch(hitPlayers, true);

				// Si es un golpe crítico, se reproduce la animación de golpe crítico
				anim.SetTrigger("KnockUp");
			}
			else if(m_Grounded && !m_wasCrouching)
			{
				// Golpe normal
				ApplyPunch(hitPlayers, false);

				// Se elige una animación de ataque básico al azar
				if(rnd.Next(0, 2) == 0)
					anim.SetTrigger("Punch0");
				else
					anim.SetTrigger("Punch1");
			}
			else if(m_Grounded && m_wasCrouching)
			{
				ApplyPunch(hitPlayers, false);
				anim.SetTrigger("CrouchedPunch");
			}
			else if(!m_Grounded)
			{
				ApplyPunch(hitPlayers, false);
				anim.SetTrigger("AirPunch");
			}

			// Se le asigna al tiempo del siguiente golpe como valor el tiempo actual más el inverso de la cadencia de golpes
			// Si la cadencia de golpes es 1 por segundo, se le suma un segundo, si la cadencia de golpes es 2 golpes por segundo, se le suma 1/2 segundo y así
			stats.nextPunchTime = Time.time + 1f / stats.punchRate;
		}
	}

	// En este método se aplica el golpe
	// Se obtiene un arreglo con los colliders que hayan hecho contacto con el rango de golpe
	void ApplyPunch(Collider2D[] hitPlayers, bool critical)
	{
		// Se recorre el arreglo de colliders
		foreach (Collider2D player in hitPlayers)
		{
			// Si el collider no pertenece al jugador Y...
			// Si el collider es de tipo BoxCollider2D
			if ((player.gameObject != gameObject) && player.GetType() == typeof(BoxCollider2D))
			{
				if (critical)
				{
					// Se le suma los componentes de fuerza tanto en X como en Y al objetivo
					player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(stats.punchVectorX, stats.punchVectorX));
					// Se le aplica el daño al objetivo, este es calculado multiplicando el daño por su modificador de crítico
					player.GetComponent<PlayerStats>().TakeDamage(stats.punchDamage * stats.criticalModifier);
				}
				else
				{
					// Se le suma el componente de fuerza en X al objetivo
					player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(stats.punchVectorX, 0f));
					// Se le aplica el daño al objetivo
					player.GetComponent<PlayerStats>().TakeDamage(stats.punchDamage);
				}
			}
		}
	}

	// En este método se aplica la patada
	// Se obtiene un arreglo con los colliders que hayan hecho contacto con el rango de patada
	public void KickAttack()
	{
		// Si el tiempo actual es mayor o igual que el tiempo en el que se permite la siguiente patada...
		if (Time.time >= stats.nextKickTime)
		{
			// Obtenemos los colliders que se encuentren en la capa seleccionada, en este caso la capa Jugadores
			// Se obtienen haciendo un Overlap con un círculo desde el kickPoint con el tamaño de kickrange
			Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(kickPoint.position, stats.kickRange, playerLayer);

			// El kickVectorX es el empuje que le aplica al otro jugador caundo lo patea.
			// Se multiplica la fuerza con el Sign (-1 o 1) del componente X de la escala del personaje para saber si lampatada va hacia la izquierda o derecha
			stats.kickVectorX = stats.kickForce * Math.Sign(playerTransform.localScale.x);

			// Se recorre el arreglo de colliders
			foreach (Collider2D player in hitPlayers)
			{
				// Si el collider no pertenece al jugador Y...
				// Si el collider es de tipo BoxCollider2D
				if ((player.gameObject != gameObject) && player.GetType() == typeof(BoxCollider2D))
				{
					// Se le suma el componente de fuerza en X al objetivo
					player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(stats.kickVectorX, 0f));
					// Se le aplica el daño al objetivo
					player.GetComponent<PlayerStats>().TakeDamage(stats.kickDamage);
				}
			}
			// Se reproduce la animación de patada dependiendo si está agachado, de pie o en el aire
			if (m_Grounded && m_wasCrouching)
				anim.SetTrigger("CrouchedKick");
			else if(m_Grounded && !m_wasCrouching)
				anim.SetTrigger("Kick");
			else if (!m_Grounded)
				anim.SetTrigger("AirKick");


			// Se le asigna al tiempo de la siguiente patada como valor el tiempo actual más el inverso de la cadencia de patadas
			// Si la cadencia de patadas es 1 por segundo, se le suma un segundo, si la cadencia de patadas es 2 golpes por segundo, se le suma 1/2 segundo y así
			stats.nextKickTime = Time.time + 1f / stats.kickRate;
		}
	}

	// Este método lo utlicé para dibujar ciruclos al rededor de los punch y kick Points para saber su rango.
	void OnDrawGizmosSelected()
	{
		if (punchPoint == null)
			return;

		Gizmos.DrawWireSphere(punchPoint.position, stats.punchRange);
		Gizmos.DrawWireSphere(kickPoint.position, stats.kickRange);

	}
}