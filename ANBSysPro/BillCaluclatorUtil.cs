using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANBSysPro
{
    public class ItemInfo
    {
        public string Name { get; set; }
        public decimal CostForOne { get; set; }
        public long Quantity { get; set; }

        private decimal totalCost = 0;
        public decimal TotalCost
        {
            get
            {
                totalCost = CostForOne * Quantity;
                return totalCost;
            }
            private set
            {
                totalCost = value;
            }
        }
    }

    public class PaymentInfo
    {
        private decimal total = 0;
        private decimal serviceTax = 0;
        private decimal vat = 0;
        public PaymentInfo(decimal total, decimal vat, decimal serviceTax)
        {
            this.total = total;
            this.vat = vat;
            this.serviceTax = serviceTax;
        }

        private decimal _vat = 0;
        public decimal VAT
        {
            get
            {
                _vat = this.total * (this.vat / 100);
                return _vat;
            }
            private set
            {
                _vat = value;
            }
        }

        private decimal _serviceTax = 0;
        public decimal ServiceTax
        {
            get
            {
                _serviceTax = this.total * (this.serviceTax / 100);
                return _serviceTax;
            }
            private set
            {
                _serviceTax = value;
            }
        }

        private decimal _netTotal = 0;
        public decimal NetTotal
        {
            get
            {
                _netTotal = this.total + VAT + ServiceTax;
                return _netTotal;
            }
            private set
            {
                _netTotal = value;
            }
        }
    }

    public class BillCaluclatorUtil
    {
        public decimal _vat = 0;
        public decimal _serviceTax = 0;
        public BillCaluclatorUtil(decimal vat, decimal serviceTax)
        {
            _vat = vat;
            _serviceTax = serviceTax;
        }

        public PaymentInfo CaluclateTotalWithVATAndServiceTax(List<ItemInfo> itemsList)
        {
            decimal total = 0;

            try
            {
                for (int i = 0; i < itemsList.Count; i++)
                {
                    total += itemsList[i].TotalCost;
                }

                PaymentInfo retVal = new PaymentInfo(total, _vat, _serviceTax);
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static ItemInfo GetItemInfo()
        {
            Console.WriteLine("----------------------------------------------------");
            ItemInfo retVal = new ItemInfo();
            Console.Write("Item Name: ");
            retVal.Name = Console.ReadLine();
            Console.Write("Cost of 1: ");
            retVal.CostForOne = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Quantity: ");
            retVal.Quantity = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("----------------------------------------------------");
            return retVal;
        }

        public static void PrintPaymentInfo(List<ItemInfo> itemInfos, PaymentInfo paymentInfo)
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Item" + "\t" + "Quantity" + "\t" + "Amount" + "\t" + "Cost");
            Console.WriteLine("----------------------------------------------------");

            foreach (ItemInfo itemInfo in itemInfos)
            {
                Console.WriteLine(itemInfo.Name + "\t" + itemInfo.Quantity + "\t" + "\t" + itemInfo.CostForOne + "\t" + itemInfo.TotalCost);
            }
            Console.WriteLine();
            Console.WriteLine("12.5 VAT" + "\t" + "\t" + "\t" + paymentInfo.VAT);
            Console.WriteLine("5% Service Tax" + "\t" + "\t" + "\t" + paymentInfo.ServiceTax);
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Net Total" + "\t" + "\t" + "\t" + paymentInfo.NetTotal);
            Console.WriteLine("----------------------------------------------------");
        }
    }
}
