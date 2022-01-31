using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Player : MonoBehaviour, IDamageable
{

	private static Player _instance;
	public static Player Instance => _instance;

	public Action<float> OnPlayerHealthChange;

	public float _health = 100;
	private float _totalHealth = 100;
	private bool _invincible;

	[SerializeField] private AudioClip _healSound;
	[SerializeField] private AudioClip _invincibleSound;
	[SerializeField] private SkinnedMeshRenderer _myMesh;
	[SerializeField] private GameObject _bloodType;
	private Material _myMat;

	public Animator animator;

	private Rigidbody _rb;



	private void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		_myMat = _myMesh.material;
		_health = _totalHealth;
	}

	public void TakeDamage(float amount, Transform source = null, Vector3 contactPoint = default(Vector3))
	{
		if (!_invincible)
		{
			_health -= amount;
			animator.SetTrigger("hit");
			_myMat.color = Color.red;
			Splatter();
			OnPlayerHealthChange?.Invoke(_health);
			if (_health <= 0)
			{
				_health = 0;
				Die();
			}
		}
	}

	void Update()
	{
		_myMat.color = Color.Lerp(_myMat.color, Color.white, Time.deltaTime * 3);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PickUps>(out var pickup))
		{
			if (pickup.Type == PickUps._powerUpType.Health)
			{
				Heal(pickup.HealAmount);
			}
			else if (pickup.Type == PickUps._powerUpType.SporkTime)
			{
				StartCoroutine(SetInvincible());
			}
			Destroy(pickup.gameObject);
		}
	}

	private IEnumerator SetInvincible()
	{
		_invincible = true;
		//play invinicble animation
		AudioManager.Instance.PlayAudioClip(_invincibleSound, 0.5f);
		yield return new WaitForSeconds(5f);
		_invincible = false;
	}

	private void Splatter()
	{
		var yOffset = new Vector3(0, 1f, 0);
		var splatter = Instantiate<GameObject>(_bloodType, transform.position + yOffset, Quaternion.identity);
		Destroy(splatter, 20f);
	}

	public void Heal(float amount)
	{
		AudioManager.Instance.PlayAudioClip(_healSound, 0.5f);
		_health += amount;
		OnPlayerHealthChange?.Invoke(_health);
	}

	private void Die()
	{
		//Game over event?
		GameManager.Instance.GameOver();
		//More GameOver event logic. E.G. Noises, animations play, player falls down, enemies celebrate.

		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		transform.GetChild(0).gameObject.SetActive(false); //Temp circumvention of goofy AudioListener console spam
														   //Enemies currently die as well

		//YOU DEAD
	}

	private void OnCollisionEnter(Collision collision)
	{
		Collider myCollider = collision.contacts[0].thisCollider;
		Enemy.EnemyType eType;

		if (myCollider.gameObject.TryGetComponent<Utensil>(out var utensil) && collision.gameObject.TryGetComponent<Enemy>(out var enemy))
		{
			eType = enemy._enemyType;
			if (utensil.GetUtensilType() == Utensil.UtensilType.FORK && eType == Enemy.EnemyType.FORKABLE)
			{
				//Debug.Log("FORK ON FORK ACTION");
				enemy.TakeDamage(utensil.GetUtensilDamage(), this.transform, collision.contacts[0].point);				
			}
			if (utensil.GetUtensilType() == Utensil.UtensilType.SPOON && eType == Enemy.EnemyType.SPOONABLE)
			{
				//Debug.Log("SPOON ON SPOON ACTION");
				enemy.TakeDamage(utensil.GetUtensilDamage(), this.transform, collision.contacts[0].point);
			}
		}
	}
}
