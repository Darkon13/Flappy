using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TilemapObjects/Ground", order = 51)]
public class Ground : ScriptableObject
{
    [SerializeField] private Tile _groundUp;
    [SerializeField] private Tile _groundMiddle;
    [SerializeField] private Tile _groundDown;

    public Tile GroundUp => _groundUp;
    public Tile GroundMiddle => _groundMiddle;
    public Tile GroundDown => _groundDown;
}
