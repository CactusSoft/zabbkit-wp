namespace CactusSoft.Stierlitz.Application.Helpers
{
    public class PluralFormHelper
    {
        public static string GetPluralForm(int number, string firstForm, string secondForm, string thirdForm)
        {
            if (number % 10 == 1 && number % 100 != 11)
                return firstForm;

            if (number % 10 >= 2 && number % 10 <= 4 && (number % 100 < 10 || number % 100 >= 20))
                return secondForm;

            return thirdForm;
        }
    }
}
