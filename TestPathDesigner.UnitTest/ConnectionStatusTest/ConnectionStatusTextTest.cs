using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestPathDesigner.ConnectionStatusLibrary;
using TestPathDesigner.ConnectionStatusLibrary.Enums;

namespace TestPathDesigner.UnitTest.ConnectionStatusTest
{
    [TestClass]
    public class ConnectionStatusTextTest
    {
        IValueConverter textConverter;
        [TestMethod]
        public void ConnectionStatusConnectedShouldReturnConnectedText()
        {
            Setup();
            Assert.AreEqual(textConverter.Convert(ConnectionStatusEnum.Connected, null, null, null),"Connected");
        }
        [TestMethod]
        public void ConnectionStatusDisconnectedShouldReturnDisconnectedText()
        {
            Setup();
            Assert.AreEqual(textConverter.Convert(ConnectionStatusEnum.Connected, null, null, null), "Connected");
        }
        private void Setup()
        {
            textConverter = new ConnectionStatusToTextConverter();
        }
    }
}
