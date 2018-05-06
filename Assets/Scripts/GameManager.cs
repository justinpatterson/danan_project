using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public QuestionManager questionManager;
    public EffectsManager effectsManager;
    public MenuManager menuManager;
    public TextAsset questionData;
    [SerializeField]
    public InstructionBox startButton;
    public enum roundStates { notStarted, firstRound, secondRound, completed }
    public roundStates roundState = roundStates.notStarted;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Don't destroy the GameManager if it's not in the scene
        }
        else
        {
            Destroy(gameObject);
        }
        QuestionListData s = new QuestionListData();
        /* HARDCODED VERSION
			s.questions = questionManager.questionSet.ToArray();
        */

		/* STREAMING ASSETS VERSION */
			string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "QuestionData2.json");
			Debug.Log(filePath);
	        WWW data_streaming = new WWW(filePath);
			/*WAIT UNTIL COMPLETE*/
			while(!data_streaming.isDone) {  Debug.Log("Waiting for data streaming to complete..."); };

		/* RESOURCES VERSION */
			//Object o = Resources.Load("Data/QuestionData");
        	//TextAsset data_resources = o as TextAsset;

        //Debug.Log(data.text);

		try
		{
        	s = JsonUtility.FromJson<QuestionListData>(data_streaming.text);
		}
		catch 
		{
			Debug.Log("Failed to load new questions");
			s = new QuestionListData();
			s.questions = questionManager.questionSet.ToArray();
		}
        //Debug.Log(questionData.text);
        //Debug.Log(s.questions.Length);
        questionManager.questionSet = new List<Question>(s.questions);
    }
    [System.Serializable]
    public class QuestionListData
    {
        [SerializeField]
        public Question[] questions;
    }
    private void Start()
    {
        TriggerNextPhase();
    }
    public void ReportFaceClick(FaceBehaviour.FaceTypes clickedFace, Vector3 facePoint)
    {
        if(menuManager.PopupOpenCheck())
        {
            return;
        }
        if(clickedFace == FaceBehaviour.FaceTypes.Neutral)
        {
            questionManager.AddToPassPile(); //If the answer was neutral, sort it to the neutral pile
        }
        else
        {
            bool isCorrect = questionManager.CheckSolution(clickedFace);
            Debug.Log("Game Manager heard a face. Correct? " + isCorrect.ToString());
            if(isCorrect == true)
            {
                effectsManager.CreateEffectAtPoint(EffectsManager.EffectTypes.goodParticle, facePoint);
            }
            else
            {
                effectsManager.CreateEffectAtPoint(EffectsManager.EffectTypes.badParticle, facePoint);
            }
            questionManager.AddToCompletePile();
        }
        TriggerQuestionCue();
    }
    void TriggerQuestionCue()
    {
        bool nextRoundTriggered = questionManager.CueNextQuestion();
        questionManager.GetQuestionProgress();
        if (nextRoundTriggered == true)
        {
            TriggerNextPhase();
        }
    }
    public void TriggerRoundStart()
    {
        Debug.Log("Trigger round start" + roundState.ToString());
        switch (roundState)
        {
            case roundStates.firstRound:
                TriggerQuestionCue();
                startButton.Close();
                questionManager.SetFaceVisibility(true);
                menuManager.TriggerPopupEvent("Round " + ((int)roundState).ToString());
                menuManager.questionMetre.CreateMetreElements(questionManager.questionSet.Count);
                break;
            case roundStates.secondRound:
                menuManager.TriggerPopupEvent("Round " + ((int)roundState).ToString());
                startButton.Close();
                questionManager.SetFaceVisibility(true);
                questionManager.HideNeutralFaces();
                break;
        }
    }
    public void TriggerNextPhase()
    {
        Debug.Log("Trigger Next Phase");
        switch(roundState)
        {
            case roundStates.notStarted:
                roundState = roundStates.firstRound;
                questionManager.SetFaceVisibility(false);
                startButton.Open();
                startButton.SetInstructions("Click To Start", "Round 1");
                break;
            case roundStates.firstRound:
                roundState = roundStates.secondRound;
                questionManager.SetFaceVisibility(false);
                startButton.Open();
                startButton.SetInstructions("Neutral Cards Need To Be Sorted!!", "Round 2");
                break;
            case roundStates.secondRound:
                roundState = roundStates.completed;
                break;
            case roundStates.completed:
                break;
        }
    }
}
