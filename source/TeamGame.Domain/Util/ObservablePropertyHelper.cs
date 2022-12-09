using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TeamGame.Domain.Util;

public class ObservablePropertyHelper<T> : IDisposable
{
    public readonly ObservableProperty<T> Property;
    private readonly ISubject<T> _subject;
    private ObservablePropertyHelper(ObservableProperty<T> property, ISubject<T> subject)
    {
        Property = property;
        _subject = subject;
    }

    public static ObservablePropertyHelper<T> Create()
    {
        Subject<T> subject = new Subject<T>();
        var connectible = subject
            .AsObservable()
            .Replay(1);

        var helper = new ObservablePropertyHelper<T>(ObservableProperty<T>.Create(subject), subject);
        connectible.Connect();
        return helper;
    }

    public static ObservablePropertyHelper<T> Create(T value)
    {
        BehaviorSubject<T> subject = new BehaviorSubject<T>(value);
        return new ObservablePropertyHelper<T>(ObservableProperty<T>.Create(value, subject.AsObservable()),
            subject);
    }

    public void OnNext(T value)
    {
        _subject.OnNext(value);
    }

    public void Dispose()
    {
        Property.Dispose();
        _subject.OnCompleted();
    }
}