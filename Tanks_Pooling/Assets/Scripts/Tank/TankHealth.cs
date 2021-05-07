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
    public bool m_SpeedCoin;
    public bool m_Check_Dead;

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
            collider.gameObject.SetActive(false); //먹은 코인은 비활성화
            //아래 세가지중 하나의 랜덤 능력을 얻음
            if (randomEffect == 0)
                StartCoroutine(WhenMeetCoin_Health()); //HP ++
            else if (randomEffect == 1)             
                StartCoroutine(WhenMeetCoin_About_Power());    //Power ++           
            else
                StartCoroutine(WhenMeetCoin_Speed()); //Speed ++
             
            CoinController.Use_CoinList(); //비활성화 된 리스트 내 코인 재사용     
        }
        SetHealthUI();
    }
    IEnumerator WhenMeetCoin_Health()
    {
        if (m_CurrentHealth + m_addLife <= 100)
            m_CurrentHealth += m_addLife;
        else
            m_CurrentHealth = m_StartingHealth;
        yield break;
    }
    IEnumerator WhenMeetCoin_About_Power()
    {
        TankShooting tankShooting = GetComponent<TankShooting>();
        tankShooting.m_bEatenPowerCoin += 2;
        yield return new WaitForSeconds(4f);
        tankShooting.m_bEatenPowerCoin = 0;
    }
    IEnumerator WhenMeetCoin_Speed()
    {
        m_SpeedCoin = true;
        yield break;
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

