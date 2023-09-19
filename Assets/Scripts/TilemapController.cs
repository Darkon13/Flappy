using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GridMover _gridMover;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private int _grassLevel;

    [Header("Grass Tiles")]
    [SerializeField] private Tile _grassUp;
    [SerializeField] private Tile _grassMiddle;
    [SerializeField] private Tile _grassDown;
    
    [Header("Up Tube Tiles")]
    [SerializeField] private Tile _tubeUpLeft;
    [SerializeField] private Tile _tubeUpRight;
    [SerializeField] private Tile _tubeUpPlaceLeft;
    [SerializeField] private Tile _tubeUpPlaceRight;

    [Header("Down Tube Tiles")]
    [SerializeField] private Tile _tubeDownLeft;
    [SerializeField] private Tile _tubeDownRight;
    [SerializeField] private Tile _tubeDownPlaceLeft;
    [SerializeField] private Tile _tubeDownPlaceRight;

    [Header("Middle Tube Tiles")]
    [SerializeField] private Tile _tubeMiddleLeft;
    [SerializeField] private Tile _tubeMiddleRight;

    private Grid _grid;
    private int extraWidth;

    private float minY;
    private float maxY;
    private float minX;
    private float maxX;

    void Start()
    {
        _grid = _gridMover.GetComponent<Grid>();
        extraWidth = _gridMover.Lenght;
        
        minY = -_camera.orthographicSize;
        maxY = _camera.orthographicSize;
        minX = -_camera.orthographicSize * _camera.aspect;
        maxX = _camera.orthographicSize * _camera.aspect;

        int groundWidth = WorldToCell(maxX, 0, 0).x - WorldToCell(minX, 0, 0).x + extraWidth + 3;

        DrawGrass(WorldToCell(minX, minY, 0), groundWidth, _grassLevel);
        DrawDownTube(WorldToCell(minX, minY, 0) + new Vector3Int(5, _grassLevel, 0), 3);
        DrawUpTube(WorldToCell(minX, minY, 0) + new Vector3Int(0, _grassLevel, 0), 3);
    }

    private void OnEnable()
    {
        _gridMover.Moved += OnGridMoved;
    }

    private void OnDisable()
    {
        _gridMover.Moved -= OnGridMoved;
    }

    private void OnGridMoved()
    {
        int groundWidth = WorldToCell(maxX, 0, 0).x - WorldToCell(minX, 0, 0).x + extraWidth + 3;

        DrawGrass(WorldToCell(minX, minY, 0), groundWidth, _grassLevel);
        DrawDownTube(WorldToCell(minX, minY, 0) + new Vector3Int(5, _grassLevel, 0), 3);
        DrawUpTube(WorldToCell(minX, minY, 0) + new Vector3Int(0, _grassLevel, 0), 3);
    }

    private Vector3Int WorldToCell(float x, float y, float z = 0)
    {
        return _grid.WorldToCell(new Vector3(x, y, z));
    }

    private void DrawGrass(Vector3Int position, int width, int height)
    {
        if(height < 3)
        {
            throw new ArgumentOutOfRangeException();
        }

        BoxFill(position + new Vector3Int(0, height - 1, 0), width, 1, _grassUp);
        BoxFill(position + Vector3Int.up, width, height - 2, _grassMiddle);
        BoxFill(position, width, 1, _grassDown);
    }

    private void DrawUpTube(Vector3Int position, int lenght)
    {
        DrawTube(position, lenght, _tubeUpLeft, _tubeUpRight, _tubeUpPlaceLeft, _tubeUpPlaceRight);
    }

    private void DrawDownTube(Vector3Int position, int lenght)
    {
        DrawTube(position, lenght, _tubeDownPlaceLeft, _tubeDownPlaceRight, _tubeDownLeft, _tubeDownRight);
    }

    private void DrawTube(Vector3Int position, int lenght, Tile tubeLeft, Tile tubeRight, Tile tubePlaceLeft, Tile tubePlaceRight)
    {
        if (lenght < 3)
        {
            throw new ArgumentOutOfRangeException();
        }

        _tilemap.SetTile(position, tubePlaceLeft);
        _tilemap.SetTile(position + Vector3Int.right, tubePlaceRight);

        BoxFill(position + Vector3Int.up, 1, lenght - 2, _tubeMiddleLeft);
        BoxFill(position + new Vector3Int(1, 1, 0), 1, lenght - 2, _tubeMiddleRight);

        _tilemap.SetTile(position + new Vector3Int(0, lenght - 1, 0), tubeLeft);
        _tilemap.SetTile(position + new Vector3Int(1, lenght - 1, 0), tubeRight);
    }

    private void BoxFill(Vector3Int from, int lenghtX, int lenghtY, Tile tile)
    {
        for(int i = 0; i < lenghtX; i++)
            for(int j = 0; j < lenghtY; j++)
                _tilemap.SetTile(from + new Vector3Int(i, j, 0), tile);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 gridLeftBound = _grid.WorldToCell(new Vector3(minX, minY, 0));// + new Vector3Int(0, 0, 0); //+ new Vector3(0, (grassLevel + 2) * _grid.transform.localScale.y, 0);
    //    Vector3 gridRightBound = _grid.WorldToCell(new Vector3(maxX, minY, 0));// + new Vector3Int(0, grassLevel + 1, 0); //+ new Vector3(0, (grassLevel + 2) * _grid.transform.localScale.y, 0);

    //    //Debug.Log(_grid.transform.localScale.y);
    //    Gizmos.DrawLine(gridLeftBound, gridRightBound);
    //}
}
