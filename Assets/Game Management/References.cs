using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static ShipController thePlayer;
    public static CanvasBehaviour theCanvas;
    public static AccelerationBar theAccelerationBar;
    public static GameController theGameController;

    //Layer Masks
    public static LayerMask wallsLayer = LayerMask.GetMask("Walls and Props");
    public static LayerMask floorsLayer = LayerMask.GetMask("Floors");

    //Tags
    public static string wallsTag = "Walls and Props";

}
