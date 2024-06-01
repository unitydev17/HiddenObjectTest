using UnityEngine;

namespace Code.Utils
{
    public static class Extensions
    {
        public static void ClearChildren(this GameObject gameObject)
        {
            for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}