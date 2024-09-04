using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TestPathDesigner.ConnectionStatusLibrary;
using TestPathDesigner.ConnectionStatusLibrary.Enums;

namespace TestPathDesigner.UnitTest.ConnectionStatusTest
{
    [TestClass]
    public class ConnectionStatusColorTest
    {
        ConnectionStatusToColorConverter converter = new ConnectionStatusToColorConverter();
        [TestMethod]
        public void ConnectionStatusConnectedReturnBlue()
        {
            Assert.AreEqual(converter.Convert(ConnectionStatusEnum.Connected, null, null, null), Brushes.Blue) ;
        }
        [TestMethod]
        public void ConnectionStatusDisconnectedReturnBlue()
        {
            Assert.AreEqual(converter.Convert(ConnectionStatusEnum.Disconnected, null, null, null), Brushes.Red);
        }
    }
}
