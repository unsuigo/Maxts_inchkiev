using System.Threading;
using UniRx;
using UniRx.Async;

namespace ARRumzzy.Core.Utils
{
    public static class ReactiveUtils 
    {
        public static async UniTask<ReactiveProperty<T>> WaitForInitialization<T>(this ReactiveProperty<T> property)
            where T : class
        {
            if (property.Value is null)
            {
                await property.WaitUntilValueChangedAsync(CancellationToken.None);
            }

            return property;
        }
        
        public static async UniTask WaitForValue<T>(this ReactiveProperty<T> property, T value)
            where T : struct
        {
            if (property.Value.Equals(value))
            {
                return;
            }

            await property.WaitUntilValueChangedAsync(CancellationToken.None);
        }
    }
}


