using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    public float m_CurrentHealth;
    private bool m_Dead;
    //////////////////////////    
    public float m_addLife = 20f;
    public bool m_PowerUpCoin;
    public bool m_SpeedCoin;

    //스테이지 새로시작시 코인,파워 초기화

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        int randomEffect;
        randomEffect = Random.Range(0, 3);
        if (collider.gameObject.CompareTag("Coin"))
        {
            collider.gameObject.SetActive(false);
            StartCoroutine(WhenMeetCoin(1));
        }
        SetHealthUI();
    }

    IEnumerator WhenMeetCoin(int randomEffect)
    {

        if (randomEffect == 0)
        {
            if (m_CurrentHealth + m_addLife <= 100)
                m_CurrentHealth += m_addLife;
            else
                m_CurrentHealth = m_StartingHealth;
            yield break;
        }
        else if (randomEffect == 1)
        {               
            m_PowerUpCoin = true;
            yield return new WaitForSeconds(4f);
            m_PowerUpCoin = false;

        }
        else if (randomEffect == 2)     
        {
            m_SpeedCoin = true;
            yield break;
        }

    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;
        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;

        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }
}

