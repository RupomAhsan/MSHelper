namespace MSHelper.MessageBrokers;

public interface IMessagePropertiesAccessor
{
    IMessageProperties MessageProperties { get; set; }
}