using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniContrl : MonoBehaviour
{
    public Animator normal_ani;
    public Animator sport_ani;
    public Animator truck_ani;
    public Animator small_truck_ani;
    public Animator toyota_ani;
    // Start is called before the first frame update
    void Start()
    {
        normal_ani.SetBool("start", false);
        sport_ani.SetBool("start", false);
        truck_ani.SetBool("start", false);
        small_truck_ani.SetBool("start", false);
        toyota_ani.SetBool("start", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        normal_ani.SetBool("start", true);
        sport_ani.SetBool("start", true);
        truck_ani.SetBool("start", true);
        small_truck_ani.SetBool("start", true);
        toyota_ani.SetBool("start", true);
    }
}
