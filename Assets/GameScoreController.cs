using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leftPlayerText;
    [SerializeField] private TextMeshProUGUI _rightPlayerText;

    private void Start()
    {
        ScoreUpdated(null, null);
        GameManager.ScoreUpdated += ScoreUpdated;
    }

    private void ScoreUpdated(object sender, EventArgs e)
    {
        _leftPlayerText.text = GameManager.LeftPlayerScore.ToString();
        _rightPlayerText.text = GameManager.RightPlayerScore.ToString();
    }
}
