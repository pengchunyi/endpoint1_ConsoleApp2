//////這是原稿==========================================================
////using System.Threading;
////using CFX;
////using CFX.Transport;
////using System;
////using System.Threading.Tasks;
////using CFX.Production;
////using CFX.ResourcePerformance;
////using CFX.Production.TestAndInspection;
////using CFX.InformationSystem.UnitValidation;


//////這是line manager
////namespace machine1
////{
////    class Program
////    {
////        static void Main(string[] args)
////        {

////            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
////            string handle = "endpoint1";
////            endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666"));
////            endpoint.AddListener("event");
////            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;


////            endpoint.AddMessageSource("MessageSource");
////            endpoint.PublishToMessageSource("MessageSource", new CFXEnvelope(new AreYouThereRequest()));

////            var resp = endpoint.ExecuteRequest(
////				//"amqp://10.181.56.175:30031",
////				"amqp://127.0.0.1:8888",
////				new CFXEnvelope(new AreYouThereRequest())
////                {
////                    Source = "endpoint1",
////                    Target = "endpoint2",
////                });
////            Console.WriteLine("endpoint1 ExecuteRequest and recive Response Msg: \n");
////            Console.WriteLine(resp.ToJson());
////            Console.ReadKey();
////        }


////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
////        {
////            //這就是
////            Console.WriteLine("endpoint1 CFXMessageReceivedFromListener" + "\n");
////            Console.WriteLine(message.ToJson());
////            Console.WriteLine("\n");

////        }

////    }
////}


//////已成功同步
//////以下是測試連上"http://10.146.212.146/eda/master/webui/node-mgmt/65806218893f4179d4d45ea1/overview/0"
//////账号 sie_admin
//////密码是passw0rd

//////using System.Threading;
//////using CFX;
//////using CFX.Transport;
//////using System;
//////using System.Threading.Tasks;
//////using CFX.Production;
//////using CFX.ResourcePerformance;
//////using CFX.Production.TestAndInspection;
//////using CFX.InformationSystem.UnitValidation;
//////using System.Security.Policy;


//////這是line manager
//////namespace machine1
//////{
//////    class Program
//////    {
//////        static void Main(string[] args)
//////        {



//////            string handle1 = "endpoint2";
//////            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
//////            endpoint.HeartbeatFrequency = TimeSpan.Zero;
//////            endpoint.Open(handle1, new Uri("amqp://127.0.0.1:6666"));
//////            Uri uri = new Uri("amqp://10.181.56.175:30031");

//////            string listenchannel = "event";
//////            endpoint.AddPublishChannel(uri, listenchannel);



//////            Task.Run(() =>
//////            {
//////                while (true)
//////                {
//////                    endpoint.Publish(new EndpointConnected());
//////                    Console.WriteLine("endpoint1 send\n");
//////                    Thread.Sleep(3000);
//////                }
//////            });



//////            Console.ReadKey();

//////        }
//////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//////        {
//////            Console.WriteLine("endpoint1 CFXMessageReceivedFromListener" + "\n");
//////            Console.WriteLine(message.ToJson());
//////            Console.WriteLine("\n");

//////        }
//////    }
//////}


//////newest_這個是可以發送請求並得到SIE平台的回覆
//////以下是測試連上"http://10.146.212.146/eda/master/webui/node-mgmt/65806218893f4179d4d45ea1/overview/0"
//////账号 sie_admin
//////密码是passw0rd
////using System;
////using System.Threading;
////using System.Threading.Tasks;
////using CFX;
////using CFX.Transport;
////using CFX.ResourcePerformance;

////namespace machine1
////{
////    class Program
////    {
////        static void Main(string[] args)
////        {
////            string handle1 = "CHUNYI_PC";
////            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
////            endpoint.HeartbeatFrequency = TimeSpan.Zero;
////            endpoint.Open(handle1, new Uri("amqp://127.0.0.1:8888"));
////            Uri uri = new Uri("amqp://10.181.56.175:30031");

////            //10.149.66.140:6666
////            string listenchannel = "event";
////            endpoint.AddPublishChannel(uri, listenchannel);

////            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;
////            //endpoint.Publish(new EnergyConsumptionRequest());


////            var resp = endpoint.ExecuteRequest("amqp://10.181.56.175:30031",

////            new CFXEnvelope(new EnergyConsumptionRequest())
////            {
////                Source = handle1,
////                Target = "Inline-control",
////            });
////            //Console.WriteLine("endpoint1 ExecuteRequest and recive Response Msg: \n");
////            Console.WriteLine(new CFXEnvelope(new EnergyConsumptionRequest()).ToJson() + "\n");
////            Console.WriteLine(resp.ToJson());


////            Console.ReadKey();

////        }

////        // 處理來自 Listener 的消息接收事件
////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
////        {
////            Console.WriteLine("收到來自 Listener 的消息：\n");
////            Console.WriteLine("目標地址：" + targetAddress);
////            Console.WriteLine("消息內容：\n" + message.ToJson());
////            Console.WriteLine("\n");
////        }
////    }
////}









////////測試二:似乎成功
//////static void Main(string[] args)
//////{
//////    string handle1 = "CRX.test.1";
//////    //Uri uri = new Uri(amqp://127.0.0.1:15673");
//////    AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
//////    endpoint.HeartbeatFrequency = TimeSpan.Zero;
//////    endpoint.Open(handle1, new Uri("amqp:///127.0.0.1:15673"));
//////    Uri uri = new Uri("amqp://10.147.70.153:6666");

//////    string listenchannel = "event";
//////    endpoint.AddPublishChannel(uri, listenchannel);



//////    Task.Run(() =>
//////    {
//////        while (true)
//////        {
//////            endpoint.Publish(new EndpointConnected());
//////            Console.WriteLine("endpoint1 send");
//////            // 建立並發送 EnergyConsumptionRequest 請求
//////            var requestEnvelope = new CFXEnvelope(new EnergyConsumptionRequest())
//////            {
//////                Source = "CRX.test.1",
//////                Target = "endpoint2" // 根據需求指定目標
//////            };
//////            // 使用 ExecuteRequest 發送請求並接收回應
//////            var resp = endpoint.ExecuteRequest("amqp://10.147.70.153:6666", requestEnvelope);

//////            // 顯示回應內容
//////            if (resp != null)
//////            {
//////                Console.WriteLine("收到回應：");
//////                Console.WriteLine(resp.ToJson());
//////            }
//////            else
//////            {
//////                Console.WriteLine("未收到回應。");
//////            }
//////            //Console.WriteLine(resp.ToJson());
//////            //endpoint.Publish(envelope);
//////            //Console.WriteLine(jsonString);
//////            Thread.Sleep(1000);
//////        }
//////    });

//////    Console.ReadKey();
//////}
//////        static void Main(string[] args)
//////        {
//////            string handle1 = "CRX.test.1";
//////            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint
//////            {
//////                HeartbeatFrequency = TimeSpan.Zero
//////            };

//////            // 開啟端點，指定 URI
//////            endpoint.Open(handle1, new Uri("amqp://127.0.0.1:6666"));

//////            //Uri uri = new Uri("amqp://10.147.70.153:6666");
//////            Uri uri = new Uri("amqp://10.181.56.175:30031");


//////            // 添加頻道並監聽回應
//////            string listenchannel = "event";
//////            endpoint.AddPublishChannel(uri, listenchannel);
//////            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;

//////            // 使用 Task 反覆發送請求並接收回應
//////            // Task to repeatedly send EnergyConsumptionRequest and receive response
//////            Task.Run(() =>
//////            {
//////                while (true)
//////                {
//////                    // Create and send an EnergyConsumptionRequest with ExecuteRequest
//////                    var envelope = new CFXEnvelope(new EnergyConsumptionRequest())
//////                    {
//////                        Source = "CRX.test.1",
//////                        Target = "endpoint2" // Specify target as needed
//////                    };

//////                    try
//////                    {
//////                        // 使用 ExecuteRequest 發送請求並等待回應
//////                        var resp = endpoint.ExecuteRequest("amqp://10.147.70.153:6666", envelope);

//////                        // 如果有回應，顯示其內容
//////                        if (resp != null)
//////                        {
//////                            Console.WriteLine("Received response:");
//////                            Console.WriteLine(resp.ToJson());
//////                        }
//////                        else
//////                        {
//////                            Console.WriteLine("No response received.");
//////                        }
//////                    }
//////                    catch (Exception ex)
//////                    {
//////                        Console.WriteLine("Error in sending request: " + ex.Message);
//////                    }

//////                    // 等待3秒後再發送下一個請求
//////                    Thread.Sleep(3000);
//////                }
//////            });






//////        }
//////        //這就是打印接收到的數據的
//////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//////        {

//////            Console.WriteLine("endpoint1 CFXMessageReceivedFromListener" + "\n");
//////            Console.WriteLine(message.ToJson());
//////            Console.WriteLine("\n");

//////        }

//////    }
//////}




//////using System;
//////using CFX;
//////using CFX.Transport;
//////using CFX.ResourcePerformance;

//////namespace machine1
//////{
//////    class Program
//////    {
//////        static void Main(string[] args)
//////        {
//////            // 初始化 AMQP 端點
//////            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
//////            string handle = "endpoint1";
//////            endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666")); // 本地端 AMQP URI
//////            endpoint.AddListener("event");


//////            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;

//////            // 创建一个包含EnergyConsumptionRequest的CFXEnvelope对象，并设置源和目标
//////            var consumptionRequestEnvelope = new CFXEnvelope(new EnergyConsumptionRequest())
//////            {
//////                Source = "endpoint1",
//////                Target = "endpoint2"
//////            };

//////            // 发送EnergyConsumptionRequest请求到指定地址，并获取响应
//////            var resp = endpoint.ExecuteRequest("amqp://127.0.0.1:8888", consumptionRequestEnvelope);
//////            Console.WriteLine("EnergyConsumptionRequest 已發送，等待回應...");


//////            // 如果收到回應，顯示其內容
//////            if (resp != null)
//////            {
//////                Console.WriteLine(resp.ToJson());
//////            }

//////            // 保持程序運行
//////            Console.ReadKey();
//////        }

//////        // 處理來自 Listener 的消息接收事件
//////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//////        {
//////            // 將整個消息物件序列化為 JSON 並輸出
//////            Console.WriteLine("收到來自 Listener 的消息：\n");
//////            Console.WriteLine(message.ToJson());
//////            Console.WriteLine("\n");
//////        }
//////    }
//////}




//////這邊是本機端的能源請求
////using System;
////using System.Threading;
////using CFX;
////using CFX.Transport;
////using CFX.ResourcePerformance;

////namespace machine1
////{
////    class Program
////    {
////        private static AmqpCFXEndpoint endpoint;
////        private static Timer requestTimer;

////        static void Main(string[] args)
////        {
////            // 初始化 AMQP 端點
////            endpoint = new AmqpCFXEndpoint();
////            string handle = "endpoint1";
////            endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666")); // 本地端 AMQP URI
////            endpoint.AddListener("event");

////            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;

////            // 每3秒執行一次 SendEnergyConsumptionRequest 方法
////            requestTimer = new Timer(SendEnergyConsumptionRequest, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));

////            Console.WriteLine("開始每3秒發送一次 EnergyConsumptionRequest...");
////            Console.ReadKey();
////        }

////        private static void SendEnergyConsumptionRequest(object state)
////        {
////            // 創建一個包含 EnergyConsumptionRequest 的 CFXEnvelope 對象，並設置源和目標
////            var consumptionRequestEnvelope = new CFXEnvelope(new EnergyConsumptionRequest())
////            {
////                Source = "endpoint1",
////                Target = "endpoint2"
////            };

////            // 發送 EnergyConsumptionRequest 請求到指定地址，並獲取響應
////            var resp = endpoint.ExecuteRequest("amqp://127.0.0.1:8888", consumptionRequestEnvelope);
////            Console.WriteLine("\nEnergyConsumptionRequest 已發送，等待回應...");

////            // 如果收到回應，顯示其內容
////            if (resp != null)
////            {
////                Console.WriteLine("收到的EnergyConsumptionResp:");
////                Console.WriteLine(resp.ToJson());
////            }
////            else
////            {
////                Console.WriteLine("未收到回應");
////            }
////        }

////        // 處理來自 Listener 的消息接收事件
////        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
////        {
////            Console.WriteLine("收到來自 Listener 的消息：\n");
////            Console.WriteLine(message.ToJson());
////            Console.WriteLine("\n");
////        }
////    }
////}







////publsih模式=====================================
//using System;
//using CFX;
//using CFX.Transport;
//using Newtonsoft.Json;

//namespace machine1
//{
//	// 定義 Endpoint1 的類
//	internal class Endpoint1
//	{
//		public static void StartEndpoint()
//		{
//			// 創建並初始化 AMQP 端點
//			AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
//			string handle = "endpoint1";
//			endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666")); // 開啟端點並綁定 AMQP 地址
//			endpoint.AddListener("event"); // 添加 Listener 地址

//			// 註冊消息處理事件
//			endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;
//			Console.WriteLine("Endpoint1 已啟動，正在監聽消息...");
//		}

//		// 處理來自 Listener 的消息
//		private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//		{
//			Console.WriteLine("Endpoint1 收到來自 Listener 的消息：\n");
//			Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented)); // 打印消息內容
//			Console.WriteLine("\n");
//		}
//	}

//	// 主程序入口
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			Endpoint1.StartEndpoint(); // 啟動 Endpoint1
//			Console.ReadKey(); // 等待用戶按鍵退出
//		}
//	}

//}
