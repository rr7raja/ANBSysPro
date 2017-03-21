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
