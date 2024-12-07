////20241123_publsih模式_測試=====================================
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



using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CFX;
using CFX.ResourcePerformance;
using CFX.Structures;
using CFX.Transport;
using Newtonsoft.Json;

namespace machine1
{
	// 定義 Endpoint1 的類
	internal class Endpoint1
	{
		private static AmqpCFXEndpoint endpoint;

		public static void StartEndpoint()
		{
			// 創建並初始化 AMQP 端點
			endpoint = new AmqpCFXEndpoint();
			string handle = "endpoint1";
			endpoint.Open(handle, new Uri("amqp://127.0.0.1:6666")); // 開啟端點並綁定 AMQP 地址
			endpoint.AddListener("event"); // 添加 Listener 地址

			// 註冊消息處理事件
			endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;
			Console.WriteLine("Endpoint1 已啟動，正在監聽消息...");
		}



		// 處理來自 Listener 的消息
		private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
		{
			Console.WriteLine("Endpoint1 收到來自 Listener 的消息：\n");
			Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented)); // 打印消息內容
			Console.WriteLine("\n");
		}

		// 發送隨機溫度請求的方法
		public static void SendRandomTemperatureRequest(byte stationNumber)
		{
			try
			{
				Random random = new Random();
				int randomValue = random.Next(50, 128); // 生成 50 ～ 127 的隨機整數

				// 創建 ModifyStationParametersRequest 消息
				var request = new ModifyStationParametersRequest
				{
					NewParameters = new List<Parameter>
					{
						new GenericParameter
						{
							Name = "保護溫度", // 根據您的要求設置名稱
                            Value = randomValue.ToString() // 傳遞整數值
                        }
					}
				};

				// 封裝成 CFXEnvelope
				var envelope = new CFXEnvelope(request)
				{
					Source = "endpoint1",         // 根據您的要求設定
					Target = "CHUNYI_PC"         // 根據您的要求設定
				};

				// 發送請求
				var resp = endpoint.ExecuteRequestAsync("amqp://127.0.0.1:8888", envelope);
				Console.WriteLine($"已發送隨機保護溫度請求到站號 {stationNumber}: {randomValue}°C");
				// 將結果轉換為格式化的 JSON 字串
				var formattedJson = JsonConvert.SerializeObject(resp.Result, Formatting.Indented);
				Console.WriteLine(formattedJson); // 輸出格式化後的 JSON
			}
			catch (Exception ex)
			{
				Console.WriteLine($"發送隨機保護溫度請求時發生錯誤: {ex.Message}");
			}


		}
	}

	// 主程序入口
	class Program
	{
		static void Main(string[] args)
		{
			Endpoint1.StartEndpoint(); // 啟動 Endpoint1

			// 設置循環發送隨機請求
			Task.Run(() =>
			{
				while (true)
				{
					Endpoint1.SendRandomTemperatureRequest(1); // 假設站號為 1
					Thread.Sleep(10000); // 每 10 秒發送一次
				}
			});

			Console.ReadKey(); // 等待用戶按鍵退出
		}
	}
}
