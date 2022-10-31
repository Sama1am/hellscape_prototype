using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playerManager : MonoBehaviour
{

    [Header("set")]
    [SerializeField] public float health;
    public float falseHeart;
    public bool hasFalseHeart;

    [Header("do not set")]
    [SerializeField] public float damage;
    public float currentHealth;
    [SerializeField] public float damageTakenOffset;
    [SerializeField] private Slider playerHealth;

    private Rigidbody2D _rb;
    private SpriteRenderer _sp;
    private Color ogColor;

    [SerializeField] private bool hasfalseheart;
    [SerializeField] private float falseHeartHealth;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private Vignette _vin;

    // Start is called before the first frame update
    void Start()
    {
        _globalVolume = GameObject.FindGameObjectWithTag("globalVolume").GetComponent<Volume>();
        _globalVolume.profile.TryGet<Vignette>(out _vin);
        _sp = GetComponentInChildren<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        ogColor = _sp.color;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        setUI();
        healthFeedback();

        if (currentHealth > health)
        {
            currentHealth = health;
        }
    }

    public void takeDamage(float dam)
    {
        if(hasfalseheart)
        {
            falseHeartHealth -= dam;

            if (falseHeartHealth <= 0)
            {
                hasfalseheart = false;
                falseHeartHealth = 0;
                checkHealth();
            }
        }
        else
        {
            currentHealth -= dam;
            StartCoroutine("changeColour");
            //Debug.Log("THE PLAYER TOOK " + dam + "DAMAGE!");

            checkHealth();
        }

        

    }

    public float getHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return health;
    }

    public void checkHealth()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void setFalseHeart()
    {
        hasfalseheart = true;
        falseHeartHealth++;
    }

    private void healthFeedback()
    {
        if(currentHealth <= 5 && currentHealth > 4)
        {

            _vin.intensity.value = 0.5f;
            _vin.smoothness.value = 1f;
        }
        else if(currentHealth <= 4 && currentHealth > 3)
        {
            _vin.intensity.value = 0.6f;
            _vin.smoothness.value = 1f;
        }
        else if(currentHealth <= 3 && currentHealth > 2)
        {
            _vin.intensity.value = 0.7f;
            _vin.smoothness.value = 1f;
        }
        else if(currentHealth <= 2)
        {
            _vin.intensity.value = 1f;
            _vin.smoothness.value = 1f;
        }
        else if(currentHealth > 5)
        {
            _vin.intensity.value = 0f;
            _vin.smoothness.value = 0f;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            _rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }
    }

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _sp.color = ogColor;
    }

    void setUI()
    {
        playerHealth.maxValue = health;
        playerHealth.value = currentHealth;
    }

}
