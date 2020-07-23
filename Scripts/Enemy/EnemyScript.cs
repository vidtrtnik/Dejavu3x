using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float defaultPosX = 35.6f;
    public float distance = 0.0f;

    public float followRange = 9.0f;
    public float attackRange = 2.0f;

    public bool guard = false;
    public bool move = false;
    public bool attack = false;

    public bool backOff = false;

    Animator m_Animator;
    CharacterController m_CharacterController;

    public GameObject checkpoint;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        m_Animator = gameObject.GetComponent<Animator>();
        m_CharacterController = gameObject.GetComponent<CharacterController>();

        m_Animator.SetBool("combat", true);
        guard = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Mathf.Abs(transform.position.x - Player.transform.position.x);

        if((distance < followRange) && !backOff)
        {
            if(distance < attackRange)
            {
                m_Animator.SetBool("attack", true);
                m_Animator.SetBool("move", false);
                attack = true;
                move = false;
                Attack();
            }
            else // Move towards Player
            {
                m_Animator.SetBool("move", true);
                m_Animator.SetBool("attack", false);
                move = true;
                attack = false;
                EnemyMove(-1.0f);
            }
        }
        else if (Player.transform.position.x < checkpoint.transform.position.x)
        {
            m_Animator.SetBool("move", false);
            m_Animator.SetBool("attack", false);
            move = false;
            attack = false;

            m_Animator.SetBool("combat", true);
            guard = true;

            Vector3 defaultPos = new Vector3(defaultPosX, transform.position.y, transform.position.z);
            transform.position = defaultPos;
        }
        else
        {
            m_Animator.SetBool("move", false);
            m_Animator.SetBool("attack", false);
            move = false;
            attack = false;
        }

        if(backOff)
        {
            m_Animator.SetBool("move", false);
            m_Animator.SetBool("attack", false);
            move = false;
            attack = false;

            backOff = true;
            m_Animator.SetBool("backOff", true);
            EnemyMove(+1.0f);

            if (!Player.GetComponent<Respawn>().respawnRequest)
            {
                backOff = false;
                m_Animator.SetBool("backOff", false);
            }
        }
    }

    void EnemyMove(float speed)
    {
        Vector3 moveVector = new Vector3(speed, 0.0f, 0.0f);
        m_CharacterController.Move(moveVector * Time.fixedDeltaTime);
    }

    void Attack()
    {

    }
}
