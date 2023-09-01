using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    void Interact();
}



public class Interactor : MonoBehaviour
{

  

    public Transform source;
    public float range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray ray = new Ray(source.position, source.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }

            
        }
        Debug.DrawRay(source.position, source.forward);
    }

    //private void OnDrawGizmos()
    //{
    //    Ray ray = new Ray(source.position, source.forward);
    //    if(Physics.Raycast(ray,out RaycastHit hitInfo, range))
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(hitInfo.point, 0.1f);
    //    }
    //}
}
