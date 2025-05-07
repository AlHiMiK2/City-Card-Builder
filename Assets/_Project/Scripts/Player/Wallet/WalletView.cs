using _Project.Scripts.City.Wallet;
using _Project.Scripts.Player;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Player _targetPlayer;
    [SerializeField] private string _prefix;
    [SerializeField] private TMP_Text _view;

    private Wallet _wallet;

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.ScoreChanged += UpdateView;
    }

    private void OnEnable()
    {
        if (_wallet != null)
        {
            _wallet.ScoreChanged += UpdateView;
        }
    }
    
    private void OnDisable()
    {
        _wallet.ScoreChanged -= UpdateView;
    }
    
    private void UpdateView(int money)
    {
        _view.text = _prefix + money;
    }
}
