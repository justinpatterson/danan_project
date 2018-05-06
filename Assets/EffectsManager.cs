using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {
    public GameObject CorrectParticle, IncorrectParticle;
    public enum EffectTypes
    {
        goodParticle, badParticle
    }
    public void CreateEffectAtPoint(EffectTypes effect, Vector3 point)
    {
        switch (effect)
        {
            case EffectTypes.goodParticle:
                Instantiate(CorrectParticle, point, Quaternion.identity);
                break;
            case EffectTypes.badParticle:
                Instantiate(IncorrectParticle, point, Quaternion.identity);
                break;
        }
    }
}
