using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
