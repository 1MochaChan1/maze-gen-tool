using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chest : MonoBehaviour, IInteractable
{
    public GameObject lid;
    public bool open;
    [Range(0f, 1f)] public float openSpeed;
    
    float timeCount = 0f;
    Quaternion start = Quaternion.Euler(new Vector3(-90, 0, 0));
    Quaternion end = Quaternion.Euler(new Vector3(-180, -90, 90));

    private void Start()
    {
        open= false;
        openSpeed= 1f;
    }

    public void Interact()
    {
        
        timeCount = 0f;
        open = !open;
    }
    public void Update()
    {
        if (timeCount > 1f) return;
        if (open)
        {
            //lid.transform.Rotate(Vector3.up, 90f);
            lid.transform.rotation = Quaternion.Lerp(lid.transform.rotation, end, timeCount * openSpeed);
            timeCount = Time.deltaTime + timeCount;

        }

        else if (!open)
        {
            //lid.transform.Rotate(Vector3.up, -90f);
            lid.transform.rotation = Quaternion.Lerp(lid.transform.rotation, start, timeCount * openSpeed);
            timeCount = Time.deltaTime + timeCount;


        }
    }
}
