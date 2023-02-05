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

    [Header("Sounds")]
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource hitBonesSound;
    [SerializeField] private AudioSource deathSound;

    private void Awake()
    {
        // setup event listeners
        GameController.playerDied += OnDeath;
        PlayerController.HitBones += OnHitBones;
        PlayerController.HitObstacle += OnHit;
        PlayerController.CollectedWater += OnCollect;
    }

    private void OnDestroy()
    {
        // clear event listeners
        GameController.playerDied -= OnDeath;
        PlayerController.HitBones -= OnHitBones;
        PlayerController.HitObstacle -= OnHit;
        PlayerController.CollectedWater -= OnCollect;
    }

    public float Life
    {
        get { return life; }
        set
        {
            if (value > 30) value = 30;
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

    public void OnCollect()
    {
        collectSound.Play();
    }

    public void OnHit()
    {
        hitSound.Play();
    }
    public void OnHitBones()
    {
        hitBonesSound.Play();
    }

    public void OnDeath()
    {
        deathSound.Play();
    }
}
