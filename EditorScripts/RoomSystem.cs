using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Atfone.Drawing;

[ExecuteInEditMode]
public class RoomSystem : MonoBehaviour {

    private RoomData _roomData;
    private TileManager _tileManager;
    private Room[ ] _roomArray;

    public Transform roomParent;
    public Cell[ , ] cells = new Cell[Data.tileWidth, Data.tileHeight];
    public Room room;
    public List<Room> roomGroup = new List<Room>( );

    void Awake( )
    {
        _roomData = GameObject.FindObjectOfType(typeof(RoomData)) as RoomData;
        _tileManager = GameObject.FindObjectOfType(typeof(TileManager)) as TileManager;

        //Debug.Log(roomGroup.Count);

        //foreach(Room room in roomGroup){
        //    room.DisplayWallAndWallPaper( );
        //}

        //CreatTileGrid( );
        //CreateRoom(0, 0, 14, 10);
        //CreateRoom(5, 9, 14, 18);
        //CreateRoom(12, 3, 18, 14);
        //InitialGameSetUp( );
    }

    public void InitialRoom( )
    {
        _roomArray = roomGroup.ToArray( );
        for(int i = 0; i < _roomArray.Length - 1; i++) {
            for(int j = i + 1; j < _roomArray.Length; j++) {
                MakeRenderReasonableWithBrick(_roomArray[i], _roomArray[j]);
                //FixTileGroupAboutCrossPoints(_roomArray[i], _roomArray[j]);
            }
        }
    }

    void MakeRenderReasonableWithBrick( Room r1, Room r2 )
    {
        Point[ ] crossPoint = GetCrossPointsUsingRectangleWithTwoRoom(r1, r2);
        foreach(Tile tile in r1.wallTiles) {
            for(int i = 0; i < crossPoint.Length; i++) {
                if(crossPoint.Length <= 2 && tile.point == crossPoint[i]) {
                    tile.cell.cellType = Data.tileTypes.SolidWall;
                    tile.GetComponent<SpriteRenderer>( ).enabled = true;
                    tile.iscrossTile = true;
                    tile.GetComponent<SpriteRenderer>( ).sprite = _roomData.solidWall.sprite;
                }
                if(crossPoint.Length > 2 && tile.point == crossPoint[i]) {
                    if(IsTerminalRectanglePoint(r1, crossPoint[i]) || IsTerminalRectanglePoint(r2, crossPoint[i])) {
                        Debug.Log("match :" + crossPoint[i]);
                        tile.cell.cellType = Data.tileTypes.SolidWall;
                        tile.GetComponent<SpriteRenderer>( ).enabled = true;
                        tile.iscrossTile = true;
                        tile.GetComponent<SpriteRenderer>( ).sprite = _roomData.solidWall.sprite;
                    }
                    //else if() {

                    //}
                    else {
                        Debug.Log("don't match :" + crossPoint[i]);
                        tile.cell.cellType = Data.tileTypes.HackWall;
                        tile.GetComponent<SpriteRenderer>( ).enabled = true;
                        tile.iscrossTile = false;
                        tile.GetComponent<SpriteRenderer>( ).sprite = _roomData.hackWall.sprite;
                    }
                }
            }
        }
        foreach(Tile tile in r2.wallTiles) {
            for(int i = 0; i < crossPoint.Length; i++) {
                if(tile.point == crossPoint[i]) {
                    tile.GetComponent<SpriteRenderer>( ).enabled = false;
                    _tileManager.tileGroup.Remove(tile);
                }
            }
        }
    }

    bool IsTerminalRectanglePoint(Room room, Point point)
    {
        bool isTerminal = false;
        for(int i = 0;i < room.terminalPoint.Length; i++) {
            if(room.terminalPoint[i] == point) {
                isTerminal = true;
            }
        }
        return isTerminal;
    }

    Point[ ] GetCrossPointsUsingRectangleWithTwoRoom( Room room1, Room room2 )
    {
        List<Point> tempPointList = new List<Point>( );
        foreach(Tile tile in room1.wallTiles) {
            //Debug.Log("tile.point :" + tile.point);
            if(room2.recTangle.Contains(tile.point)) {
                foreach(Tile tile1 in room2.wallTiles) {
                    if(tile.point == tile1.point) {
                        tempPointList.Add(tile.point);
                    }
                }
            }
        }
        return tempPointList.ToArray( );
    }

    public void CreateRoom(int locationX, int locationY, int wallWidth, int wallHeight )
    {
        Room rm = Instantiate(room) as Room;

        rm.recTangle.X = locationX;
        rm.recTangle.Y = locationY;
        rm.recTangle.Width = wallWidth;
        rm.recTangle.Height = wallHeight;

        rm.terminalPoint[0] = new Point(locationX, locationY);
        rm.terminalPoint[1] = new Point(locationX + wallWidth - 1, locationY);
        rm.terminalPoint[2] = new Point(locationX, locationY + wallHeight - 1);
        rm.terminalPoint[3] = new Point(locationX + wallWidth - 1, locationY + wallHeight - 1);

        rm.transform.parent = roomParent;
        roomGroup.Add(rm);
        rm.DisplayWallAndWallPaper( );
    }

    //void FixTileGroupAboutCrossPoints( Room r1, Room r2 )
    //{
    //    Point[ ] crossPoint = GetCrossPointsUsingRectangleWithTwoRoom(r1, r2);
    //    foreach(Tile tile in _tileManager.tileGroup) {
    //        for(int i = 0; i < crossPoint.Length; i++) {
    //            if(tile.point == crossPoint[i]) {
    //                tile.cell.cellType = Data.tileTypes.SolidWall;
    //                tile.iscrossTile = true;
    //            }
    //        }
    //    }
    //}

    //public void CreatTileGrid( )
    //{
    //    for(int x = 0; x < Data.tileWidth; x++) {
    //        for(int y = 0; y < Data.tileHeight; y++) {
    //            cells[x, y] = new Cell( );
    //        }
    //    }
    //}
}
