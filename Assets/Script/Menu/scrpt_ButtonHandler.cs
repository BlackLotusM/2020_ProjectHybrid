using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;
using UnityEngine.SceneManagement;

public class scrpt_ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField]
    private bool OnlyAnim = false;
    [SerializeField]
    private bool Option = false;
    [SerializeField]
    private bool Host = false;
    [SerializeField]
    private bool Join = false;
    [SerializeField]
    private bool Exit = false;
    [SerializeField]
    private GameObject OptionPanel = null;

    [SerializeField]
    private GameObject HostPanel = null;
    [SerializeField]
    private GameObject JoinPanel = null;
    [SerializeField]
    private GameObject MainPanel = null;

    [SerializeField]
    private Animator anim = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("Pressed", true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("Hover", false);
        anim.SetBool("Pressed", false);
    }

    public void EnablePanel()
    {
        if (OnlyAnim)
        {

        }
        else
        {
            if (Option)
            {
                EnableOption();
            }
            if (Host)
            {
                EnableHost();
            }
            if (Join)
            {
                EnableJoin();
            }
            if (Exit)
            {
                Application.Quit();
            }
            MainPanel.SetActive(false);
        }
    }

    private void EnableOption()
    {
        OptionPanel.SetActive(true);
        HostPanel.SetActive(false);
        JoinPanel.SetActive(false);
    }

    private void EnableHost()
    {
        OptionPanel.SetActive(false);
        HostPanel.SetActive(true);
        JoinPanel.SetActive(false);
    }

    private void EnableJoin()
    {
        OptionPanel.SetActive(false);
        HostPanel.SetActive(false);
        JoinPanel.SetActive(true);
    }
}
