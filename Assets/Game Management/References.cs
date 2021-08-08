using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public static class References
{
    public static ShipController thePlayer;
    public static CanvasBehaviour theCanvas;
    public static AccelerationBar theAccelerationBar;
    public static GameController theGameController;
    public static Camera theCamera;
    public static EnemyRailsBehaviour theEnemyDolly;
    public static LevelManager theLevelManager;
    public static Persistent essentials;

    //Layer Masks
    public static LayerMask wallsLayer = LayerMask.GetMask("Walls and Props");
    public static LayerMask floorsLayer = LayerMask.GetMask("Floors");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");

    //Tags
    public static string wallsTag = "Walls and Props";
    public static string enemiesTag = "Enemy";

    //Variables
    public static float maxLevelDistance = 6000;

}
