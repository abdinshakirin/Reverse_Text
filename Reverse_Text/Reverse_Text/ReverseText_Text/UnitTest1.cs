using System;
using System.Drawing.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reverse_Text.Controllers;
using Reverse_Text.Data;

namespace ReverseText_Test
{
    [TestClass]
    public class UnitTest1
    {

        private const string Expected = "54321";

        [TestMethod]
        public void TestGetReverseText_ValidInput()
        {
          

            var inputText = "12345";

            char[] charArray = inputText.ToCharArray();
            Array.Reverse(charArray);

            // Act
            var result = new string(charArray);

            // Assert
            Assert.AreEqual(Expected, result);

        }

      
    }
}