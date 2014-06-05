using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeError
{
	class Program
	{
		static void Main(string[] args)
		{
			var testError = new TestError();
			testError.Init();
			testError.Start();


			Thread.Sleep(1000);

			testError.Stop(); //FOR FIX REPOLCE STRINGS 22 AND 23
			testError.Deinit();


			//wait error unhandled

			Console.ReadLine();
		}
	}

	public class TestError
	{
		private SerialPort _serialPort;
		private Thread _thread;

		public TestError()
		{
			var r = SerialPort.GetPortNames();

			_serialPort = new SerialPort(r[0], 115200);
		}

		public void Init()
		{
			_serialPort.Open();
		}

		public void Start()
		{
			_thread = new Thread(() =>
			{
				while (_serialPort != null && _serialPort.IsOpen)
				{
					var line = _serialPort.ReadLine();
					Console.WriteLine(line);
				}
			});

			_thread.Start();
		}

		public void Deinit()
		{
			_serialPort.Close();
			_serialPort.Dispose();
		}

		public void Stop()
		{
			_thread.Abort();
		}
	}
}
