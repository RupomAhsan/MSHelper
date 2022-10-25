namespace MSHelper.MessageBrokers;

public interface ICorrelationContextAccessor
{
    object CorrelationContext { get; set; }
}