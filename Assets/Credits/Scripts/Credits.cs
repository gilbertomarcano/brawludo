using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    RectTransform rectTransform;
    private float yPosition;
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
        yPosition = 0f;
        rectTransform = GameObject.Find("ContentCredits").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectTransform.localPosition = new Vector3(0, yPosition += velocidad, 0);
    }

    public void Back()
    {
        StartMenu.Load("StartMenu");
    }
}
