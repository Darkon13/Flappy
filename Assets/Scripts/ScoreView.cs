using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private Score _score;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        _score.Changed += OnScoreChanged;
        _text.SetText($"Score: {_score.Points}");
    }

    private void OnDisable()
    {
        _score.Changed -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _text.SetText($"Score: {_score.Points}");
    }
}
