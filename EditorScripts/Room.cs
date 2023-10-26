using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Atfone.Drawing;

[ExecuteInEditMode]
public class Room : MonoBehaviour
{
    private RoomData _roomData;
    private RoomSystem _roomSystem;
    private TileManager _tileManager;
    public Rectangle recTangle;
    public Point[ ] terminalPoint = new Point[4];
    public List<Tile> wallTiles = new List<Tile>( );
    public Transform wallParent;

    void Awake( )
    {
        _roomData = GameObject.FindObjectOfType(typeof(RoomData)) as RoomData;
        _roomSystem = GameObject.FindObjectOfType(typeof(RoomSystem)) as RoomSystem;
        _tileManager = GameObject.FindObjectOfType(typeof(TileManager)) as TileManager;
    }

    void Start( )
    {

    }

    public void DisplayWallAndWallPaper( )
    {
        for(int y = 0; y < recTangle.Height; y++) {
            for(int x = 0; x < recTangle.Width; x++) {
                if(x == 0 || x == recTangle.Width - 1) {
                    if(y == 0 || y == recTangle.Height - 1) {
                        SpriteRenderer sr = GetWallAndSetTransform(_roomData.solidWall, x, y);
                        SetWallTile(sr, Data.tileTypes.SolidWall, x, y);
                    }
                    else {
                        SpriteRenderer sr = GetWallAndSetTransform(_roomData.hackWall, x, y);
                        SetWallTile(sr, Data.tileTypes.HackWall, x, y);
                    }
                }
                else {
                    if(y == 0 || y == recTangle.Height - 1) {
                        SpriteRenderer sr = GetWallAndSetTransform(_roomData.hackWall, x, y);
                        SetWallTile(sr, Data.tileTypes.HackWall, x, y);
                    }
                    else {
                        if(x % 4 == 0 && y % 4 == 0) {
                            SpriteRenderer sr = GetWallPaperAndSetTransform(_roomData.wallPaper, x, y);
                            //SetWallTile(sr, Data.tileTypes.WallPaper, x, y);
                        }
                    }
                }
            }
        }
    }

    private SpriteRenderer GetWallAndSetTransform( SpriteRenderer sr1, int x, int y )
    {
        SpriteRenderer sr = Instantiate(sr1) as SpriteRenderer;
        sr.transform.parent = wallParent;
        sr.transform.localPosition = new Vector3((recTangle.X + x) * _roomData.cellWidth, (recTangle.Y + y) * _roomData.cellHeight, 0f);
        return sr;
    }

    private SpriteRenderer GetWallPaperAndSetTransform( SpriteRenderer sr1, int x, int y )
    {
        SpriteRenderer sr = Instantiate(sr1) as SpriteRenderer;
        sr.transform.parent = wallParent;
        sr.transform.localPosition = new Vector3(0.8f + ((recTangle.X + x - 4) * _roomData.cellWidth), 0.8f + ((recTangle.Y + y - 4) * _roomData.cellHeight), 0f);
        return sr;
    }

    private void SetWallTile( SpriteRenderer sr, Data.tileTypes dt, int x, int y )
    {
        Tile tile = sr.GetComponent<Tile>( );
        tile.cell = new Cell( );
        tile.cell.cellType = dt;
        tile.editorPoint = new Point(recTangle.X + x, recTangle.Y + y);
        wallTiles.Add(tile);
        _tileManager.tileGroup.Add(tile);
        //CheckOnlyOneTileCanBeAddedToTileGroup(tile);

        if(dt == Data.tileTypes.HackWall) {
            tile.iscrossTile = false;
        }
        else {
            tile.iscrossTile = true;
        }
    }

    private void CheckOnlyOneTileCanBeAddedToTileGroup( Tile tile )
    {
        int temp = 0;
        foreach(Tile tile1 in _tileManager.tileGroup) {
            if(tile1 != null && tile1.point == tile.point) {
                temp++;
            }
        }
        if(temp == 0) {
            _tileManager.tileGroup.Add(tile);

        }
    }

    private Point[ ] WallPath( bool isClockwise )
    {
        List<Point> pointList = new List<Point>( );
        Point[ ] pointArray;
        for(int x = 0; x < recTangle.Width; x++) {
            pointList.Add(new Point(x, 0));
        }
        for(int y = 1; y < recTangle.Height; y++) {
            pointList.Add(new Point(recTangle.Width - 1, y));
        }
        for(int x = recTangle.Width - 2; x >= 0; x--) {
            pointList.Add(new Point(x, recTangle.Height - 1));
        }
        for(int y = recTangle.Height - 2; y >= 1; y--) {
            pointList.Add(new Point(0, y));
        }
        pointArray = pointList.ToArray( );
        if(isClockwise) {
            Array.Reverse(pointArray);
        }
        return pointArray;
    }

    private Vector3[ ] TilePointToVector( Point[ ] pointArray )
    {
        Vector3[ ] tempVector = new Vector3[pointArray.Length];
        for(int i = 0; i < pointArray.Length; i++) {
            tempVector[i] = new Vector3(-4.0f + pointArray[i].X * _roomData.cellWidth, pointArray[i].Y * _roomData.cellHeight, 0f);
        }
        return tempVector;
    }
}
