using System.IO;
using UnityEngine;

namespace Code.Services
{
    public class CacheService : ICacheService
    {
        public Texture2D LoadFromCache(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            texture.Apply();
            return texture;
        }

        public void CacheTexture(byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
        }

        public bool IsCached(string path)
        {
            return File.Exists(path);
        }
    }
}