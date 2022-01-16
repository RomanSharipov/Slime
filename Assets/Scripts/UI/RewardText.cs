using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardText : MonoBehaviour
{
    [SerializeField] private TextMesh _textTemplate;

    public void SetText(string text)
    {
        _textTemplate.text = text;
    }
}
