using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TilemapObjects/Tube", order = 51)]
public class Tube : ScriptableObject
{
    [SerializeField] private Tile _tubeUpLeft;
    [SerializeField] private Tile _tubeUpRight;
    [SerializeField] private Tile _tubeMiddleLeft;
    [SerializeField] private Tile _tubeMiddleRight;
    [SerializeField] private Tile _tubeDownLeft;
    [SerializeField] private Tile _tubeDownRight;

    public Tile TubeUpLeft => _tubeUpLeft;
    public Tile TubeUpRight => _tubeUpRight;
    public Tile TubeMiddleLeft => _tubeMiddleLeft;
    public Tile TubeMiddleRight => _tubeMiddleRight;
    public Tile TubeDownLeft => _tubeDownLeft;
    public Tile TubeDownRight => _tubeDownRight;
}
