using System.Collections.Generic;
using GreenFlux.Application.Dto.Queries;

namespace GreenFlux.Application.Dto.Commands
{
    public class SuggestionDto
    {
        public List<ConnectorDto> ConnectorsToRemove { get; set; }
    }
}