namespace WeatherAppWPF.Interfaces
{
    public interface IFileProvider
    {
        public void Save(object data, string fileName);
        public T Load<T>(string fileName);  
    }
}
