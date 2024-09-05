namespace Services.Test
{
    [TestClass]
    public class UnitTest1
    {
        ConnectionService service = new ConnectionService();
        [TestMethod]
        public void ConnectionRequestWillReturnFalse()
        {
            Setup();
            Assert.IsFalse(service.IsConnected("", $@"http://127.0.0.1:9999"));
        }
        [TestMethod]
        public void ConnectionRequesWillReturnTrue()
        {
            Setup();
            Assert.IsTrue(service.IsConnected("", $@"http://127.0.0.1:80"));
        }
        private void Setup()
        {
            service = new ConnectionService();
        }
    }
}