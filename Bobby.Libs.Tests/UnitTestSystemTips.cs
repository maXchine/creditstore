namespace Bobby.Libs.Tests
{
    using Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class UnitTestSystemTips
    {
        [TestMethod]
        public void TestSystemTipsCtor()
        {
            var settingValue = AppSettings.GetSettings(Constants.ProfixOfConstant + "ErrorOption", "用户登录超时，请重新登录");
            Assert.AreEqual(SystemTips.ErrorOption, settingValue);
        }
    }
}
