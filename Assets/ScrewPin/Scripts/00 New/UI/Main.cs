using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : GameMonobehavior
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _coinText;


    

    private void OnEnable()
    {
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Gold, UpdateTextCoin);
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Level, UpdateLevelText);


    }

    private void OnDisable()
    {
        Dtm.UnregisterResourceTypeChangedListener(ResourceType.Gold, UpdateTextCoin);
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Level, UpdateLevelText);


    }

    private void Start()
    {
        UpdateTextCoin(ResourceType.Gold);
        UpdateLevelText(ResourceType.Level); // Initial update
    }

    

    public void UpdateTextCoin(ResourceType resourceType)
    {
        _coinText.text = Dtm.Gold.ToString();
        Debug.Log(Dtm.Gold + " " + resourceType);
    }


    public void PlayGame()
    {
        Ac.PlaySound(Ac.click);
        gameObject.SetActive(false);
        UIManager.Instance.ShowInGameUi();
        Gm.cantClick = false;
        

    }

    public void UpdateLevelText(ResourceType resourceType)
    {
        _levelText.text = "LEVEL " + Dtm.Level.ToString();
    }
    



}
