using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public event UnityAction PlayerAwaked;
    public event UnityAction GameStarted;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDead;
        _player.Awaked += OnPlayerAwake;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDead;
        _player.Awaked -= OnPlayerAwake;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        GameStarted?.Invoke();
    }

    private void OnPlayerDead()
    {
        Time.timeScale = 0;
    }

    private void OnPlayerAwake()
    {
        Time.timeScale = 1;

        PlayerAwaked?.Invoke();
    }
}
