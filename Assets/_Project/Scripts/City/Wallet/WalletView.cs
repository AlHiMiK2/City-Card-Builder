using System;
using System.Collections.Generic;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using TMPro;
using UniRx;
using UnityEngine;

public class WalletView : MonoBehaviour
{
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
        Init();
    }

    private void Init()
    {
        _wallet = GameHandler.Instance.City.Wallet;
        
        _wallet.Wood
            .Subscribe(v => { _woodView.text = _woodPrefix + v; })
            .AddTo(_disposables);
        _wallet.Stone
            .Subscribe(v => { _stoneView.text = _stonePrefix + v; })
            .AddTo(_disposables);
        _wallet.Metal
            .Subscribe(v => { _metalView.text = _metalPrefix + v; })
            .AddTo(_disposables);
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
