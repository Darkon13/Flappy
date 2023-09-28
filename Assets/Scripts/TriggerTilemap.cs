using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Tilemap))]
public class TriggerTilemap : MonoBehaviour
{
    public event UnityAction<Vector3> Entered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entered?.Invoke(collision.bounds.center);
    }
}
