#nullable enable

namespace System
{
    internal sealed class ImplFunc<TResult> : IFunc<TResult>
    {
        private readonly Func<TResult> func;

        public ImplFunc(
            Func<TResult> func)
            =>
            this.func = func;

        public TResult Invoke()
            =>
            func.Invoke();
    }
}