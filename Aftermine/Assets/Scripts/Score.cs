using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Score : MonoBehaviour
{
    public TMP_Text scoreElement;

    public int score;
    public float incrementDuration = 0.5f;

    private int targetScore = 0;

    public void Reset()
    {
        score = 0;
        targetScore = 0;
        UpdateScoreText();
    }

    public void OnBrickCleared(Brick brick)
    {
        if (brick == null) return;

        targetScore += brick.type.ClearValue;
        DOTween.To(()=> score, s => score = s, targetScore, incrementDuration)
            .OnUpdate(() => UpdateScoreText());
    }

    private void UpdateScoreText()
    {
        scoreElement.text = score.ToString();
    }
}
