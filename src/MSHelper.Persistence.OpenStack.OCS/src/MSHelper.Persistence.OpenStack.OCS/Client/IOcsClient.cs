using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MSHelper.Persistence.OpenStack.OCS.OcsTypes;
using MSHelper.Persistence.OpenStack.OCS.OcsTypes.Definition;

namespace MSHelper.Persistence.OpenStack.OCS.Client;

public interface IOcsClient
{
    Task<IOperationResult<byte[]>> GetObjectAsByteArray(params string[] relativePath);
    Task<IOperationResult<Stream>> GetObjectAsStream(params string[] relativePath);
    Task<IOperationResult<string>> GetObjectAsBase64String(params string[] relativePath);
    Task<IOperationResult<List<OcsObjectMetadata>>> GetDirectoryList(params string[] relativePath);
    Task<IOperationResult> UploadAsStream(Stream stream, params string[] relativePath);
    Task<IOperationResult> UploadAsBase64String(string base64String, params string[] relativePath);
    Task<IOperationResult> CopyInternally(string relativeSourcePath, string relativeDestinationPath);
    Task<IOperationResult> Delete(params string[] relativePath);
}