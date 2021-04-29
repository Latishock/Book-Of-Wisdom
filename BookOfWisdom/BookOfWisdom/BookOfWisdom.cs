namespace BookOfWisdom
{
    using UnityEngine;
    namespace Patterns
    {
        public static class Singleton
        {
            public static void Initialize<T>(T instance, ref T instanceField, GameObject instanceGameObject)
            {
                if (instanceField == null)
                {
                    instanceField = instance;
                }
                else
                {
                    Object.Destroy(instanceGameObject);
                }
            }
        }
    }

}