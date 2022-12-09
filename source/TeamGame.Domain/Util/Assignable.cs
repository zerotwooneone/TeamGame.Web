namespace TeamGame.Domain.Util
{
    public struct Assignable<T>
    {
        public T Value { get; private set;}
        public bool HasBeenSet { get; private set; }
        public static readonly Assignable<T> Unassigned = new Assignable<T> { HasBeenSet = false };
        public static Assignable<T> Create(T value) { return new Assignable<T> { Value= value, HasBeenSet=true }; }
    }
}
