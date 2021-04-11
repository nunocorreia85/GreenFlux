using System.Collections.Generic;

namespace GreenFlux.Application.Dto
{
    public class SuggestionDto
    {
        public IEnumerable<ConnectorDto> ConnectorsToRemove { get; set; }
    }
}