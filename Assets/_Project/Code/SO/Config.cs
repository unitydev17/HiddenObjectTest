using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "Config")]
    public class Config  : ScriptableObject
    {
        public string configUrl;
        public int timeout;

    }
}