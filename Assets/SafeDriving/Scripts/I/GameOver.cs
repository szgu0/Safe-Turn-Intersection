using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool isGameOver;

    public GameObject RestartObj_1;
    public GameObject QuitObj_1;

    public GameObject RestartObj_2;
    public GameObject QuitObj_2;

    public GameObject RestartObj_3;
    public GameObject QuitObj_3;

    public GameObject W_1;
    public GameObject W_2;
    public GameObject W_3;

    //public GameObject Car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Debug.Log("Gameover");

            RestartObj_1.SetActive(true);
            QuitObj_1.SetActive(true);
            W_1.SetActive(true);

            RestartObj_2.SetActive(true);
            QuitObj_2.SetActive(true);
            W_2.SetActive(true);

            RestartObj_2.SetActive(true);
            QuitObj_2.SetActive(true);
            W_3.SetActive(true);

            //Car.transform.rotation = Quaternion.Euler(0f, -60f, 0f);
            

            isGameOver = true;
        }
    }
}
