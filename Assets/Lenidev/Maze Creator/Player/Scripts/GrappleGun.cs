using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 grapplePoint;
    private SpringJoint joint;


    public LayerMask grappleableMask;
    public Transform muzzle, cam, player;
    public float maxDistance;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            StartGrapple();
        }
        else if(Input.GetMouseButtonUp(0)) { StopGrapple(); }
    }


    private void LateUpdate()
    {
        DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleableMask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = 0f;
            joint.minDistance = 0f;

            joint.spring = distanceFromPoint * .25f;
            joint.damper = distanceFromPoint * .05f;
            joint.massScale = 1f;

            lineRenderer.positionCount = 2;
        }
    }


    void DrawRope()
    {
        if (!joint) return;
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, grapplePoint);
        
    }

    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
