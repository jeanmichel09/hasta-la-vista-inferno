using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy_controller : MonoBehaviour
{
    public event EventHandler<EnemyOnShootEventArgs> EnemyOnShoot;
    public class EnemyOnShootEventArgs : EventArgs
    {
        public Vector3 saida;
        public Vector3 shootPosition;
    }
    [SerializeField] private float fireRate;

    Animator myAnim;

    public AIPath aIPath;
    public AIDestinationSetter destinationSetter;
    private Transform aimTransform;
    private Transform aimSaidaTransform;
    private float nextShootTime;

    Vector3 direction;
    Vector3 startingPosition;
    Vector3 roamPosition;

    Player player;

    private State currentState;
    private enum State
    {
        Roaming,
        ChaseTarget,
        Attacking,
        GoingBackToStart
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        currentState = State.Roaming;
        player = FindObjectOfType<Player>();

        aimTransform = transform.Find("Aim");
        aimSaidaTransform = aimTransform.Find("Saida");

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(5f, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        var playerPos = player.GetPosition();
        switch (currentState)
        {
            case State.Roaming:
                destinationSetter.target.position = roamPosition;

                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    //Reached roam position
                    roamPosition = GetRoamingPosition();
                }
                FaceVelocity();
                FindTarget();
                break;
            case State.ChaseTarget:
                FaceVelocity();
                destinationSetter.target.position = playerPos;

                float attackRange = 10f;
                if(Vector3.Distance(transform.position, playerPos) < attackRange)
                {
                    //Within attack range
                    if(Time.time > nextShootTime)
                    {
                        Debug.Log("Attacking!");
                        EnemyOnShoot?.Invoke(this, new EnemyOnShootEventArgs
                        {
                            saida = aimSaidaTransform.position,
                            shootPosition = playerPos,
                        }) ;
                        fireRate = 1f;
                        nextShootTime = Time.time + fireRate;
                    }
                }
                float stopChasingDistance = 20f;
                if(Vector3.Distance(transform.position, playerPos) > stopChasingDistance)
                {
                    currentState = State.GoingBackToStart;
                }
                break;
            case State.GoingBackToStart:
                reachedPositionDistance = 10f;
                destinationSetter.target.position = startingPosition;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                {
                    currentState = State.Roaming;
                }
                break;


        }
        //Debug.Log(currentState);
        
    }

    private void FaceVelocity()
    {


        aIPath.enableRotation = false;
        direction = aIPath.desiredVelocity;
        //transform.up = direction;
        
        if (direction != Vector3.zero)
        {
            aIPath.rotation = Quaternion.LookRotation(Vector3.zero);
        }
        myAnim.SetFloat("moveX", direction.x);
        myAnim.SetFloat("moveY", direction.y);
        myAnim.SetFloat("speed", direction.magnitude);

    }
    private void FindTarget()
    {
        float targetRange = 15f;
        var playerPos = player.GetPosition();
        if (Vector3.Distance(transform.position, playerPos ) < targetRange)
        {
            //Player within target range
            currentState = State.ChaseTarget;
        }
    }
}

