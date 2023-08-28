using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform Player;
    public Transform Receiver;

    public bool playerIsOverlapping = false;
    public float dotProduct;
    public float rotationDiff;

    public bool ReceivingTP = false;
    private bool exiting;
    private float resetTime;
    public float resetTimeMax = 2f;
    public bool wantOffset = false;

    private void Start()
    {
        resetTime = resetTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping && !ReceivingTP)
        {
            Vector3 portalToPlayer = Player.position - transform.position;
            dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //If this is true: The player hs moved across the portal
            if(dotProduct < 0f || dotProduct > 0f && !ReceivingTP)
            {
                if(Receiver.GetComponent<PortalTeleporter>())Receiver.GetComponent<PortalTeleporter>().ReceivingTP = true;
                //tp
                rotationDiff = -Quaternion.Angle(transform.rotation, Receiver.transform.rotation);
                rotationDiff += 180;
                Player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                Player.position = Receiver.transform.position + positionOffset;

                playerIsOverlapping = false;
            }

        }


        if (exiting)
        {
            resetReceiving();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exiting = true;
            playerIsOverlapping = false;
        }
    }

    private void resetReceiving()
    {
        if(resetTime <= 0)
        {
            ReceivingTP = false;
            exiting = false;
            resetTime = resetTimeMax;
        }
        else
        {
            resetTime -= Time.deltaTime;
        }


    }
}
