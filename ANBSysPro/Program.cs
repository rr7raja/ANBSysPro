using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ANBSysPro
{
    class Program
    {
        static void Main(string[] args)
        {
            string readKey = string.Empty;
            if (args.Length > 0)
            {
                readKey = args[0];
            }

            if (string.IsNullOrEmpty(readKey))
            {
                Console.WriteLine("1.Calculate the frequency of the word occurences");
                Console.WriteLine("2.Calculate the Bill");
                Console.WriteLine("Enter 1 or 2 to proceed");
                readKey = Console.ReadLine();
            }

            if (readKey.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                CalculateWordOccurences();
            }
            else if (readKey.Equals("2", StringComparison.OrdinalIgnoreCase))
            {
                CaluclateBill();
            }
            else
            {
                Console.WriteLine("Please restart the application to proceed");
            }
            Console.ReadLine();
        }

        private static void CalculateWordOccurences()
        {
            Console.WriteLine("Sample text file is added to executing path. You can modify the text file and calculate the occurences.");

            string SamplePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sample.txt");

            Console.Write("Enter the number of words to List: ");
            long topNNumberofWords = Convert.ToInt64(Console.ReadLine());

            Console.Write("Enter the 'n' words with semicolon to skip: ");
            string[] wordsToSkip = Console.ReadLine().Split(';');

            FileReaderUtil.CalculateWordOccurences(SamplePath, topNNumberofWords, wordsToSkip);
        }

        private static void CaluclateBill()
        {
            Console.Write("Enter Total Items Count: ");
            int itemsCount = Convert.ToInt32(Console.ReadLine());

            List<ItemInfo> itemsList = new List<ItemInfo>();

            for (int i = 0; i < itemsCount; i++)
            {
                itemsList.Add(BillCaluclatorUtil.GetItemInfo());
            }

            BillCaluclatorUtil billCalculatorUtil = new BillCaluclatorUtil(Convert.ToDecimal(12.5), Convert.ToDecimal(5));

            PaymentInfo paymentInfo = billCalculatorUtil.CaluclateTotalWithVATAndServiceTax(itemsList);

            BillCaluclatorUtil.PrintPaymentInfo(itemsList, paymentInfo);
        }
    }
}
