using System;

namespace GreenFlux.Application.Dto.Queries
{
    public class DtoBase
    {
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}