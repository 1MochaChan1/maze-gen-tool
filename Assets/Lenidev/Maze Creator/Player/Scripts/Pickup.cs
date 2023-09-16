using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public SOPickup pickupObject;
    public float speed;

    float speedMultiplier=0.01f;
    bool triggered=false;
    Player _player;


    public GameObject target;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (triggered & _player)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * speedMultiplier);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!triggered)
        {
            var _ = other.gameObject.transform.parent;

            
            if (_.TryGetComponent<Player>(out Player player))
            {
                _player = player;
                triggered = true;
                //player.Pick(pickupObject);
                //Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        var _ = collision.gameObject;

        if (_.TryGetComponent<Player>(out Player player))
        {
            player.Pick(pickupObject);
            Destroy(gameObject);
        }
    }
}
