using System.Collections.Generic;
using GreenFlux.Domain.Common;

namespace GreenFlux.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildGroupsFile(IEnumerable<AuditableEntity> records);
    }
}