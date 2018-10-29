using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ripple.WebSocketClient.Exceptions;
using Ripple.WebSocketClient.Model.Account;
using Ripple.WebSocketClient.Model.Admin;
using Ripple.WebSocketClient.Model.Ledger;
using Ripple.WebSocketClient.Model.Server;
using Ripple.WebSocketClient.Model.Subscription;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;
using Ripple.WebSocketClient.Requests;
using Ripple.WebSocketClient.Requests.Account;
using Ripple.WebSocketClient.Requests.Admin;
using Ripple.WebSocketClient.Requests.Ledger;
using Ripple.WebSocketClient.Requests.Subscription;
using Ripple.WebSocketClient.Requests.Transaction;
using Ripple.WebSocketClient.Responses;
using Ripple.WebSocketClient.Responses.Transaction;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.TransactionTypes;
using BookOffers = Ripple.WebSocketClient.Model.Transaction.BookOffers;
using ChannelAuthorize = Ripple.WebSocketClient.Model.Transaction.ChannelAuthorize;
using ChannelVerify = Ripple.WebSocketClient.Model.Transaction.ChannelVerify;
using Submit = Ripple.WebSocketClient.Model.Transaction.Submit;

namespace Ripple.WebSocketClient
{
    public interface IRippleClient
    {
        Task Connect();

        void Disconnect();

        Task Ping();

        Task<SubscriptionStream> Subscribe(SubscriptionRequest request);
        Task Unsubscribe();

        Task<AccountCurrencies> AccountCurrencies(string account);

        Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request);

        Task<AccountChannels> AccountChannels(string account);

        Task<AccountChannels> AccountChannels(AccountChannelsRequest request);

        Task<AccountInfo> AccountInfo(string account);

        Task<AccountInfo> AccountInfo(AccountInfoRequest request);

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="account">The account number to query.</param>
        /// <returns>An <see cref="Model.Account.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(string account);

        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An <see cref="Model.Account.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(AccountLinesRequest request);

        Task<AccountOffers> AccountOffers(string account);

        Task<AccountOffers> AccountOffers(AccountOffersRequest request);

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>An <see cref="Model.Account.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(string account);

        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Model.Account.AccountLines"/> instead.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An <see cref="Model.Account.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(AccountObjectsRequest request);

        Task<AccountTransactions> AccountTransactions(string account);

        Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request);

        Task<NoRippleCheck> NoRippleCheck(string account);

        Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request);

        Task<GatewayBalances> GatewayBalances(string account);

        Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request);

        Task<ITransactionResponseCommon> Transaction(string transaction);

        Task<IBaseTransactionResponse> TransactionAsBinary(string transaction);

        Task<ServerInfo> ServerInfo();

        Task<Fee> Fees();

        Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request);

        Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request);

        Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request);

        Task<Submit> SubmitTransaction(SubmitRequest request);

        Task<BookOffers> BookOffers(BookOffersRequest request);

        Task<Ledger> Ledger(LedgerRequest request);

        Task<BaseLedgerInfo> ClosedLedger();

        Task<LedgerCurrentIndex> CurrentLedger();

        Task<LedgerData> LedgerData(LedgerDataRequest request);

        Task<WalletPropose> WalletPropose(WalletProposeRequest request);
    }

    public class RippleClient : IRippleClient
    {
        private readonly WebSocketClient client;
        private readonly ConcurrentDictionary<Guid, TaskInfo> tasks;
        private readonly JsonSerializerSettings serializerSettings;

        private SubscriptionStream subscriptionStream;

        public RippleClient(string url)
        {
            tasks = new ConcurrentDictionary<Guid, TaskInfo>();
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            client = WebSocketClient.Create(url);
            client.OnMessageReceived(MessageReceived);
            client.OnConnectionError(Error);
        }

        public async Task Connect() => await client.Connect();

        public void Disconnect()
        {
            client.Disconnect();
        }

        public Task Ping()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "ping";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(object);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountCurrencies> AccountCurrencies(string account)
        {
            AccountCurrenciesRequest request = new AccountCurrenciesRequest(account);
            return AccountCurrencies(request);
        }

        public Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountCurrencies> task = new TaskCompletionSource<AccountCurrencies>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountCurrencies);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountChannels> AccountChannels(string account)
        {
            AccountChannelsRequest request = new AccountChannelsRequest(account);
            return AccountChannels(request);
        }

        public Task<AccountChannels> AccountChannels(Requests.Account.AccountChannelsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountChannels> task = new TaskCompletionSource<AccountChannels>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountChannels);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountInfo> AccountInfo(string account)
        {
            AccountInfoRequest request = new AccountInfoRequest(account);
            return AccountInfo(request);
        }

        public Task<AccountInfo> AccountInfo(AccountInfoRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountInfo> task = new TaskCompletionSource<AccountInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountInfo);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountLines> AccountLines(string account)
        {
            AccountLinesRequest request = new AccountLinesRequest(account);
            return AccountLines(request);
        }

        public Task<AccountLines> AccountLines(AccountLinesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountLines> task = new TaskCompletionSource<AccountLines>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountLines);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountOffers> AccountOffers(string account)
        {
            AccountOffersRequest request = new AccountOffersRequest(account);
            return AccountOffers(request);
        }

        public Task<AccountOffers> AccountOffers(AccountOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountOffers> task = new TaskCompletionSource<AccountOffers>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountOffers);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountObjects> AccountObjects(string account)
        {
            AccountObjectsRequest request = new AccountObjectsRequest(account);
            return AccountObjects(request);
        }

        public Task<AccountObjects> AccountObjects(AccountObjectsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountObjects> task = new TaskCompletionSource<AccountObjects>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountObjects);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<AccountTransactions> AccountTransactions(string account)
        {
            AccountTransactionsRequest request = new AccountTransactionsRequest(account);
            return AccountTransactions(request);
        }

        public Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<AccountTransactions> task = new TaskCompletionSource<AccountTransactions>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(AccountTransactions);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<NoRippleCheck> NoRippleCheck(string account)
        {
            NoRippleCheckRequest request = new NoRippleCheckRequest(account);
            return NoRippleCheck(request);
        }

        public Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<NoRippleCheck> task = new TaskCompletionSource<NoRippleCheck>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(NoRippleCheck);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<GatewayBalances> GatewayBalances(string account)
        {
            GatewayBalancesRequest request = new GatewayBalancesRequest(account);
            return GatewayBalances(request);
        }

        public Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<GatewayBalances> task = new TaskCompletionSource<GatewayBalances>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(GatewayBalances);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<ITransactionResponseCommon> Transaction(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ITransactionResponseCommon> task = new TaskCompletionSource<ITransactionResponseCommon>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(TransactionResponseCommon);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<IBaseTransactionResponse> TransactionAsBinary(string transaction)
        {
            TransactionRequest request = new TransactionRequest(transaction);
            request.Binary = true;
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<IBaseTransactionResponse> task = new TaskCompletionSource<IBaseTransactionResponse>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BinaryTransactionResponse);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;

        }

        public Task<ServerInfo> ServerInfo()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "server_info";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ServerInfo> task = new TaskCompletionSource<ServerInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ServerInfo);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<Fee> Fees()
        {
            RippleRequest request = new RippleRequest();
            request.Command = "fee";

            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Fee> task = new TaskCompletionSource<Fee>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Fee);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<ChannelAuthorize> ChannelAuthorize(ChannelAuthorizeRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ChannelAuthorize> task = new TaskCompletionSource<ChannelAuthorize>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ChannelAuthorize);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<ChannelVerify> ChannelVerify(ChannelVerifyRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<ChannelVerify> task = new TaskCompletionSource<ChannelVerify>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(ChannelVerify);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<Submit> SubmitTransactionBlob(SubmitBlobRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Submit);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<Submit> SubmitTransaction(SubmitRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Submit> task = new TaskCompletionSource<Submit>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Submit);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<BookOffers> BookOffers(BookOffersRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<BookOffers> task = new TaskCompletionSource<BookOffers>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BookOffers);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<Ledger> Ledger(LedgerRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<Ledger> task = new TaskCompletionSource<Ledger>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(Ledger);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<BaseLedgerInfo> ClosedLedger()
        {
            ClosedLedgerRequest request = new ClosedLedgerRequest();
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<BaseLedgerInfo> task = new TaskCompletionSource<BaseLedgerInfo>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(BaseLedgerInfo);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;

        }

        public Task<LedgerCurrentIndex> CurrentLedger()
        {
            CurrentLedgerRequest request = new CurrentLedgerRequest();
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<LedgerCurrentIndex> task = new TaskCompletionSource<LedgerCurrentIndex>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(LedgerCurrentIndex);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<LedgerData> LedgerData(LedgerDataRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<LedgerData> task = new TaskCompletionSource<LedgerData>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(LedgerData);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<WalletPropose> WalletPropose(WalletProposeRequest request)
        {
            var command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<WalletPropose> task = new TaskCompletionSource<WalletPropose>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(WalletPropose);

            tasks.TryAdd(request.Id, taskInfo);

            this.SendMessage(command);
            return task.Task;
        }

        public Task<SubscriptionStream> Subscribe(SubscriptionRequest request)
        {
            if (this.subscriptionStream != null)
                throw new Exception("One subscription at a time");

            string command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<SubscriptionStream> task = new TaskCompletionSource<SubscriptionStream>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(SubscriptionStream);

            tasks.TryAdd(request.Id, taskInfo);

            task.SetResult(new SubscriptionStream(request));

            this.SendMessage(command);
            return task.Task;
        }

        public Task Unsubscribe()
        {
            if (this.subscriptionStream == null)
                throw new Exception("No subscription");

            SubscriptionRequest request = this.subscriptionStream.Request;
            request.Command = "unsubscribe";

            string command = JsonConvert.SerializeObject(request, serializerSettings);
            TaskCompletionSource<object> task = new TaskCompletionSource<object>();

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.TaskId = request.Id;
            taskInfo.TaskCompletionResult = task;
            taskInfo.Type = typeof(object);
            taskInfo.IsUnsubscribe = true;

            tasks.TryAdd(request.Id, taskInfo);

            client.SendMessage(command);
            return task.Task;
        }

        private void SendMessage(string command)
        {
            client.SendMessage(command);
        }

        private void Error(Exception ex, WebSocketClient client)
        {
            throw new Exception(ex.Message, ex);
        }

        private Dictionary<string, object> ConvertJObjectToDictionary(JObject obj)
        {
            Dictionary<string, object> result = obj.ToObject<Dictionary<string, object>>();

            string[] keys = result.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];

                JToken jVal = result[key] as JToken;

                if (jVal == null)
                    continue;

                if (jVal is JArray)
                {
                    List<JObject> list = (jVal as JArray).ToObject<List<JObject>>();
                    List<Dictionary<string, object>> newList = new List<Dictionary<string, object>>();

                    for (int x = 0; x < list.Count; x++)
                    {
                        newList.Add(this.ConvertJObjectToDictionary(list[x]));
                    }

                    result[key] = newList;
                } else if (jVal is JObject)
                {
                    result[key] = this.ConvertJObjectToDictionary(jVal as JObject);
                }
            }

            return result;
        }

        private void MessageReceived(string s, WebSocketClient client)
        {
            JObject jResponse = JObject.Parse(s);

            // if the response does not have an id field, it is not a RippleResponse.
            // it is most likely a stream from the subscribe API
            if (jResponse["id"] == null)
            {
                if (!jResponse.HasValues)
                    return;

                // if there is no active subscriptionStream
                // disregard the message
                if (this.subscriptionStream == null)
                    return;

                this.subscriptionStream.Push(this.ConvertJObjectToDictionary(jResponse));

                return;   
            }

            RippleResponse response = jResponse.ToObject<RippleResponse>();

            var taskInfoResult = tasks.TryGetValue(response.Id, out var taskInfo);
            if (taskInfoResult == false)
                throw new Exception("Task not found");

            if (response.Status == "success")
            {
                if (taskInfo.Type != typeof(SubscriptionStream))
                {
                    if (taskInfo.IsUnsubscribe)
                    {
                        this.subscriptionStream = null;
                    }

                    object deserialized = JsonConvert.DeserializeObject(response.Result.ToString(), taskInfo.Type, serializerSettings);

                    MethodInfo setResult = taskInfo.TaskCompletionResult.GetType().GetMethod("SetResult");
                    setResult.Invoke(taskInfo.TaskCompletionResult, new[] { deserialized });
                } else
                {
                    TaskCompletionSource<SubscriptionStream> taskCompletionSource = taskInfo.TaskCompletionResult as TaskCompletionSource<SubscriptionStream>;
                    this.subscriptionStream = taskCompletionSource.Task.Result;

                    // since subscriptionStream is not null, MessageReceived will push the first subscription to the SubscriptionStream
                    this.MessageReceived(response.Result.ToString(), client);
                }

                if (taskInfo.RemoveUponCompletion)
                {
                    tasks.TryRemove(response.Id, out taskInfo);
                }
            }
            else if (response.Status == "error")
            {
                var setException = taskInfo.TaskCompletionResult.GetType().GetMethod("SetException", new Type[] { typeof(Exception) }, null);

                RippleException exception = new RippleException(response.Error);
                setException.Invoke(taskInfo.TaskCompletionResult, new[] { exception });

                tasks.TryRemove(response.Id, out taskInfo);
            }
        }

       
    }
}
