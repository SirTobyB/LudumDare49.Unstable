using System;

namespace Assets.Scripts
{
    public static class LongExtension
    {
        public static string ToStringMoney(this long money, string currency, string language)
        {
            string result;
            double moneyFormatted = Convert.ToDouble(money);

            if (moneyFormatted < 1000000000d)  // less than one billion?
            {
                moneyFormatted /= 1000000d;
                result = moneyFormatted.ToString("F2") + " Mio.";
            }
            else  //  more than one billion?
            {
                moneyFormatted /= 1000000000d;

                if (language == "en")
                    result = moneyFormatted.ToString("F2") + " Bn.";
                else
                    result = moneyFormatted.ToString("F2") + " Mil.";
            }

            if (!string.IsNullOrEmpty(currency))
                result += " " + currency;

            return result;
        }

        public static string ToStringCO2(this long emission, bool withCO2)
        {
            string result;
            double emissionFormatted = Convert.ToDouble(emission);
            
            /*
            if (emissionFormatted < 1000000d)  // less than one million?
            {
                result = emissionFormatted.ToString();
            }
            else */
            if (emissionFormatted < 1000000000d)  // less than one billion?
            {
                emissionFormatted /= 1000000d;
                result = emissionFormatted.ToString("F2") + " Mio.";
            }
            else  //  more than one billion?
            {
                emissionFormatted /= 1000000000d;
                result = emissionFormatted.ToString("F2") + " Bn.";
            }

            result += " t";

            if (withCO2)
                result += " CO₂";

            return result;
        }
    }
}
