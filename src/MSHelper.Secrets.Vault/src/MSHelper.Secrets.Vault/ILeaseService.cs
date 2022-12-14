using System.Collections.Generic;

namespace MSHelper.Secrets.Vault;

public interface ILeaseService
{
    IReadOnlyDictionary<string, LeaseData> All { get; }
    LeaseData Get(string key);
    void Set(string key, LeaseData data);
}