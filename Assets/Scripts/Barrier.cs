using UnityEngine;

[CreateAssetMenu(menuName = "TilemapObjects/Barrier", order = 51)]
public class Barrier : ScriptableObject
{
    [SerializeField] private Tube _tubeUp;
    [SerializeField] private Tube _tubeDown;

    public Tube TubeUp => _tubeUp;
    public Tube TubeDown => _tubeDown;
}
