using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomHelpers;
using UnityEngine.UIElements;

public class BobsMazeGen : MonoBehaviour
{
    # nullable enable
    private Dictionary<Vector3Int, Pair<Wall?, Cell?>> board = new Dictionary<Vector3Int, Pair<Wall?, Cell?>>();
    private List<Cell> visited = new List<Cell>();
    private Stack<Cell> stack = new Stack<Cell>();

    private List<Vector3Int> selectedRoomCoords = new List<Vector3Int>();
    private List<List<Vector3Int>> selectedRooms = new List<List<Vector3Int>>();

    public int mazeSize = 12;
    public int maxRoomSize = 8;
    public int maxNumberOfRooms = 2;

#nullable enable
    public Cell? cell;
    public Wall? wall;

    void Start()
    {
        for (int z = 0; z < mazeSize; z++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if (wall)
                {
                    Vector3Int wallPos = new Vector3Int(x, 0, z);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    Wall wallInst = Instantiate(wall, wallPos, Quaternion.identity);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    board.Add(wallPos, new Pair<Wall?, Cell?>(wallInst, null));
                }
               
            }
        }
        CreateRooms();
    }

    void Update()
    {
        
    }

    void CreateRooms()
    {
        int width = UnityEngine.Random.Range(0,maxRoomSize);
        int length = UnityEngine.Random.Range(0, maxRoomSize);

        // pick random location
        Vector3Int loc = new Vector3Int(UnityEngine.Random.Range(0, mazeSize), 0, UnityEngine.Random.Range(0, mazeSize));

        while (selectedRooms.Count < maxNumberOfRooms)
        {

            if (selectedRoomCoords.Contains(loc)) { continue ; }
           List<Vector3Int> tempRooms = new List<Vector3Int>();
            int initialLength = selectedRoomCoords.Count;

            for (int z = 0;z < length;z++)
            {
                loc = new Vector3Int(loc.x, 0, z + loc.z);
                for (int x = 0;x < width;x++) {
                    if (selectedRoomCoords.Contains(loc)) { 
                        loc = new Vector3Int(x + loc.x, 0, loc.z); continue; 
                    }

                    else
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        Cell cellInst = Instantiate(cell, loc, Quaternion.identity);
                        if (!board[loc].First) { continue; }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        board[loc].First.DestroyWall();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        board[loc].First = null;
                        board[loc].Second = cell;
                        selectedRoomCoords.Add(loc);
                        tempRooms.Append(loc);
                    }
                    loc = new Vector3Int(x + loc.x, 0, loc.z);
                }
            }
            selectedRooms.Add(tempRooms);
            if (selectedRooms.Count == initialLength) { continue; }

        }
    }
}
