using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(PlayerWallet))]
    public class Player : BasePlayer
    {
        private void Awake()
        {
            Wallet = GetComponent<PlayerWallet>();
        }
    }
}