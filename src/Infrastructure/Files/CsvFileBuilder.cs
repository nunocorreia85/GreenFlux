using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.ChargeStations.Queries.ExportTodos;
using GreenFlux.Infrastructure.Files.Maps;

namespace GreenFlux.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildGroupsFile(IEnumerable<GroupRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<GroupRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}