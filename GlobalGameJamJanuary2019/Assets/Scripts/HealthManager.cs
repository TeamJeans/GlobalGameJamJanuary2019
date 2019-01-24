using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	[SerializeField]
	int maxHealth;
	public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

	[SerializeField]
	int currentHealth;
	public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

	[SerializeField]
	float invincibilityLength;
	float invincibilityCounter;

	[SerializeField]
	float flashLength = 0.1f;
	float flashCounter;

	[SerializeField]
	Renderer objectRenderer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (invincibilityCounter > 0)
		{
			invincibilityCounter -= Time.deltaTime;
			flashCounter -= Time.deltaTime;

			if (flashCounter <= 0)
			{
				objectRenderer.enabled = !objectRenderer.enabled;
				flashCounter = flashLength;
			}

			if (invincibilityCounter <= 0)
			{
				objectRenderer.enabled = true;
			}
		}
	}

	public void damage(int amount, Vector3 knockBackDir, float knockBackForce, float knockBackTime)
	{
		if (invincibilityCounter <= 0)
		{
			if ((currentHealth - amount) <= 0)
			{
				// Dead
				currentHealth = 0;
			}
			else
			{
				currentHealth -= amount;
			}

			gameObject.GetComponent<PlayerController>().knockBack(knockBackDir, knockBackForce, knockBackTime);

			invincibilityCounter = invincibilityLength;

			flashCounter = flashLength;
		}
	}

	public void heal(int amount)
	{
		if ((currentHealth + amount) >= maxHealth)
		{
			currentHealth = maxHealth;
		}
		else
		{
			currentHealth += amount;
		}
	}
}
