using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShellExplosion : MonoBehaviour
{
	public LayerMask m_TankMask;
	public ParticleSystem m_ExplosionParticles;
	public AudioSource m_ExplosionAudio;
	public float m_MaxDamage = 100f;
	public float m_ExplosionForce = 1000f;
	public float m_MaxLifeTime = 2f;
	public float m_ExplosionRadius = 5f;
	//미사일 데미지,발사속도 등등
	//public TankShooting shooter;
    private float m_nomalDamage;
    public float m_PowerUpDamage;
    public bool m_ChangedPower;


	private void Start()
	{
		Destroy(gameObject, m_MaxLifeTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		// Find all the tanks in an area around the shell and damage them.
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

		for (int i = 0; i < colliders.Length; i++)
		{
			Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();

			if (!targetRigidBody)
			{
				continue;
			}

			targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

			TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>();

			if (!targetHealth)
			{
				continue;
			}
			float damage;
			
			damage = CalculateDamage(targetRigidBody.position);
			targetHealth.TakeDamage(damage);
		}

		m_ExplosionParticles.transform.parent = null;
		m_ExplosionParticles.Play();
		m_ExplosionAudio.Play();

		Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
		Destroy(gameObject);
	}

	private float CalculateDamage(Vector3 targetPosition)
	{
		Vector3 explosionToTarget = targetPosition - transform.position;
		float explosionDistance = explosionToTarget.magnitude;
		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
		float damage;
		if (m_PowerUpDamage > 0)
		{
			damage = m_PowerUpDamage + (relativeDistance * m_MaxDamage);
			m_ChangedPower = true;
		}
		else
			damage = relativeDistance * m_MaxDamage; 

		damage = Mathf.Max(0f, damage);

		return damage;
	}

	IEnumerator Delay_Time(Rigidbody targetRigidBody)
	{
		float damage;
		yield return new WaitForSeconds(4f);
		damage = CalculateDamage(targetRigidBody.position);

	}
}

