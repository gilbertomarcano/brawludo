using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;         // Es una lista de Transform de los objetivos a capturar con la cámara
    public float smoothTime = 0.5f;         // Es el tiempo en el que se suaviza la cámara
    public Vector3 offset;                  // Es el offset de la cámara
    public float minZoom = 10f;             // Es el Zoom mínimo
    public float maxZoom = 5f;              // Es el Zoom máximo
    public float zoomLimiter = 20f;         // Es el limitador de Zoom

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        //Se obtiene al compontente Camara
        cam = GetComponent<Camera>();
    }

    //El LateUpdate se ejecuta al final de cada frame, a diferencia del Update normal que es al inicio
    // Esto es perfecto para capturar movimiento ya que permite actualizar ya despues de que se hayan hecho estos
    private void LateUpdate()
    {
        // Si la cantidad de objetos es 0, retorna
        if (targets.Count == 0)
            return;

        // Al final de cada frame utilizamos estos métodos

        // Move es para mque el centro de la cámara este en el punto medio de los objetos más lejanos
        Move();
        // Zoom permite hacer el zoom necesario para ver los objetos
        Zoom();

        
    }

    void Zoom()
    {
        // Con este método se hace una interpolación con el zoom máximo, el mínimo y la Distancia más grande entre dos objetivos entre el limitante del zoom
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        // Se modifica el ortographicSize que es como un Field of View pero para 2d.

        //Se aplica otra interpolación para suavizar el zoom.
        cam.orthographicSize = Mathf.Lerp(cam.fieldOfView, newZoom, Time.time);
    }

    // Con este método obtenemosla distancia más grande entre los objetos a seguir
    float GetGreatestDistance()
    {
        // Esto crea un cuadrado al rededor de los objetos contenidos en este
        // El primer parámetro es el primer objeto y el segundo parámetro es el padding del cuadrado a generar
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        // Recorremos toda la lista de objetivos a seguir y los encapsulamos en este cuadrado
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        // Con bounds.size.x retornamos el valor de width del cuadrado, es decir la distancia más grande entre dos objetos cualquiera de la lista a seguir
        return bounds.size.x;
    }

    // Este método mueve la cámara
    void Move()
    {
        // Se obtiene el punto del medio de los objetos a seguir
        Vector3 centerPoint = GetCenterPoint();
        // Aquí le agregamos el offset que dseemos
        Vector3 newPosition = centerPoint + offset;
        // Esta posición en z la asigno para que se vea bien con las capas
        newPosition.z = -10;

        // Se suaviza la posición de la cámara con SmoothDamp
        Vector3 smoothVector = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        //transform.position = new Vector3(
        //    Mathf.Clamp(smoothVector.x, 0.88f, 6f),
        //    Mathf.Clamp(smoothVector.x, 0f, 4f),
        //    -10f
        //    );

    }


    // En este método se obtiene el punto del medio de los objetos a seguir
    Vector3 GetCenterPoint()
    {
        // Si hay solo un objeto a seguir, el centro es la posición de ese objeto
        if( targets.Count == 1)
        {
            return targets[0].position;
        }

        // Esto crea un cuadrado al rededor de los objetos contenidos en este
        // El primer parámetro es el primer objeto y el segundo parámetro es el padding del cuadrado a generar
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        // Recorremos toda la lista de objetivos a seguir y los encapsulamos en este cuadrado
        for (int i = 0; i < targets.Count ; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        // bounds.center retorna el centro de ese cuadrado que encapsula a todos los objetos
        return bounds.center;
    }
}
