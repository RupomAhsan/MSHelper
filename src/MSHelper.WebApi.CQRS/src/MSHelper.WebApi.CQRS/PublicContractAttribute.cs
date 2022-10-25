using System;

namespace MSHelper.WebApi.CQRS;

//Marker
[AttributeUsage(AttributeTargets.Class)]
public class PublicContractAttribute : Attribute
{
}