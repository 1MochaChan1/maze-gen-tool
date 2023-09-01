using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    public GameObject mesh;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void DestroyWall()
    {
        mesh.SetActive(false);
    }
}
