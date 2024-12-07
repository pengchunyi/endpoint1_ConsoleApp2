
////20241123_能源請求RR模式 =====================================
//using System;
//using System.Threading;
//using CFX;
//using CFX.Transport;
//using CFX.ResourcePerformance;
//using Newtonsoft.Json;

//namespace machine1
//{
//	class Program
//	{
//		private static AmqpCFXEndpoint endpoint;
//		private static Timer requestTimer;

//		static void Main(string[] args)
//		{
//			// 初始化 AMQP 端點
//			endpoint = new AmqpCFXEndpoint();
//			string handle = "endpoint1";
//			endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666")); // 本地端 AMQP URI
//			endpoint.AddListener("event");

//			endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;

//			// 每3秒執行一次 SendEnergyConsumptionRequest 方法
//			requestTimer = new Timer(SendEnergyConsumptionRequest, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));

//			Console.WriteLine("開始每3秒發送一次 EnergyConsumptionRequest...");
//			Console.ReadKey();
//		}

//		private static void SendEnergyConsumptionRequest(object state)
//		{
//			// 創建一個包含 EnergyConsumptionRequest 的 CFXEnvelope 對象，並設置源和目標
//			var consumptionRequestEnvelope = new CFXEnvelope(new EnergyConsumptionRequest())
//			{
//				Source = "endpoint1",
//				Target = "CHUNYI_PC"
//			};

//			// 發送 EnergyConsumptionRequest 請求到指定地址，並獲取響應
//			var resp = endpoint.ExecuteRequest("amqp://127.0.0.1:8888", consumptionRequestEnvelope);
//			Console.WriteLine("\nEnergyConsumptionRequest 已發送，等待回應...");

//			// 如果收到回應，顯示其內容
//			if (resp != null)
//			{
//				Console.WriteLine("收到的EnergyConsumptionResp:");
//				Console.WriteLine(JsonConvert.SerializeObject(resp, Formatting.Indented));
//			}
//			else
//			{
//				Console.WriteLine("未收到回應");
//			}
//		}

//		//private static void SendEnergyConsumptionRequest(object state)
//		//{
//		//	var requestID = Guid.NewGuid().ToString(); // 唯一的請求 ID

//		//	// 創建包含 RequestID 的請求消息
//		//	var consumptionRequestEnvelope = new CFXEnvelope(new EnergyConsumptionRequest())
//		//	{
//		//		Source = "endpoint1",
//		//		Target = "CHUNYI_PC",
//		//		RequestID = requestID
//		//	};

//		//	Console.WriteLine($"\n發送 EnergyConsumptionRequest，RequestID: {requestID}");
//		//	endpoint.Publish(consumptionRequestEnvelope); // 發送請求

//		//	Console.WriteLine("等待回應，所有回應將直接打印...");
//		//}


//		//處理來自 Listener 的消息接收事件
//		private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//		{
//			Console.WriteLine("收到來自 Listener 的消息：\n");
//			Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
//			Console.WriteLine("\n");
//		}

//	}
//}