using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Transform[] checkers;
    public PlayerMovement player;

     bool forward;
     bool backwards;
     bool left;
     bool right;

    public LayerMask groundMask;
    public float groundDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.forward = Physics.CheckSphere(checkers[0].position, groundDistance, groundMask);
        player.backwards = Physics.CheckSphere(checkers[1].position, groundDistance, groundMask);
        player.left = Physics.CheckSphere(checkers[2].position, groundDistance, groundMask);
        player.right = Physics.CheckSphere(checkers[3].position, groundDistance, groundMask);
    }
}
