using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GridMover _gridMover;
    [SerializeField] private TriggerTilemap _trigger;
    [SerializeField] private Tilemap _tilemap;

    [Header("Styles")]
    [SerializeField] private Ground _ground;
    [SerializeField] private Barrier _barrier;

    [Header("Generator options")]
    [SerializeField] private int _groundLevel;
    [SerializeField] private int _tubeSpawnOffsetX;
    [SerializeField] private int _spaceHeight;
    [SerializeField] private Tile _triggerTile;

    private Tilemap _triggerTilemap;
    private Grid _grid;
    private int _extraWidth;

    private float _minY;
    private float _maxY;
    private float _minX;
    private float _maxX;

    private void Start()
    {
        _grid = _gridMover.GetComponent<Grid>();
        _triggerTilemap = _trigger.GetComponent<Tilemap>();
        _extraWidth = _gridMover.Lenght;
        
        _minY = -_camera.orthographicSize;
        _maxY = _camera.orthographicSize;
        _minX = -_camera.orthographicSize * _camera.aspect;
        _maxX = _camera.orthographicSize * _camera.aspect;
    }

    private void OnEnable()
    {
        _gridMover.Moved += OnGridMoved;
        _gridMover.Inited += OnGridInited;
        _trigger.Entered += OnTriggerEntered;
    }

    private void OnDisable()
    {
        _gridMover.Moved -= OnGridMoved;
        _gridMover.Inited -= OnGridInited;
        _trigger.Entered -= OnTriggerEntered;
    }

    private void OnGridInited()
    {
        _tilemap.ClearAllTiles();
        _triggerTilemap.ClearAllTiles();

        int groundWidth = WorldToCell(_maxX + _extraWidth + 1, 0).x - WorldToCell(_minX, 0).x;

        DrawGround(WorldToCell(_minX, _minY), groundWidth, _groundLevel);
    }

    private void OnTriggerEntered(Vector3 center)
    {
        BoxClear(_triggerTilemap, WorldToCell(center) + new Vector3Int(0, -_spaceHeight), 3, _spaceHeight * 2 + 1);
    }

    private void OnGridMoved()
    {
        int groundWidth = WorldToCell(_maxX + _extraWidth + 1, 0).x - WorldToCell(_maxX, 0).x;
        int minLenghtUpTube = 3;
        int upBoundY = WorldToCell(0, _maxY).y + 1;
        int groundY = WorldToCell(0, _minY).y + _groundLevel;

        BoxClear(_tilemap, WorldToCell(_minX, _minY) - new Vector3Int(groundWidth, 0, 0), groundWidth, upBoundY - WorldToCell(0, _minY).y);

        DrawGround(WorldToCell(_maxX, _minY), groundWidth, _groundLevel);
        DrawBarrier(WorldToCell(_maxX, _minY) + new Vector3Int(_tubeSpawnOffsetX, _groundLevel, 0), minLenghtUpTube, (upBoundY - groundY) - _spaceHeight - minLenghtUpTube + 1, upBoundY, _spaceHeight);
    }

    private Vector3Int WorldToCell(float x, float y, float z = 0)
    {
        return _grid.WorldToCell(new Vector3(x, y, z));
    }

    private Vector3Int WorldToCell(Vector3 vector)
    {
        return _grid.WorldToCell(vector);
    }

    private void DrawTrigger(Vector3Int position, int height, int width = 2)
    {
        BoxFill(_triggerTilemap, position, width, height, _triggerTile);
    }

    private void DrawBarrier(Vector3Int position, int minLenghtUpTube, int maxLenghtUpTube, int upBoundY, int spaceHeight)
    {
        int height = UnityEngine.Random.Range(minLenghtUpTube, maxLenghtUpTube);

        DrawUpTube(position, height);
        DrawTrigger(position + new Vector3Int(0, height, 0), spaceHeight);
        DrawDownTube(position + new Vector3Int(0, spaceHeight + height, 0), Mathf.Abs(position.y - upBoundY) - spaceHeight - height);
    }

    private void DrawGround(Vector3Int position, int width, int height)
    {
        if(height < 3)
        {
            throw new ArgumentOutOfRangeException();
        }

        BoxFill(_tilemap, position + new Vector3Int(0, height - 1, 0), width, 1, _ground.GroundUp);
        BoxFill(_tilemap, position + Vector3Int.up, width, height - 2, _ground.GroundMiddle);
        BoxFill(_tilemap, position, width, 1, _ground.GroundDown);
    }

    private void DrawUpTube(Vector3Int position, int height)
    {
        DrawTube(position, height, _barrier.TubeUp);
    }

    private void DrawDownTube(Vector3Int position, int height)
    {
        DrawTube(position, height, _barrier.TubeDown);
    }

    private void DrawTube(Vector3Int position, int height, Tube tube)
    {
        if (height < 3)
        {
            throw new ArgumentOutOfRangeException();
        }

        _tilemap.SetTile(position, tube.TubeDownLeft);
        _tilemap.SetTile(position + Vector3Int.right, tube.TubeDownRight);

        BoxFill(_tilemap, position + Vector3Int.up, 1, height - 2, tube.TubeMiddleLeft);
        BoxFill(_tilemap, position + new Vector3Int(1, 1, 0), 1, height - 2, tube.TubeMiddleRight);

        _tilemap.SetTile(position + new Vector3Int(0, height - 1, 0), tube.TubeUpLeft);
        _tilemap.SetTile(position + new Vector3Int(1, height - 1, 0), tube.TubeUpRight);
    }

    private void BoxFill(Tilemap tilemap, Vector3Int from, int lenghtX, int heightY, Tile tile)
    {
        for(int i = 0; i < lenghtX; i++)
            for(int j = 0; j < heightY; j++)
                tilemap.SetTile(from + new Vector3Int(i, j, 0), tile);
    }

    private void BoxClear(Tilemap tilemap, Vector3Int from, int lenghtX, int heightY) => BoxFill(tilemap, from, lenghtX, heightY, null);
}
