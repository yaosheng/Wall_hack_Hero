using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;

public class TestHero : MonoBehaviour
{
    public DirectionType directionType;
    public Side currentSide;
    private float _walkSpeed = 2.0f;
    public SpriteRenderer hero;
    public Text Blood;
    public Image reStartButton;
    public int nowBlood = 3;
    public Text debug2;

    void ShowUI( )
    {
        Blood.text = nowBlood + "/" + "3";
    }

    void ReStartScene( )
    {
        if(nowBlood == 0) {
            reStartButton.gameObject.SetActive(true);
            hero.enabled = false;
        }
    }

    void Update( )
    {
        //debug2.text = currentSide.parentTile.point.ToString( );
        JumpToOppositeSide( );
        CheckCurrentSide( );
        WalkBySide(directionType);
        TransformDirection( );
        ShowUI( );
        ReStartScene( );
    }

    void WalkBySide(DirectionType DT)
    {
        Vector3 direction;
        if(DT == DirectionType.Right) {
            direction = currentSide.right.position - transform.position;
        }
        else {
            direction = currentSide.left.position - transform.position;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation = Quaternion.Lerp(transform.rotation, rotation, 5.5f * Time.deltaTime);
        transform.rotation = rotation;
        transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
    }

    void JumpToOppositeSide( )
    {
        if(Input.GetButtonDown("Fire1") && currentSide.OppositeSide( ) != null && !currentSide.parentTile.iscrossTile) {
            currentSide = currentSide.OppositeSide( );
            transform.position = currentSide.OppositeSide( ).position;

            if(directionType == DirectionType.Right) {
                directionType = DirectionType.Left;
            }
            else {
                directionType = DirectionType.Right;
            }
        }
    }

    void CheckCurrentSide( )
    {
        Vector3 leftVector = currentSide.leftPosition - currentSide.position;
        Vector3 rightVector = currentSide.rightPosition - currentSide.position;

        if(Vector3.Dot(transform.right, rightVector) > 0) {
            Vector3 checkVector = currentSide.rightPosition - transform.position;
            if(Vector3.Dot(checkVector, rightVector) < 0) {
                currentSide = currentSide.right;
            }
        }
        if(Vector3.Dot(transform.right, leftVector) > 0) {
            Vector3 checkVector = currentSide.leftPosition - transform.position;
            if(Vector3.Dot(checkVector, leftVector) < 0) {
                currentSide = currentSide.left;
            }
        }
    }

    void TransformDirection( )
    {
        if(directionType == DirectionType.Right) {
            hero.flipY = false;
            hero.GetComponent<Collider2D>( ).offset = new Vector2(0, 0.64f);
            //hero.transform.eulerAngles = Vector3.zero;
        }
        else {
            hero.flipY = true;
            hero.GetComponent<Collider2D>( ).offset = new Vector2(0, -0.64f);
            //hero.transform.eulerAngles = Vector3.up * 180;
        }
    }



//#if UNITY_EDITOR
//    [Range(5, 30)]
//    public int gizmosCount = 5;
//    public Color gizmosColor = Color.cyan;

//    void OnDrawGizmos( )
//    {
//        if(currentSide == null)
//            return;

//        Gizmos.color = gizmosColor;
//        Color fade = new Color(0, 0, 0, 1f / gizmosCount);

//        Side side = currentSide;
//        for(int i = 0; i < gizmosCount; ++i) {
//            Gizmos.color -= fade;
//            Gizmos.DrawSphere(side.position, 0.1f);

//            side = (directionType == DirectionType.Right) ? side.right : side.left;
//        }
//    }
//#endif

    //public void CheckHeroPositionAndLookAtRoad( )
    //{
    //    Vector3 selfPosition = transform.position;
    //    float checkDistence = 0;
    //    float minDistence = 100.0f;
    //    Tile checkTile = null;
    //    Vector3 rightPosition;

    //    foreach(Tile tile in _tileManager.tileGroup) {
    //        checkDistence = (selfPosition - tile.transform.position).sqrMagnitude;
    //        if(checkDistence < minDistence) {
    //            minDistence = checkDistence;
    //            checkTile = tile;
    //        }
    //    }
    //    _currentSide = checkTile.topSide;
    //    selfPosition = _currentSide.position;
    //    rightPosition = _currentSide.rightPosition;
    //    transform.right = (rightPosition - selfPosition).normalized;
    //}

    //if(_currentSide == _currentSide.parentTile.rightSide) {
    //    _currentSide = _currentSide.parentTile.leftSide;
    //    transform.position = _currentSide.position;
    //}
    //else if(_currentSide == _currentSide.parentTile.leftSide) {
    //    _currentSide = _currentSide.parentTile.rightSide;
    //    transform.position = _currentSide.position;
    //}
    //else if(_currentSide == _currentSide.parentTile.topSide) {
    //    _currentSide = _currentSide.parentTile.bottomSide;
    //    transform.position = _currentSide.position;
    //}
    //else{
    //    _currentSide = _currentSide.parentTile.topSide;
    //    transform.position = _currentSide.position;
    //}

    //Vector3 nextVector = checkTile.topSide.right.position;
    //transform.right = (nextVector - transform.position).normalized;
    //_targetSide = checkTile.topSide.right;
    //_targetTile = checkTile.rightTile;
    //directionType = DirectionType.Right;
}
