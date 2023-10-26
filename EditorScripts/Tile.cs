using UnityEngine;
using System.Collections;
using Atfone.Drawing;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public Point point;
    public Point editorPoint;
    public Cell cell;
    public bool iscrossTile;

    public Tile topTile;
    public Tile bottomTile;
    public Tile rightTile;
    public Tile leftTile;

    public Side topSide;
    public Side bottomSide;
    public Side rightSide;
    public Side leftSide;

    void Awake( )
    {
        topSide = new Side(this);
        bottomSide = new Side(this);
        rightSide = new Side(this);
        leftSide = new Side(this);
    }

    void Start( )
    {

    }

    public void SetConnectionBySide( )
    {
        if(topTile == null) {
            SetConnectionByTopSide( );
        }
        if(bottomTile == null) {
            SetConnectionByBottomSide( );
        }
        if(rightTile == null) {
            SetConnectionByRightSide( );
        }
        if(leftTile == null) {
            SetConnectionByLeftSide( );
        }
    }

    public void SetPositionBySide( )
    {
        topSide.SetPosition( );
        bottomSide.SetPosition( );
        rightSide.SetPosition( );
        leftSide.SetPosition( );
        //Debug.Log(point + " : " + " topSide " + " : " + topSide.position);
        //Debug.Log(point + " : " + " bottomSide " + " : " + bottomSide.position);
        //Debug.Log(point + " : " + " rightSide " + " : " + rightSide.position);
        //Debug.Log(point + " : " + " leftSide " + " : " + leftSide.position);
    }

    public void SetConnectionByTopSide( )
    {
        if(rightTile != null) {
            if(rightTile.topTile != null) {
                topSide.right = rightTile.topTile.leftSide;
            }
            else {
                topSide.right = rightTile.topSide;
            }
        }
        else {
            topSide.right = rightSide;
        }
        if(leftTile != null) {
            if(leftTile.topTile != null) {
                topSide.left = leftTile.topTile.rightSide;
            }
            else {
                topSide.left = leftTile.topSide;
            }
        }
        else {
            topSide.left = leftSide;
        }
    }

    public void SetConnectionByBottomSide( )
    {
        if(rightTile != null) {
            if(rightTile.bottomTile != null) {
                bottomSide.left = rightTile.bottomTile.leftSide;
            }
            else {
                bottomSide.left = rightTile.bottomSide;
            }
        }
        else {
            bottomSide.left = rightSide;
        }
        if(leftTile != null) {
            if(leftTile.bottomTile != null) {
                bottomSide.right = leftTile.bottomTile.rightSide;
            }
            else {
                bottomSide.right = leftTile.bottomSide;
            }
        }
        else {
            bottomSide.right = leftSide;
        }
    }

    public void SetConnectionByRightSide( )
    {
        if(topTile != null) {
            if(topTile.rightTile != null) {
                rightSide.left = topTile.rightTile.bottomSide;
            }
            else {
                rightSide.left = topTile.rightSide;
            }
        }
        else {
            rightSide.left = topSide;
        }
        if(bottomTile != null) {
            if(bottomTile.rightTile != null) {
                rightSide.right = bottomTile.rightTile.topSide;
            }
            else {
                rightSide.right = bottomTile.rightSide;
            }
        }
        else {
            rightSide.right = bottomSide;
        }
    }

    public void SetConnectionByLeftSide( )
    {
        if(topTile != null) {
            if(topTile.leftTile != null) {
                leftSide.right = topTile.leftTile.bottomSide;
            }
            else {
                leftSide.right = topTile.leftSide;
            }
        }
        else {
            leftSide.right = topSide;
        }
        if(bottomTile != null) {
            if(bottomTile.leftTile != null) {
                leftSide.left = bottomTile.leftTile.topSide;
            }
            else {
                leftSide.left = bottomTile.leftSide;
            }
        }
        else {
            leftSide.left = bottomSide;
        }
    }
}
