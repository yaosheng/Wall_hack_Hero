using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    private TileManager _tileManager;
    private EnemyManager _enemyManager;
    private DirectionType directionType;
    public SpriteRenderer thisenemySprite;
    private float _timer = 0;
    private float _checkTime = 0;
    private Tile checkTile;
    private List<Tile> PatrolTile;

    void Awake( )
    {
        _tileManager = GameObject.FindObjectOfType(typeof(TileManager)) as TileManager;
        _enemyManager = GameObject.FindObjectOfType(typeof(EnemyManager)) as EnemyManager;
        _enemyManager.enemyGroup.Add(this);
    }

	void Start () {
        directionType = DirectionType.Right;
        _checkTime = Random.Range(1.5f, 10.5f);
    }
	
	void Update () {
        TransformDirection( );
        if(_timer < _checkTime) {
            _timer += Time.deltaTime;
        }
        else {
            _timer = 0;
            _checkTime = Random.Range(1.5f, 10.5f);
            if(directionType == DirectionType.Left) {
                directionType = DirectionType.Right;
            }
            else {
                directionType = DirectionType.Left;
            }
        }
    }

    public void CheckEnemyPosition( )
    {
        float checkDistence = 0;
        float minDistence = 100.0f;
        //TcheckTile = null;
        Vector3 targetPosition;

        foreach(Tile tile in _tileManager.tileGroup) {
            checkDistence = (transform.position - tile.transform.position).sqrMagnitude;
            if(checkDistence <= minDistence) {
                minDistence = checkDistence;
                checkTile = tile;
            }
        }
        transform.position = checkTile.topSide.position;
        targetPosition = checkTile.topSide.rightPosition;
        transform.right = (targetPosition - transform.position).normalized;
    }

    void TransformDirection( )
    {
        if(directionType == DirectionType.Right) {
            transform.eulerAngles = Vector3.zero;
        }
        else {
            transform.eulerAngles = Vector3.up * 180;
        }
    }

    void AoToPatrol( )
    {

    }
}
