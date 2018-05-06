using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour {
    public Text endText;
    public Text correctAnswers;
    public Text incorrectAnswers;
    public Transform resultContainer;
    public GameObject resultElementPrefab;
    [SerializeField]
    public QuestionDetailPopup questionDetailPopup;
    void Start()
    {
        GetQuestionResults();
        endText.text = "Thanks for playing! Your score is: " + PlayerPrefs.GetInt("score") + "/40"; //reports score
        questionDetailPopup.exitButton.onClick.AddListener(()=>questionDetailPopup.Close());
    }
    void GetQuestionResults()
    {
        string correctAnswersStr = "";
        string incorrectAnswersStr = "";
        if (GameManager.instance != null)
        {
            foreach(Question q in GameManager.instance.questionManager.questionSet)
            {
                GameObject resultInstance = Instantiate(resultElementPrefab, resultContainer);
                resultInstance.GetComponent<ResultButtonElement>().SetResult(q.isSolved, q.content, q.solution);
                resultInstance.GetComponent<ResultButtonElement>().resultButton.onClick.AddListener(() => { questionDetailPopup.SetQuestionContent(q); questionDetailPopup.Open(); });
                if (q.isSolved == true)
                {
                    correctAnswersStr += q.content/*.Substring(0,20)*/ + "\n";
                } 
                else
                {
                    incorrectAnswersStr += q.content/*.Substring(0, 20)*/ + "\n";
                }
            }
            
        }
        correctAnswers.text = correctAnswersStr;
        incorrectAnswers.text = incorrectAnswersStr;
    }
    [System.Serializable]
    public class QuestionDetailPopup : Popup
    {
        public Text questionContent;
        public Button exitButton;
        public void SetQuestionContent(Question inputQuestion)
        {
            questionContent.text = inputQuestion.content;
        }
    }
}
