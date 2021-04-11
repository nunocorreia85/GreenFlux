using System.Collections.Generic;

namespace GreenFlux.Application.Dto
{
    public class AddConnectorResponseDto
    {
        public int? AddedConnectorId { get; set; }
        public List<SuggestionDto> Suggestions { get; set; }
    }
}