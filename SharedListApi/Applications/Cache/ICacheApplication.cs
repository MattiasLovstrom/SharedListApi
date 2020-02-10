namespace SharedListApi.Applications.Cache
{
    public interface ICacheApplication
    {
        void Add<T>(string cacheKey, T val);
        T Get<T>(string cacheKey);
    }
}