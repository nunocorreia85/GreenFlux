using System;
using System.Collections.Generic;
using System.Linq;
using GreenFlux.Domain.Entities;

namespace GreenFlux.Application.Utils
{
    public static class CombinationsCalculator {
        public static List<Connector[]> GetCombinations(List<Connector> connectors, float targetNum)
        {
            var combinations = new List<Connector[]>();
            SumUpRecursive(connectors, targetNum, new List<Connector>(), combinations);
            return combinations;
        }

        private static void SumUpRecursive(IReadOnlyList<Connector> connectors, float target, List<Connector> partial, ICollection<Connector[]> combinations)
        {
            var s = partial.Sum(connector => connector.MaxCurrent);

            if (Math.Abs(s - target) < 0.000001)
                combinations.Add(partial.ToArray());

            if (s >= target)
                return;
                
            for (var i = 0; i < connectors.Count; i++)
            {
                var remaining = new List<Connector>();
                var n = connectors[i];
                for (int j = i + 1; j < connectors.Count; j++) remaining.Add(connectors[j]);

                var partialRec = new List<Connector>(partial) {n};
                SumUpRecursive(remaining, target, partialRec, combinations);
            }
        }
    }
}