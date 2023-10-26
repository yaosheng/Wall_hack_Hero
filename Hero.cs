//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;

//public class Hero : MonoBehaviour
//{
//    private float _walkSpeed = 1.0f;
//    private int _pathPoint = 0;
//    private RoomSystem _roomSystem;
//    private TileManager _tileManager;
//    private Tile _checkTile;
//    private Tile _targetTile;

//    public SpriteRenderer heroSprite;

//    void Awake( )
//    {
//        _roomSystem = GameObject.FindObjectOfType(typeof(RoomSystem)) as RoomSystem;
//        _tileManager = GameObject.FindObjectOfType(typeof(TileManager)) as TileManager;
//    }

//    void Start( )
//    {
//        CheckHeroPositionAndLookAtRoad( );
//        _targetTile = _checkTile;
//    }

//    void Update( )
//    {
//        WalkingByRoad( );
//    }

//    void CheckHeroPositionAndLookAtRoad( )
//    {
//        Vector3 selfPosition = transform.position;
//        float checkDistence = 0;
//        float minDistence = 100.0f;

//        foreach(Tile tile in _tileManager.tileGroup) {
//            checkDistence = Vector3.Distance(selfPosition, tile.transform.position);
//            if(checkDistence < minDistence) {
//                minDistence = checkDistence;
//                _checkTile = tile;
//            }
//        }
//        transform.right = _checkTile.transform.position - transform.position;
//    }



//    void WalkingByRoad( )
//    {
//        Tile[ ] neighborTiles = GetNeighborTiles(_targetTile);
//        Vector3[ ] neighborVectors = GetNeighborVectors(_targetTile, neighborTiles);
//        int targetIndex = GetMatchVectorIndex(neighborVectors);

//        Debug.Log("_targetTile point:" + _targetTile.point.ToString( ));
//        for(int i = 0; i < neighborTiles.Length; i++) {
//            Debug.Log("neighborTiles : " + i + ":" + neighborTiles[i].point.ToString( ));
//        }
//        Debug.Log("targetIndex :" + targetIndex);

//        transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);

//        if(Vector3.Distance(transform.position, _targetTile.transform.position) < 0.05f) {
//            _targetTile = neighborTiles[targetIndex];
//            Vector3 dir = _targetTile.transform.position - transform.position;
//            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            //rotation = Quaternion.Lerp(transform.rotation, rotation, 10000 * Time.deltaTime);
//            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//            //Debug.Log(_targetTile.point.ToString( ));
//        }
//    }

//    Tile[ ] GetNeighborTiles( Tile tile1 )
//    {
//        List<Tile> pointList = new List<Tile>( );
//        foreach(Tile tile in _tileManager.tileGroup) {
//            if((tile.point.x == tile1.point.X && Mathf.Abs(tile.point.Y - tile1.point.Y) == 1) ||
//                (tile.point.Y == tile1.point.Y && Mathf.Abs(tile.point.x - tile1.point.X) == 1)) {
//                pointList.Add(tile);
//            }
//        }
//        return pointList.ToArray( );
//    }

//    Vector3[ ] GetNeighborVectors(Tile tile, Tile[] tileArray)
//    {
//        Vector3[ ] neighborVectors = new Vector3[tileArray.Length];
//        for(int i = 0; i < tileArray.Length; i++) {
//            neighborVectors[i] = tileArray[i].transform.position - tile.transform.position;
//        }
//        return neighborVectors;
//    }

//    int GetMatchVectorIndex(Vector3[] neighborVectors)
//    {
//        Vector3 selfPosition = transform.position;
//        Vector3 heroVector = heroSprite.transform.position - selfPosition;
//        int targetInt = 0;
//        for(int i = 0; i < neighborVectors.Length - 1; i++) {
//            for(int j = i + 1; j < neighborVectors.Length; j++) {
//                if(Vector3.Dot(neighborVectors[i], heroVector) <= Vector3.Dot(neighborVectors[j], heroVector)) {
//                    targetInt = j;
//                }
//                else {
//                    targetInt = i;
//                }
//            }
//        }
//        return targetInt;
//    }


//    //Vector3 selfPosition = transform.position;
//    //Vector3 heroVector = heroSprite.transform.position - selfPosition;
//    //int targetInt = 0;
//    //for(int i = 0; i < neighborVectors.Length - 1; i++) {
//    //    for(int j = i + 1; j < neighborVectors.Length; j++) {
//    //        if(Vector3.Dot(neighborVectors[i], heroVector) < Vector3.Dot(neighborVectors[j], heroVector)) {
//    //            targetInt = j;
//    //        }
//    //        else {
//    //            targetInt = i;
//    //        }
//    //    }
//    //}

//    //Tile[ ] neighborTiles = GetNeighborTiles(_targetTile);
//    //Vector3[ ] neighborVector = new Vector3[neighborTiles.Length];

//    //for(int i = 0; i < neighborTiles.Length; i++) {
//    //    neighborVector[i] = neighborTiles[i].transform.position - _targetTile.transform.position;
//    //}

//    //Debug.Log("pathDircetion : " + pathDircetion);
//    //transform.up = Vector3.forward;
//    //transform.forward = Vector3.forward;
//    //Quaternion rotation = transform.rotation;
//    //rotation.eulerAngles = new Vector3(0, 180, transform.rotation.eulerAngles.z);

//    //if(Vector3.Distance(transform.position, Room.nowPath[_pathPoint]) > 0.25f) {
//    //    transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
//    //}
//    //else {
//    //    if(_pathPoint < Room.nowPath.Length - 1) {
//    //        _pathPoint++;
//    //    }
//    //    else {
//    //        _pathPoint = 0;
//    //    }
//    //}
//    //Vector3 dir = Room.nowPath[_pathPoint] - transform.position;
//    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//    //rotation = Quaternion.Lerp(transform.rotation, rotation, 10000 * Time.deltaTime);
//    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

//    //if(checkPoint < Room.nowPath.Length - 1) {
//    //    wayPointVector1 = Room.nowPath[checkPoint] - selfVector;
//    //    wayPointVector2 = Room.nowPath[checkPoint + 1] - selfVector;

//    //    if(Vector3.Dot(wayPointVector1, wayPointVector2) > 0) {
//    //        _pathPoint = checkPoint;
//    //    }
//    //    else {
//    //        _pathPoint = checkPoint + 1;
//    //    }
//    //}
//    //else {
//    //    _pathPoint = checkPoint;
//    //}
//    //transform.right = Room.nowPath[_pathPoint] - transform.position;
//    //playerRigid.transform.LookAt(new Vector3(pillers[startPoint].localPosition.x, 
//    //playerRigid.transform.localPosition.y, pillers[startPoint].localPosition.z));
//}
