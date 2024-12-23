using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPopUps : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] GameObject popUpFine;
    [SerializeField] GameObject popUpDamaged;
    [SerializeField] GameObject popUpCaution;
    [SerializeField] GameObject popUpDanger;


    [SerializeField] float timeBeforeFadeOutBegins;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void DisableAllPopUps()
    {
        popUpFine.SetActive(false);
        popUpDamaged.SetActive(false);
        popUpCaution.SetActive(false);
        popUpDanger.SetActive(false);
    }
    public void DisplayHealthPopUp(int playerHealth)
    {
        if (playerHealth >= 100)
        {
            popUpFine.SetActive(true);
        }
        else if (playerHealth >= 66 && playerHealth <= 99)
        {
            popUpDamaged.SetActive(true);
        }
        else if (playerHealth >= 30 && playerHealth <= 65)
        {
            popUpCaution.SetActive(true);
        }
        else if (playerHealth >= 1 && playerHealth <= 29)
        {
            popUpDanger.SetActive(true);
        }

        StartCoroutine(FadeInPopUp());
    }
    private IEnumerator FadeInPopUp()
    {
        for (float fade = .05f; fade < 1; fade = fade + .05f)
        {
            canvasGroup.alpha = fade;
            if (fade > .9f)
            {
                StartCoroutine(FadeOutPopUp());
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    private IEnumerator FadeOutPopUp()
    {
        yield return new WaitForSeconds(timeBeforeFadeOutBegins);
        for (float fade = 1; fade > 0; fade = fade - .05f)
        {
            canvasGroup.alpha = fade;
            if (fade <= 0.05f)
            {
                DisableAllPopUps();
            }
            yield return new WaitForSeconds(.05f);
        }

    }
}
