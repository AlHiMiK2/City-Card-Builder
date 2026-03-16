namespace _Project.Scripts.Interfaces
{
    public interface IWallet
    {
        public bool TryTakeMoney(int money);
        public void AddMoney(int money);
    }
}