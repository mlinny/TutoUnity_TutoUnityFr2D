using System;
using System.Collections;
using System.Xml.Schema;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public SpriteRenderer graphics;
    public bool isInvicible = false;
    public float InvicibilityFlashDelay = 0.2f;


/// <summary>
/// Return True si le soin a été nécessaires
/// </summary>
/// <param name="amount"></param>
/// <returns></returns>
internal bool  HealPlayer(int amount)
    {
        if (currentHealth >= maxHealth) return false;
        if ((currentHealth + amount) > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;
        ActualiseBarre();
        return true;
    }

    public float InvicibilityDuration = 3f;

    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //ActualiseBarre();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }

    public void TakeDamage(int _damage)
    {
        if (!isInvicible)
        {
            currentHealth -= _damage;
            //healthBar.SetHealth(currentHealth);
            ActualiseBarre();
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvibilityDuration());

        }
    }
    #region "Take Damage méthods"
    private IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);

        }

    }
    private IEnumerator HandleInvibilityDuration()
    {
        if (isInvicible)
        {
            yield return new WaitForSeconds(InvicibilityDuration);
            isInvicible = false;
        }


    }

    #endregion

    private void Die()
    {
        Debug.Log("Vous êtes mort");
        //bloquer les mouvements du personnages
        PlayerMouvement.instance.enabled = false;
        //jouer l'animation d'élimination
        PlayerMouvement.instance.animator.SetTrigger("Die");
        //empêcher les interactions physiques
        PlayerMouvement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMouvement.instance.playerCollider.enabled = false;
    }



    void ActualiseBarre()
    {
        healthBar.SetHealth(currentHealth);
    }


}
