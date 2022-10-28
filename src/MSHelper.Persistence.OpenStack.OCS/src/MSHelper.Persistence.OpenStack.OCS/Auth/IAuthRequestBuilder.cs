using System.Collections.Generic;
using MSHelper.Persistence.OpenStack.OCS.OpenStack.Requests;

namespace MSHelper.Persistence.OpenStack.OCS.Auth;

internal interface IAuthRequestBuilder
{
    IAuthRequestBuilder WithMethod(string method);
    IAuthRequestBuilder WithMethods(IEnumerable<string> methods);
    IAuthRequestBuilder WithProject(string projectId);
    IAuthRequestBuilder WithUser(string id, string password);
    AuthRequest Build();
}