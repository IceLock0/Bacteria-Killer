namespace Weapon
{
    public interface IShootChecker
    {
        public IShootChecker NextChecker { get; }

        public void Check();
    }
}