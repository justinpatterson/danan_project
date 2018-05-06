using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultButtonElement : MonoBehaviour {
    public Button resultButton;
    public Text resultText;
    public Image resultAnswerImage;
    public Image resultButtonBackground;
    public Sprite happySprite, sadSprite;
    public void SetResult(bool isCorrect, string content, FaceBehaviour.FaceTypes face)
    {
        if(isCorrect == true)
        {
            resultButtonBackground.color = new Color(0.9f, 1f, .9f);
        }
        else
        {
            resultButtonBackground.color = new Color(1f, .9f, .9f);
        }
        resultText.text = content;
        Sprite resultSprite = happySprite;
        if (face != FaceBehaviour.FaceTypes.Happy)
        {
            resultSprite = sadSprite;
        }
        resultAnswerImage.sprite = resultSprite;
    }
}
