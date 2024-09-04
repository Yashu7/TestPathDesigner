using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TestPathDesigner.ConnectionStatusLibrary.Converters;
using TestPathDesigner.ConnectionStatusLibrary.Enums;

namespace TestPathDesigner.UnitTest.ConnectionStatusTest
{
    [TestClass]
    public class ConnectionStatusColorTest
    {
        [TestMethod]
        public void ConnectionStatusConnectedReturnBlue()
        {
            ConnectionStatusEnum connectionStatus = ConnectionStatusEnum.Connected;
            ConnectionStatusToColorConverter converter = new ConnectionStatusToColorConverter();
            var color = converter.Convert(connectionStatus, null,null,null);
            Assert.AreEqual(color, Brushes.Blue);
        }
    }
}
