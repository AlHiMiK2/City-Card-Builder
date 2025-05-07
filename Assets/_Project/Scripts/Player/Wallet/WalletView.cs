using System;
using System.Collections.Generic;
using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using _Project.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Player _targetPlayer;
    [SerializeField] private string _moneyPrefix;
    [SerializeField] private TMP_Text _moneyView;

    private List<IDisposable> _disposables = new ();
    private Wallet _wallet;
    
    private void Start()
    {
        _wallet = _targetPlayer.Wallet;
        
        _wallet.Money
            .Subscribe(UpdateView)
            .AddTo(_disposables);
    }

    private void UpdateView(int money)
    {
        _moneyView.text = _moneyPrefix + money;
    }

    private void OnDestroy()
    {
        Dispose();
    }
    
    private void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
        _disposables.Clear();
    }
}
