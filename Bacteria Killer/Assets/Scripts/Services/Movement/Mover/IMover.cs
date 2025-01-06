namespace Services.Movement.Mover
{
    public interface IMoverService
    {
        public void Move(float deltaTime, float speed);
    }
}