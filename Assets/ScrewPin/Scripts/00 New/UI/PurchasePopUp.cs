using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchasePopUp : GameMonobehavior
{
    [SerializeField] private int _cost;
    [SerializeField] private TMP_Text _coinText;

    private void Start()
    {
        UpdateCostText();
    }
    public void Close()
    {
        Time.timeScale = 1f;
        Ac.PlaySound(Ac.closePopup);
        Gm.cantClick = false;

        gameObject.SetActive(false);

    }
    public void UpdateCostText()
    {
        _coinText.text = _cost.ToString();
    }
    public void BuyLife()
    {
        if (Dtm.Gold < _cost)
        {
            Debug.Log("not enough money");
            return;
        }
        DescreaCoin(_cost);
        InscreaseLife(1);
        Ac.PlaySound(Ac.click);
    }

    public void DescreaCoin(int amount)
    {
        if (Dtm.Life >= 5) return;
        Dtm.Gold -= amount;
    }

    public void InscreaseLife(int amount)
    {
        if (Dtm.Life >= 5) return;
        Dtm.Life += amount;
    }
}
