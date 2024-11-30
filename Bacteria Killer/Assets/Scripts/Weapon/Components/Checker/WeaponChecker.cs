namespace Weapon
{
    public abstract class WeaponChecker : IShootChecker
    {
        public IShootChecker NextChecker { get; set; }
        
        public virtual void Check()
        {
            NextChecker?.Check();
        }
    }
}