using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class DecisionCollection
    {
        public List<Decision> Decisions = new List<Decision>();
    }

    [Serializable]
    public class Decision
    {
        public int Number;
        public string Segment = "Society";
        public string DescriptionDE = "";
        public string DescriptionEN = "";
        public string ResultDescriptionDE = "";
        public string ResultDescriptionEN = "";
        public long Costs = 0;
        public short Pollution = 0;
        public short SocietySatisfaction = 0;
        public long SocietyCO2Emission = 0;
        public short IndustrySatisfaction = 0;
        public long IndustryCO2Emission = 0;
        public short EnergySectorSatisfaction = 0;
        public long EnergySectorCO2Emission = 0;
        public short AgricultureSatisfaction = 0;
        public long AgricultureCO2Emission = 0;
    }
}
