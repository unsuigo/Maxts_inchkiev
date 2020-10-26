﻿using UniRx.Async;

namespace Game.Utils
{
    public static class UniTaskUtils
    {
        public static UniTask Delay(float time)
        {
            return UniTask.Delay((int)(time * 1000));
        }
    }
}

