using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversii_ASC
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Introduceti numarul in baza initiala:");
            string number = Console.ReadLine();
            Console.WriteLine("Introduceti baza initiala:");
            int base1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti baza finala:");
            int base2 = int.Parse(Console.ReadLine());
            string[] parts = number.Split('.');
            string integerPart = parts[0];
            string fractionalPart;
            if (parts.Length > 1)
                fractionalPart = parts[1];
            else
                fractionalPart = "";
            double decimalInteger = ConvertIntegerToDecimal(integerPart, base1);
            double decimalFractional = ConvertFractionToDecimal(fractionalPart, base1);
            double decimalNumber = decimalInteger + decimalFractional;
            string convertedNumber = ConvertFromDecimal(decimalNumber, base2);
            Console.WriteLine($"Numarul in baza {base2}: {convertedNumber}");
        }
        static double ConvertIntegerToDecimal(string integer_part, int b)
        {
            double nr = 0;
            int prod = 1;
            for(int i = integer_part.Length - 1; i>=0 ;i--)
            {
                char digit = integer_part[i];
                int value;
                if (digit >= '0' && digit <= '9')
                    value = digit - '0';
                else
                    value = digit - 'A' + 10;
                nr += value * prod;
                prod *= b;
            }
            return nr;

        }
        static double ConvertFractionToDecimal(string fractional_part, int base1)
        {
            double nr = 0;
            double prod = base1;
            for(int i=0;i<fractional_part.Length;i++)
            {
                char digit = fractional_part[i];
                int value;
                if (digit >= '0' && digit <= '9')
                    value = digit - '0';
                else
                    value = digit - 'A' + 10;
                nr += (double)value / prod;
                prod *= base1;
            }
            return nr;
        }
        static string ConvertFromDecimal(double decimal_number, int base2)
        {
            int integerPart = (int)decimal_number;
            string integerResult = "";
            while (integerPart > 0)
            {
                int remainder = integerPart % base2;
                char digit;
                if (remainder < 10)
                    digit = (char)(remainder + '0');
                else
                    digit = (char)(remainder - 10 + 'A');
                integerResult = digit + integerResult;
                integerPart /= base2;
            }
            if (integerResult == "")
                integerResult = "0";
            double fractionalPart = decimal_number - (int)decimal_number;
            string fractionalResult = "";
            for (int i = 0; i < 10 && fractionalPart > 1e-10; i++)
            {
                fractionalPart *= base2;
                int digit = (int)fractionalPart;
                fractionalResult += digit < 10 ? (char)(digit + '0') : (char)(digit - 10 + 'A');
                fractionalPart -= digit;
            }
            string final_result;
            if (fractionalResult != "")
                final_result = $"{integerResult}.{fractionalResult}";
            else
                final_result = $"{integerResult}";
            return final_result ;
        }   
        
    }
}
