using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreviewPanel : MonoBehaviour
{
    [SerializeField] private Button visitButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI title;

    public void Set(string titleName, Action visitAction, Action closeAction) 
    {
        title.text = titleName;
        //make sure the buttons have no listeners
        visitButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        visitButton.onClick.AddListener(() => visitAction?.Invoke());
        closeButton.onClick.AddListener(() => closeAction?.Invoke());
    }

}
