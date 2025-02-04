namespace Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public void Save(string key, object data);
        public T Load<T>(string key);
    }
}