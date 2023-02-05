using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private Gradient lifeBarGradient;
    [SerializeField] private float maxLife = 30f;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

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

    public float Score
    {
        get { return score; }
        set
        {
            score = value;

            var intScore = (int)value;
            scoreText.text = "Score: " + intScore.ToString();
        }
    }

    [SerializeField] private float score;
}
