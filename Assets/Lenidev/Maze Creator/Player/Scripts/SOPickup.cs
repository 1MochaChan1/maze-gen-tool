using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SOPickup", menuName ="SOs/Pickup")]
public class SOPickup : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;

}
