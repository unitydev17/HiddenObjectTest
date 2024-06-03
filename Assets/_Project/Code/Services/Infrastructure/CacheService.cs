using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Code.Services
{
    public class CacheService : ICacheService
    {
        
        private readonly Dictionary<string, Texture2D> _memoryCache = new Dictionary<string, Texture2D>();
        public Texture2D LoadFromCache(string path)
        {
            if (_memoryCache.ContainsKey(path)) return _memoryCache.GetValueOrDefault(path);
            
            var bytes = File.ReadAllBytes(path);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            texture.Apply();
            
            _memoryCache.Add(path, texture);
            
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