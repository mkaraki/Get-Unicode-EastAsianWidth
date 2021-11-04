using Microsoft.VisualStudio.TestTools.UnitTesting;
using Get_Unicode_EastAsianWidth;

namespace Get_Unicode_EastAsianWidth.Test
{
    [TestClass]
    public class TestMain
    {
        [TestMethod]
        public void Check_IsFullWidth_1()
        {
            char test_tgt = '��';
            bool res = EAWCheck.IsFullWidth(test_tgt);

            Assert.AreEqual(true,res);
        }

        [TestMethod]
        public void Check_IsFullWidth_2()
        {
            char test_tgt = 'g';
            bool res = EAWCheck.IsFullWidth(test_tgt);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Check_IsHalfWidth_1()
        {
            char test_tgt = '��';
            bool res = EAWCheck.IsHalfWidth(test_tgt);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void Check_IsHalfWidth_2()
        {
            char test_tgt = 'g';
            bool res = EAWCheck.IsHalfWidth(test_tgt);

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void Check_LenB_1()
        {
            string test_tgt = "����́A\"Test\"�p�̃e�L�X�g�ł��B"; // (13 * 2) + 4 = 32 
            int res = EAWCheck.LenB(test_tgt);

            Assert.AreEqual(32, res);
        }
    }
}
