using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public float Life
    {
        get { return life; }
        set
        {
            life = value;
        }
    }

    [SerializeField] private float life;
}
