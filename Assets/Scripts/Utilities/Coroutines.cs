using System.Collections;
using UnityEngine;
using System;

namespace MiniGameWorld.Utilities
{
    public static class Coroutines
    {
        private static MonoBehaviour s_CoroutineRunner;

        public static bool IsInitialized => s_CoroutineRunner != null;

        public static void Initialize(MonoBehaviour runner)
        {
            s_CoroutineRunner = runner;
        }

        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (s_CoroutineRunner == null)
            {
                throw new InvalidOperationException("Coroutine Runner is not Initialized");
            }

            return s_CoroutineRunner.StartCoroutine(coroutine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            if (s_CoroutineRunner != null)
            {
                s_CoroutineRunner.StopCoroutine(coroutine);
            }
        }

        public static void StopCoroutine(ref Coroutine coroutine)
        {
            if (s_CoroutineRunner != null && coroutine != null)
            {
                s_CoroutineRunner.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}


