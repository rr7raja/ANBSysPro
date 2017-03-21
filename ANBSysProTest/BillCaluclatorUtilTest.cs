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
