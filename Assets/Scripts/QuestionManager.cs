using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour {
    [SerializeField]
    public List<Question> questionSet = new List<Question>();
    int currentQuestion = 0;
    List<Question> completed, incompleted, passed = new List<Question>();
    public Text contentText;
    public int score = 0;
    public FaceBehaviour[] faces;
    void Awake()
    {
        incompleted = new List<Question>(questionSet);
        completed = new List<Question>(); 
    }
    public void CreateQuestion()
    {
        
        if (incompleted.Count > 0)
        {
            currentQuestion = Random.Range(0, incompleted.Count); //As long as there are questions to answer, pick a random one
            contentText.text = incompleted[currentQuestion].content; //sets the question
        }
        else
        {
            if(passed.Count > 0)
            {
                incompleted = new List<Question>(passed);
                passed.Clear();
                CreateQuestion();
                //HideNeutralFaces();
            }
            else
            {
                Debug.Log("All Done, Ready To Score");
                ReportCompletion();
                SetFaceVisibility(false);
            }
            //int indexSC = SceneManager.GetActiveScene().buildIndex; 
            //SceneManager.LoadScene(indexSC + 1); //If there are no more questions to be asked, move on to the next scene
        }
    }
    void ReportCompletion()
    {
        contentText.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
		SceneManager.LoadScene("Credits");
    }
    public void SetFaceVisibility(bool active)
    {
        Debug.Log("found faces: " + faces.Length);
        foreach (FaceBehaviour face in faces)
        {
                face.gameObject.SetActive(active);
        }
    }
    public void HideNeutralFaces()
    {
        foreach (FaceBehaviour face in faces)
        {
            if (face.face == FaceBehaviour.FaceTypes.Neutral)
            {
                face.gameObject.SetActive(false);
            }
        }
    }
    public bool CheckSolution(FaceBehaviour.FaceTypes inputSolution)
    {
        if(incompleted[currentQuestion].solution == inputSolution) //If the correct solution is the same as your input:
        {
            incompleted[currentQuestion].isSolved = true;
            score++;
            return true; //Set the question to the completed pile and add 1 to the score
        }
        else
        {
            return false; //If it's wrong, keep it in the incompleted pile, do not update score
        }
    }
    public void AddToPassPile()
    { 
			passed.Add(incompleted[currentQuestion]); //Add it to the neutral pile
            Debug.Log("Added to passed pile");
            incompleted.RemoveAt(currentQuestion); //Remove the question from the incomplete pile

    }
    public void AddToCompletePile()
    {
        completed.Add(incompleted[currentQuestion]); //Add the current question to the completed pile
        incompleted.RemoveAt(currentQuestion); //Remove the question from the incomplete pile
        Debug.Log("Added to Complete Pile");
    }
    public bool CueNextQuestion()
    {
        bool nextRoundTrigger = false;
        switch (GameManager.instance.roundState)
        {
            case GameManager.roundStates.notStarted:
                nextRoundTrigger = true;
                break;
            case GameManager.roundStates.firstRound:
                nextRoundTrigger = (incompleted.Count == 0);
                break;
            case GameManager.roundStates.secondRound:
                nextRoundTrigger = (incompleted.Count == 0);
                break;
            case GameManager.roundStates.completed:
                break;
        }
        CreateQuestion();
        Debug.Log("Next Round Trigger: " + nextRoundTrigger.ToString());
        return nextRoundTrigger;
    }
    public float GetQuestionProgress()
    {
        float numerator_incompleted = incompleted.Count;
        float numerator_passed = passed.Count;
        float denominator = incompleted.Count + passed.Count + completed.Count;
        //GameManager.instance.menuManager.questionMetre.SetMetreFill(incompleted.Count, passed.Count, completed.Count);
        GameManager.instance.menuManager.questionMetre.SetMetreFill(completed, passed.Count);
        return numerator_incompleted / denominator;
    }
}
