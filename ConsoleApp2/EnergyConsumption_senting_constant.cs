//using System;
//using System.Threading.Tasks;
//using CFX;
//using CFX.Transport;
//using CFX.ResourcePerformance;

//namespace machine1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            AmqpCFXEndpoint endpoint = new AmqpCFXEndpoint();
//            string handle = "endpoint1";
//            endpoint.Open(handle, new Uri("amqp://10.149.66.140:8888"));
//            endpoint.AddListener("event");
//            endpoint.OnCFXMessageReceivedFromListener += Endpoint_OnCFXMessageReceivedFromListener;

//            // 創建 EnergyDataMessage 實例並填充數據
//            var energyData = new EnergyDataMessage
//            {
//                StartTime = DateTime.Parse("2018-04-05T09:33:06.1358356-04:00"),
//                EndTime = DateTime.Parse("2018-04-05T09:38:06.1358356-04:00"),
//                EnergyUsed = 0.012,
//                PeakPower = 125.6,
//                MinimumPower = 120.3,
//                MeanPower = 124.6,
//                PowerNow = 121.1,
//                PowerFactorNow = 0.95,
//                PeakCurrent = 0.82,
//                MinimumCurrent = 0.00,
//                MeanCurrent = 0.68,
//                CurrentNow = 0.69,
//                PeakVoltage = 239.1,
//                MinimumVoltage = 231.6,
//                MeanVoltage = 232.9,
//                VoltageNow = 212.1,
//                PeakFrequency = 52,
//                MinimumFrequency = 50.5,
//                MeanFrequency = 50.6,
//                FrequencyNow = 50.6,
//                PeakPowerRYB = new double[] { 125.6, 77.4, 10.2 },
//                MinimumPowerRYB = new double[] { 120.3, 68.5, 8.5 },
//                MeanPowerRYB = new double[] { 124.6, 70.2, 9.8 },
//                PowerNowRYB = new double[] { 121.1, 68.9, 10.1 },
//                PowerFactorNowRYB = new double[] { 0.95, 0.92, 0.80 },
//                PeakCurrentRYB = new double[] { 0.82, 0.65, 0.33 },
//                MinimumCurrentRYB = new double[] { 0.00, 0.01, 0.32 },
//                MeanCurrentRYB = new double[] { 0.68, 0.58, 0.32 },
//                CurrentNowRYB = new double[] { 0.69, 0.57, 0.32 },
//                PeakVoltageRYB = new double[] { 420.1, 413.7, 404.6 },
//                MinimumVoltageRYB = new double[] { 393.6, 399.8, 397.4 },
//                MeanVoltageRYB = new double[] { 402.9, 400.1, 402.3 },
//                VoltageNowRYB = new double[] { 401.1, 401.5, 402.3 },
//                ThreePhaseNeutralCurrentNow = 0.06
//            };

//            // 將消息封裝在 CFXEnvelope 中
//            var envelope = new CFXEnvelope(energyData) { Source = "endpoint1", Target = "endpoint2" };

//            // 發送數據
//            endpoint.PublishToMessageSource("MessageSource", envelope);

//            Console.WriteLine("Energy data sent successfully.");
//            Console.ReadKey();
//        }

//        private static void Endpoint_OnCFXMessageReceivedFromListener(string targetAddress, CFXEnvelope message)
//        {
//            Console.WriteLine("endpoint1 CFXMessageReceivedFromListener" + "\n");
//            Console.WriteLine(message.ToJson());
//            Console.WriteLine("\n");
//        }
//    }
//}



