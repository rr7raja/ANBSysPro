using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ANBSysPro;
using System.Collections.Generic;

namespace ANBSysProTest
{
    [TestClass]
    public class BillCaluclatorUtilTest
    {
        [TestMethod]
        public void CaluclateTotalWithVATAndServiceTaxTest()
        {
            try
            {
                BillCaluclatorUtil billCalculatorUtil = new BillCaluclatorUtil(Convert.ToDecimal(12.5), Convert.ToDecimal(5));

                Assert.IsFalse(billCalculatorUtil._vat <= 0, "Please enter the vat greater than zero");
                Assert.IsFalse(billCalculatorUtil._serviceTax <= 0, "Please enter the service tax greater than zero");

                int itemsCount = 5;

                List<ItemInfo> items = new List<ItemInfo>();

                for (int i = 1; i <= itemsCount; i++)
                {
                    items.Add(new ItemInfo()
                    {
                        Name = "item " + i,
                        Quantity = i + 1,
                        CostForOne = i * 1
                    });
                }

                foreach (var item in items)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(item.Name), "Please enter the item name");
                    Assert.IsFalse(item.CostForOne < 1, "Please enter the cost more than zero");
                    Assert.IsFalse(item.Quantity < 1, "Please enter the quantity 1 or more.");
                }

                PaymentInfo paymentInfo = billCalculatorUtil.CaluclateTotalWithVATAndServiceTax(items);

                if (paymentInfo == null)
                    throw new ArgumentOutOfRangeException("payment is null");
                else
                    Assert.IsTrue(true, "Bill Calculated");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
