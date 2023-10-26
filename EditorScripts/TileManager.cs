using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TileManager : MonoBehaviour
{
    public List<Tile> tileGroup = new List<Tile>( );
    private EnemyManager _enemyManager;
    private RoomSystem _roomSystem;
    private TestHero _testHero;
    //public Text tilegroupCount;
    //public Text allTile;

    void Awake( )
    {
        //allTile.text = "debug0";
        _testHero = GameObject.FindObjectOfType(typeof(TestHero)) as TestHero;
        _enemyManager = GameObject.FindObjectOfType(typeof(EnemyManager)) as EnemyManager;
        _roomSystem = GameObject.FindObjectOfType(typeof(RoomSystem)) as RoomSystem;
    }

    void Start( )
    {
        //foreach(Room room in _roomSystem.roomGroup) {
        //    for(int i = 0; i < room.terminalPoint.Length; i++) {
        //        tilegroupCount.text += room.terminalPoint[i].ToString( );
        //    }
        //}
        //allTile.text = "debug1";
        SetNeighborTiles( );
        //allTile.text = "debug2";
        //tilegroupCount.text = tileGroup.Count.ToString( );
        //foreach(Tile tile in tileGroup) {
        //    if(tile.rightTile != null) {
        //        allTile.text += tile.rightTile.ToString( );
        //    }
        //}

        SetConnectionByAllSide( );
        //allTile.text = "debug3";
        CheckHeroPositionAndLookAtPath( );
        //allTile.text = "debug4";
        _enemyManager.CheckEnemyPosition( );
        //allTile.text = "debug5";


    }

    public void SetNeighborTiles( )
    {
        foreach(Tile tile in tileGroup) {
            foreach(Tile tile1 in tileGroup) {
                if(tile != tile1) {
                    if(tile.point.x == tile1.point.x && tile.point.y == tile1.point.y + 1) {
                        tile.bottomTile = tile1;
                        tile1.topTile = tile;
                    }
                    if(tile.point.x == tile1.point.x && tile.point.y + 1 == tile1.point.y) {
                        tile.topTile = tile1;
                        tile1.bottomTile = tile;
                    }
                    if(tile.point.y == tile1.point.y && tile.point.x == tile1.point.x + 1) {
                        tile.leftTile = tile1;
                        tile1.rightTile = tile;
                    }
                    if(tile.point.y == tile1.point.y && tile.point.x + 1 == tile1.point.x) {
                        tile.rightTile = tile1;
                        tile1.leftTile = tile;
                    }
                }
            }
        }
    }

    public void SetConnectionByAllSide( )
    {
        foreach(Tile tile in tileGroup) {
            tile.SetConnectionBySide( );
            tile.SetPositionBySide( );
        }
    }

    public void CheckHeroPositionAndLookAtPath( )
    {
        Vector3 selfPosition = _testHero.transform.position;
        float checkDistence = 0;
        float minDistence = 100.0f;
        Tile checkTile = null;
        Vector3 rightPosition;

        foreach(Tile tile in tileGroup) {
            checkDistence = (selfPosition - tile.transform.position).sqrMagnitude;
            if(checkDistence <= minDistence) {
                minDistence = checkDistence;
                checkTile = tile;
            }
        }
        _testHero.currentSide = checkTile.topSide;
        selfPosition = _testHero.currentSide.position;
        rightPosition = _testHero.currentSide.rightPosition;
        transform.right = (rightPosition - selfPosition).normalized;
    }


}
