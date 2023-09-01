using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public SOPickup pickupObject;
    

    private void OnTriggerEnter(Collider other)
    {
        var _ = other.gameObject.transform.parent;
        
        if(_.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.Pick(pickupObject);
            Destroy(gameObject);
        }
    }
}
