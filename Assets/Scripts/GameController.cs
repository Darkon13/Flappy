using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Died += OnPlayerDead;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        Time.timeScale = 0;
    }
}
