using UnityEngine;
using System.Collections;
using System.Drawing;

public class Data
{
    public const int tileWidth = 60;
    public const int tileHeight = 100;
    public const float cellWidth = 0.32f;
    public const float cellHeight = 0.32f;
    public enum tileTypes { Empty, HackWall, SolidWall, Trap, Enemy, Decoration, Coin, WallPaper};
}

public class Cell
{
    public Data.tileTypes cellType;
    public bool IsEmpty
    {
        get { return cellType == Data.tileTypes.Empty; }
    }
}

public class Side
{
    public Side right;
    public Side left;
    public Tile parentTile;
    public Vector3 position;
    public Vector3 rightPosition;
    public Vector3 leftPosition;

    public Side(Tile tile)
    {
        parentTile = tile;
    }

    public void SetPosition( )
    {
        if(parentTile.topSide == this) {
            position = parentTile.transform.position + Vector3.up * Data.cellHeight * 0.5f;
            rightPosition = position + Vector3.right * Data.cellWidth * 0.5f;
            leftPosition = position + Vector3.left * Data.cellWidth * 0.5f;
        }
        else if(parentTile.bottomSide == this) {
            position = parentTile.transform.position + Vector3.down * Data.cellHeight * 0.5f;
            rightPosition = position + Vector3.left * Data.cellWidth * 0.5f;
            leftPosition = position + Vector3.right * Data.cellWidth * 0.5f;
        }
        else if (parentTile.rightSide == this) {
            position = parentTile.transform.position + Vector3.right * Data.cellWidth * 0.5f;
            rightPosition = position + Vector3.down * Data.cellHeight * 0.5f;
            leftPosition = position + Vector3.up * Data.cellHeight * 0.5f;
        }
        else if (parentTile.leftSide == this) {
            position = parentTile.transform.position + Vector3.left * Data.cellWidth * 0.5f;
            rightPosition = position + Vector3.up * Data.cellHeight * 0.5f;
            leftPosition = position + Vector3.down * Data.cellHeight * 0.5f;
        }
    }

    public Side OppositeSide( )
    {
        Side tempSide;
        if(parentTile.topSide == this) {
            tempSide = parentTile.bottomSide;
        }
        else if(parentTile.bottomSide == this) {
            tempSide = parentTile.topSide;
        }
        else if(parentTile.rightSide == this) {
            tempSide = parentTile.leftSide;
        }
        else {
            tempSide = parentTile.rightSide;
        }
        return tempSide;
    }
}

public enum DirectionType
{
    Right,
    Left
}