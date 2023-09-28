using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform _mainMenuPanel;
    [SerializeField] private RectTransform _deathScreenPanel;
    [SerializeField] private RectTransform _settingsPanel;
    [SerializeField] private RectTransform _hud;
    [SerializeField] private Player _player;
    [SerializeField] private GameController _gameController;

    private void Start()
    {
        ShowMainMenu();
    }

    private void OnDisable()
    {
        _player.Died -= ShowDeathScreen;
        _gameController.GameStarted -= ShowHUD;
    }

    private void OnEnable()
    {
        _player.Died += ShowDeathScreen;
        _gameController.GameStarted += ShowHUD;
    }

    public void ShowMainMenu() => EnablePanel(_mainMenuPanel);

    public void ShowDeathScreen() => EnablePanel(_deathScreenPanel);

    public void ShowSettings() => EnablePanel(_settingsPanel);

    public void ShowHUD() => EnablePanel(_hud);

    private void DisableAllPanels()
    {
        _mainMenuPanel.gameObject.SetActive(false);
        _deathScreenPanel.gameObject.SetActive(false);
        _settingsPanel.gameObject.SetActive(false);
        _hud.gameObject.SetActive(false);
    }

    private void EnablePanel(RectTransform panel)
    {
        DisableAllPanels();
        panel.gameObject.SetActive(true);
    }
}
