using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private float maxLife = 30f;

    [SerializeField] private Slider lifeBar;
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private Gradient lifeBarGradient;

    public float Life
    {
        get { return life; }
        set
        {
            life = value;

            lifeBar.value = value;
            lifeBarFill.color = lifeBarGradient.Evaluate(lifeBar.normalizedValue);
        }
    }

    [SerializeField] private float life;
}
