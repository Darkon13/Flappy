using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenBound : MonoBehaviour
{
    private Camera _camera;    

    public float MinX { private set; get; }
    public float MinY { private set; get; }
    public float MaxX { private set; get; }
    public float MaxY { private set; get; }

    void Start()
    {
        _camera = GetComponent<Camera>();

        MinY = -_camera.orthographicSize;
        MaxY = _camera.orthographicSize;
        MinX = -_camera.orthographicSize * _camera.aspect;
        MaxX = _camera.orthographicSize * _camera.aspect;
    }
}
