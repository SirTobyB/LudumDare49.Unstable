using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class Game
    {
        // the world/country
        public string Language = "en";
        public string PlayerName = "";
        public string CountryName = "";
        public string Currency = "";
        public long PreviousCO2EmissionOverall; // only for statistical informations relevant
        public List<int> ListOfMadeDecisions = new List<int>();
        public List<int> ListOfSeenDecisions = new List<int>();

        public short CurrentYear;
        public short CurrentRound;
        public long Money;
        public long Debt;
        public long InterestPerRound;
        public long TaxIncomePerRound;
        public short EnvironmentPollutionInPercent;
        public long StartCO2EmissionOverall;  // the sum of all segments at the beginning of the game
        
        // the segments
        public short SocietySatisfactionInPercent;
        public long SocietyCO2Emission;
        public short IndustrySatisfactionInPercent;
        public long IndustryCO2Emission;
        public short EnergySectorSatisfactionInPercent;
        public long EnergySectorCO2Emission;
        public short AgricultureSatisfactionInPercent;
        public long AgricultureCO2Emission;
    }
}
