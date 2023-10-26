using UnityEngine;
using System.Collections;
using UnityEditor;
using Atfone.Drawing;
using System.Collections.Generic;

[CustomEditor(typeof(Room))]
public class RoomDragEditor : Editor
{
    public override void OnInspectorGUI( )
    {
        base.OnInspectorGUI( );
    }

    void OnSceneGUI( )
    {
        Room room = (Room)target;
        int offsetX = 0;
        int offsetY = 0;
        if(Event.current.type == EventType.MouseUp && Event.current.button == 0) {
            Debug.Log("Left-Mouse Up");

            for(int j = 0; j < 70; j++) {
                for(int i = 0; i < 32; i++) {
                    if(room.transform.position.x < -5.12f + (0.32f * i) + (0.32f / 2) && room.transform.position.x >= -5.12f + (0.32f * i)) {
                        room.transform.position = new Vector3(-5.12f + (0.32f * i), room.transform.position.y, 0);
                        offsetX = i;
                    }
                    else if(room.transform.position.x >= -5.12f + (0.32f * i) + (0.32f / 2) && room.transform.position.x < -5.12f + (0.32f * (i + 1))) {
                        room.transform.position = new Vector3(-5.12f + (0.32f * (i + 1)), room.transform.position.y, 0);
                        offsetX = i + 1;
                    }
                    else if(room.transform.position.y < -8.96f + (j * 0.32f) + (0.32f / 2) && room.transform.position.y >= -8.96f + (j * 0.32f)) {
                        room.transform.position = new Vector3(room.transform.position.x, -8.96f + (j * 0.32f), 0);
                        offsetY = j;
                    }
                    else if(room.transform.position.y >= -8.96f + (j * 0.32f) + (0.32f / 2) && room.transform.position.y < -8.96f + ((j + 1) * 0.32f)) {
                        room.transform.position = new Vector3(room.transform.position.x, -8.96f + ((j + 1) * 0.32f), 0);
                        offsetY = j + 1;
                    }
                }
            }
            Debug.Log("offsetX : " + offsetX + " , " + "offsetY : " + offsetY);
            GetTileByPoint(new Point(offsetX, offsetY), room.wallTiles);
            room.recTangle.X = offsetX;
            room.recTangle.Y = offsetY;

            for(int i = 0; i < room.terminalPoint.Length; i++) {
                room.terminalPoint[i] = new Point(room.terminalPoint[i].X + offsetX, room.terminalPoint[i].Y + offsetY);
            }
        }
    }

    void GetTileByPoint(Point point, List<Tile> tiles)
    {
        foreach(Tile tile in tiles) {
            tile.point = new Point(tile.editorPoint.x + point.x, tile.editorPoint.y + point.y);
        }
    }
}