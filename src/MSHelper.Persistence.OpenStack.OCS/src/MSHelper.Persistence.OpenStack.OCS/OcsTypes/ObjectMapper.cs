﻿using System.Collections.Generic;
using System.Linq;
using MSHelper.Persistence.OpenStack.OCS.OpenStack.Responses;

namespace MSHelper.Persistence.OpenStack.OCS.OcsTypes;

internal static class ObjectMapper
{
    public static OcsObjectMetadata Map(ObjectMetadata objectMetadata)
        => new(objectMetadata.hash,
            objectMetadata.last_modified,
            objectMetadata.bytes,
            objectMetadata.name,
            objectMetadata.content_type);

    public static IEnumerable<OcsObjectMetadata> Map(IEnumerable<ObjectMetadata> objectMetadataCollection)
        => objectMetadataCollection.Select(Map);
}