using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _leftPlayerScore = 0;
    public static int LeftPlayerScore
    {
        get => _leftPlayerScore;
        set
        {
            _leftPlayerScore = value;
            ScoreUpdated?.Invoke(null, EventArgs.Empty);
        }
    }

    private static int _rightPlayerScore = 0;

    public static int RightPlayerScore
    {
        get => _rightPlayerScore;
        set
        {
            _rightPlayerScore = value;
            ScoreUpdated?.Invoke(null, EventArgs.Empty);
        }
    }

    public static EventHandler ScoreUpdated;

    private void Start()
    {
        LeftPlayerScore = 0;
        RightPlayerScore = 0;
    }
}
