using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Camera _camera;
    private float size;

    void Start()
    {
        _camera = GetComponent<Camera>();
        size = _camera.orthographicSize;
        Debug.Log(_camera.pixelHeight);
        Debug.Log(_camera.aspect);
        Debug.Log(size);

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(Vector3.zero, new Vector3(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize, 0));
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(Vector3.zero, new Vector3(-_camera.orthographicSize * _camera.aspect, _camera.orthographicSize, 0));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(Vector3.zero, new Vector3(_camera.orthographicSize * _camera.aspect, -_camera.orthographicSize, 0));
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawLine(Vector3.zero, new Vector3(-_camera.orthographicSize * _camera.aspect, -_camera.orthographicSize, 0));
    //}
}
