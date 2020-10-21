using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public SpriteRenderer graphics;
    public bool isInvicible = false;
    public float InvicibilityFlashDelay = 0.2f;
    public float InvicibilityDuration = 3f;

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
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int _damage)
    {
        if (!isInvicible)
        {
            currentHealth -= _damage;
            //healthBar.SetHealth(currentHealth);
            ActualiseBarre();
            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvibilityDuration());
        }
    }

    private IEnumerator InvicibilityFlash()
    {
        while (isInvicible )
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);

        }
        
    }
    private IEnumerator HandleInvibilityDuration()
    {
        if (isInvicible )
        {
            yield return new WaitForSeconds(InvicibilityDuration);
            isInvicible = false;
        }


    }


    void ActualiseBarre()
    {
        healthBar.SetHealth(currentHealth);
    }


}
