using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject shopPanel;
    public CanvasGroup shopCanvasGroup;
    public ShopLogic shopLogic;

    public float fadeDuration = 0.3f;

    private bool isInTrigger = false;
    private bool isShopOpen = false;

    void Update()
    {
        if (isInTrigger)
        {
            interactPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F) && !isShopOpen)
            {
                StartCoroutine(OpenShop());
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }

        if (isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(CloseShop());
        }
    }

    public void ClosingShopUI()
    {
        StartCoroutine(CloseShop());
    }
    IEnumerator OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
        isShopOpen = true;

        if (shopLogic != null)
            shopLogic.OpenShop();

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            shopCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        shopCanvasGroup.alpha = 1f;
    }

    IEnumerator CloseShop()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            shopCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        shopCanvasGroup.alpha = 0f;

        if (shopLogic != null)
            shopLogic.CloseShop();

        shopPanel.SetActive(false);
        Time.timeScale = 1f;
        isShopOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}