using UnityEngine;

namespace Code.Services
{
    public interface ICacheService
    {
        public void CacheTexture(byte[] bytes, string path);

        public Texture2D LoadFromCache(string path);

        public bool IsCached(string path);
    }
}