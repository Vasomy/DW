using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanJamGod : MonoBehaviour
{
    public SpriteRenderer sr;
    public Animator animator;
    


    enum States
    {
        Idle,
        Searching,
        Holding
    }

    States states = States.Idle;

    private void Update()
    {
             
    }


}
