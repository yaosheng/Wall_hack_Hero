using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomSystem))]
public class RoomSystemEditor : Editor {

    public int roomWidth;
    public int roomHeight;
    public int crossType;
    public int wallpaperType;

    public TileManager _tileManager
    {
        get {
            TileManager tm = GameObject.FindObjectOfType(typeof(TileManager)) as TileManager;
            return tm;
        }
    }

    public override void OnInspectorGUI( )
    {
        RoomSystem roomSystem = (RoomSystem)target;
        base.OnInspectorGUI( );
        GUILayout.BeginVertical( );

        GUILayout.BeginHorizontal( );
        roomWidth = EditorGUILayout.IntField("Room Width", roomWidth);
        crossType = EditorGUILayout.IntField("Cross Type", crossType);
        GUILayout.EndHorizontal( );

        GUILayout.BeginHorizontal( );
        roomHeight = EditorGUILayout.IntField("Room Height", roomHeight);
        wallpaperType = EditorGUILayout.IntField("Wall Paper Type", wallpaperType);
        GUILayout.EndHorizontal( );

        GUILayout.EndVertical( );

        GUILayout.BeginHorizontal( );
        if(GUILayout.Button("Create Room")) {
            roomSystem.CreateRoom(0, 0, roomWidth, roomHeight);
        }
        if(GUILayout.Button("Delete Room")) {
            Room[ ] rooms = roomSystem.roomParent.GetComponentsInChildren<Room>( );
            //Debug.Log("rooms length : " + rooms.Length);
            for(int i = 0; i < rooms.Length; i++) {
                DestroyImmediate(rooms[i].gameObject);
            }

            _tileManager.tileGroup.Clear( );
            roomSystem.roomGroup.Clear( );
        }
        GUILayout.EndHorizontal( );

        GUILayout.BeginHorizontal( );
        if(GUILayout.Button("Initial Room")) {
            roomSystem.InitialRoom( );
            //_tileManager.SetNeighborTiles( );
            //_tileManager.SetConnectionByAllSide( );
            //_tileManager.CheckHeroPositionAndLookAtPath( );
        }
        GUILayout.EndHorizontal( );
    }
}
