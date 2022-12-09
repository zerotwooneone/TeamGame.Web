namespace TeamGame.Domain.Util
{
    public class ObservableProperty<T> : IDisposable
    {
        public Assignable<T> Assignable { get; private set; }
        public readonly IObservable<T> Observable;
        private readonly IDisposable _subscription;
        private ObservableProperty(
            Assignable<T> assignable, 
            IObservable<T> observable,
            IDisposable subscription)
        {
            Assignable = assignable;
            Observable = observable;
            _subscription = subscription;
        }

        public static ObservableProperty<T> Create(IObservable<T> observable)
        {
            ObservableProperty<T>? result = null;
            var subscription = observable.Subscribe(v =>
            {
                if (result == null)
                {
                    return;
                }
                result.Assignable = Assignable<T>.Create(v);
            });
            result = new ObservableProperty<T> (Assignable<T>.Unassigned,  observable, subscription);
            
            return result;
        }
        public static ObservableProperty<T> Create(T value, IObservable<T> observable)
        {
            ObservableProperty<T>? result = null;
            var subscription = observable.Subscribe(v =>
            {
                if (result == null)
                {
                    return;
                }
                result.Assignable = Assignable<T>.Create(v);
            });
            result = new ObservableProperty<T> (Assignable<T>.Create(value), observable, subscription);
            return result;
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
