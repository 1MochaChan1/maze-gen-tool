using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CellBlock : MonoBehaviour
{
   

    [SerializeField]
    public WallDirection[] pathDirection;

    public float pathSize = 1.0f;
    public float wallWidth = 1.0f;

    public GameObject path;
    public GameObject[] walls;

    /// <summary>
    /// The Position it occupies in the grid's X-Axis (rows)
    /// (These are not position of the objects in the World)
    /// </summary>
    public int x;

    /// <summary>
    /// The Position it occupies in the grid's Y-Axis (Columns)
    /// (These are not position of the objects in the World)
    /// </summary>
    public int z;
    public enum WallDirection
    {
            NORTH=0, WEST=1, SOUTH=2, EAST=3
    }

    public CellBlock() { 
        pathDirection = new WallDirection[4];
        walls = new GameObject[4];
    }

    public CellBlock(int x, int z)
    {
        this.x = x;
        this.z = z;
        pathDirection = new WallDirection[4];
        walls = new GameObject[4];
    }

    [SerializeField]
    private bool _visited;
    public bool visited
    {
        get { return _visited; }
        set
        {
            _visited = value;
            Material pathMat = path.GetComponent<MeshRenderer>().material;
            if (_visited==true)
            {
                pathMat.color = Color.cyan;
            }
            else
            {
                pathMat.color = Color.white;
            }
        }
    }

    private void Start()
    {
        for (var i = 0; i < walls.Length; i++)
        {
            Material mat = walls[i].GetComponent<MeshRenderer>().material;

            switch (i)
            {
                case 0:
                    mat.color = Color.red; break;
                case 1:
                    mat.color = Color.green; break;
                case 2:
                    mat.color = Color.blue; break;
            }
        }
    }

   

    public void AddPathDirection(CellBlock.WallDirection dir)
    {
        switch (dir)
        {
            case WallDirection.NORTH:
                pathDirection[0]=dir;
                break;
            case WallDirection.EAST:
                pathDirection[1]=dir;
                break;
            case WallDirection.SOUTH:
                pathDirection[2]=dir;
                break;
            case WallDirection.WEST:
                pathDirection[3]=dir;
                break;
        }
    }

    public void RemoveWall(CellBlock.WallDirection dir)
    {
        switch (dir)
        {
            case WallDirection.NORTH:
                walls[0].SetActive(false);
                break;
            case WallDirection.EAST:
                walls[1].SetActive(false);
                break;
            case WallDirection.SOUTH:
                walls[2].SetActive(false);
                break;
            case WallDirection.WEST:
                walls[3].SetActive(false);
                break;
        }
    }
}
