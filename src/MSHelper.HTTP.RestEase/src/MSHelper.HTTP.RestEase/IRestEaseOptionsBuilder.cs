using System;

namespace MSHelper.HTTP.RestEase;

public interface IRestEaseOptionsBuilder
{
    IRestEaseOptionsBuilder WithLoadBalancer(string loadBalancer);
    IRestEaseOptionsBuilder WithService(Func<IRestEaseServiceBuilder, IRestEaseServiceBuilder> buildService);
    RestEaseOptions Build();
}