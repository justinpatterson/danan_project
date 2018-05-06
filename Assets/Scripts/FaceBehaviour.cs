using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBehaviour : MonoBehaviour {
    /// <summary>
    /// Faces contain their own lists
    /// Hold the lists
    /// </summary>
    public enum FaceTypes {Sad, Neutral, Happy}
    public FaceTypes face = FaceTypes.Neutral;
    void OnMouseDown()
    {
        Debug.Log("Badabingbadaboom " + face.ToString());
        GameManager.instance.ReportFaceClick(face, transform.position); //Checks which face has been clicked
    }
}
