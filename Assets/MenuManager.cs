using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField]
    public RoundPopup roundPopup;
    [SerializeField]
    public Metre questionMetre;
    public void TriggerPopupEvent(string popupTextString)
    {
        roundPopup.TriggerRoundPopup(popupTextString);
        StartCoroutine(PopupEventCoroutine(roundPopup));
    }
    IEnumerator PopupEventCoroutine(Popup p)
    {
        p.Open();
        yield return new WaitForSeconds(1f);
        p.Close();
    }
    public bool PopupOpenCheck()
    {
        return roundPopup.isOpen;
    }
}
[System.Serializable]
public class UIElement
{
    public Transform container;
}
[System.Serializable]
public class Metre : UIElement
{
    public GameObject metreElementPrefab;
    public void CreateMetreElements(int count)
    {
        for (int i = container.childCount - 1; i>=0; i--)
        {
            GameObject.Destroy(container.GetChild(i).gameObject);
        }
            for (int i = 0; i<count; i++)
        {
            GameObject.Instantiate(metreElementPrefab, container.transform);
        }
    }
    public void SetMetreFill(List<Question>completedQuestions, int passedQuestionCount)
    {
        int index = 0;
        foreach (Transform t in container)
        {
            if (completedQuestions.Count > index)
            {
                Question q = completedQuestions[index];
                if (q.isSolved)
                {
                    t.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    t.GetComponent<Image>().color = Color.red;
                }
            }
            else
            {
                if(index < completedQuestions.Count + passedQuestionCount)
                {
                    t.GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    t.GetComponent<Image>().color = Color.grey;
                }
            }
            /*if (completed > 0)
            {
                completed--;
                t.GetComponent<Image>().color = Color.green;
            }
            else if (passed > 0)
            {
                passed--;
                t.GetComponent<Image>().color = Color.yellow;
            }
            else if (incompleted > 0)
            {
                incompleted--;
                t.GetComponent<Image>().color = Color.grey;
            }*/
            index++;
        }
    }
    public void SetMetreFill(int incompleted, int passed, int completed)
    {
        foreach(Transform t in container)
        {
            if (completed > 0)
            {
                completed--;
                t.GetComponent<Image>().color = Color.green;
            }
            else if(passed > 0)
            {
                passed--;
                t.GetComponent<Image>().color = Color.yellow;
            }
            else if (incompleted > 0)
            {
                incompleted--;
                t.GetComponent<Image>().color = Color.grey;
            }
        }
    }
}

[System.Serializable]
public class Popup : UIElement
{
    public bool isOpen = false;
    public void Open()
    {
        isOpen = true;
        container.gameObject.SetActive(true);
    }
    public void Close()
    {
        isOpen = false;
        container.gameObject.SetActive(false);
    }
}
[System.Serializable]
public class RoundPopup : Popup
{
    public Text roundText;
    public void TriggerRoundPopup(string roundString)
    {
        roundText.text = roundString;

    }
}
[System.Serializable]
public class InstructionBox : Popup
{
    public Text content;
    public Text title;
    public void SetInstructions(string contentString, string titleString)
    {
        content.text = contentString;
        title.text = titleString;
    }
}

