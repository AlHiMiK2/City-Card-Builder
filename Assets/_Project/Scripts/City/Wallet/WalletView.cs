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
    [SerializeField] private string _woodPrefix;
    [SerializeField] private TMP_Text _woodView;
    [SerializeField] private string _stonePrefix;
    [SerializeField] private TMP_Text _stoneView;
    [SerializeField] private string _metalPrefix;
    [SerializeField] private TMP_Text _metalView;

    private List<IDisposable> _disposables = new ();
    private Wallet _wallet;
    
    private void Start()
    {
        _wallet = _targetPlayer.Wallet;
        
        _wallet.Resources
            .Subscribe(UpdateView)
            .AddTo(_disposables);
    }

    private void UpdateView(CityResources resources)
    {
        _woodView.text = _woodPrefix + resources.Wood;
        _stoneView.text = _stonePrefix + resources.Stone;
        _metalView.text = _metalPrefix + resources.Metal;
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
