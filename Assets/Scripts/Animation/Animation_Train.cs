using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Train : MonoBehaviour
{

    Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Active");
    }

}
