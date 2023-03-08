using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int scoreKills=0;

    [SerializeField] private TMP_Text scoreUI;

    public int GetScore()
    {
        return scoreKills;
    }

    private void Start()
    {
        scoreKills = 0;
        OnScoreChanged(0);
    }

    private void OnEnable()
    {
        Zombie.onScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        Zombie.onScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        scoreKills += value;
        scoreUI.text = scoreKills.ToString();
    }
}
