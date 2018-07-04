using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Responses.Transaction;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.TransactionTypes;
using Ripple.WebSocketClient.Tests.Properties;

namespace Ripple.WebSocketClient.Tests
{
    [TestClass]
    public class DeserializationTests
    {

        [TestMethod]
        public void CanDeserializeTransaction()
        {
            var transaction = Encoding.ASCII.GetString(Resources.Transaction);
            ITransactionResponseCommon responseCommon = JsonConvert.DeserializeObject<TransactionResponseCommon>(transaction);            
            Assert.IsNotNull(responseCommon);
        }

        [TestMethod]
        public void CanDeserializeBinaryTransaction()
        {
            var transaction = Encoding.ASCII.GetString(Resources.TransactionBinary);
            BinaryTransactionResponse binaryTransaction =
                JsonConvert.DeserializeObject<BinaryTransactionResponse>(transaction);
            Assert.IsNotNull(binaryTransaction);
            Assert.IsNotNull(binaryTransaction.Meta);
            Assert.IsNotNull(binaryTransaction.Transaction);
        }
    }
}
