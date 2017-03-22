using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ANBSysPro;
using System.IO;

namespace ANBSysProTest
{
    [TestClass]
    public class FileReaderUtilTest
    {
        [TestMethod]
        public void CalculateWordOccurencesTest()
        {
            try
            {
                string filePath = @"D:\VSProjects\ANBSysPro\ANBSysPro\bin\Debug\Sample.txt";

                long topNNumberofWords = 5;
                string[] wordsToSkip = new string[] { "is", "was", "the" };

                Assert.IsFalse(string.IsNullOrEmpty(filePath), "Please enter the filepath");
                Assert.IsFalse(topNNumberofWords < 1, "Please enter the top n words more than 1");
                Assert.IsFalse(!File.Exists(filePath), "Please enter the valida filepath");

                FileReaderUtil.CalculateWordOccurences(filePath, topNNumberofWords, wordsToSkip);
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
