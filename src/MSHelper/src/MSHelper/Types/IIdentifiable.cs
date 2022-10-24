namespace MSHelper.Types;

public interface IIdentifiable<out T>
{
    T Id { get; }
}