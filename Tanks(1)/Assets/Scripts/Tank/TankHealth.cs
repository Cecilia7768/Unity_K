using UnityEngine;
using UnityEngine.UI;

//다른 클래스에도 쓸수있는 전역변수 필요함!
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
    public static bool m_eattenCoin; //코인 먹으면 다른 챕터에서 활성화 여부 사용
    public float m_lifeCoin = 1;
    public float m_addLife = 20f;    

    private void OnTriggerEnter(Collider collider) //낸주 코드 정리 필요!!
    {
        //충돌여부 담을 변수필요
        if (collider.gameObject.CompareTag("Coin")) 
        {            
            collider.gameObject.SetActive(false);
            m_eattenCoin = true;
            Effectsometing();
        }       
        SetHealthUI();
    }
    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    private void Effectsometing()
    {
        int randomEffect;
        //float timer = 3f;

        CoinController coin = GetComponent<CoinController>();
        ShellExplosion sell = GetComponent<ShellExplosion>();
        TankMovement tankMovement = GetComponent<TankMovement>();

        randomEffect = Random.Range(0, 3);
    
        if (randomEffect == 0)
        {
            if (m_CurrentHealth + m_addLife <= 100)
                m_CurrentHealth += m_addLife;
            else
                m_CurrentHealth = m_StartingHealth;
        }
        else if (randomEffect == 1)
            sell.m_MaxDamage = sell.m_MaxDamage * 2; //데미지가 두배!
        else if (randomEffect == 2)
        {
            //코루틴사용 / while 안됨
            //if (timer == 3 && timer > 0)
            //    timer -= Time.deltaTime;               
            //else
            tankMovement.m_Speed *= 2; //스피드 두배
        }
        else
        { //연사?? 뭐하지
            //tankshooting script->Fire() 함수 체크

        }
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

