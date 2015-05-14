using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilienDuell
{
    internal class SNESBuzzer : IBuzzer
    {
        private HidDevice _player1;
        private HidDevice _player2;

        public event EventHandler<BuzzeredEventArgs> Buzzered;

        private void OnBuzzered(int player)
        {
            if (Buzzered != null)
                Buzzered(this, new BuzzeredEventArgs(player));
        }

        internal SNESBuzzer()
        {
            var deviceCount = 0;
            foreach (var d in HidDevices.Enumerate(121)) //Vendor_ID 0x0079
            {
                if (deviceCount == 0)
                {
                    _player1 = d;
                    _player1.OpenDevice();
                    _player1.Read(new ReadCallback(ReadDataPlayer1));

                    deviceCount++;
                }
                else if (deviceCount == 1)
                {
                    _player2 = d;
                    _player2.OpenDevice();
                    _player2.Read(new ReadCallback(ReadDataPlayer2));

                    deviceCount++;
                }
            }
        }

        private void ReadDataPlayer1(HidDeviceData data)
        {
            if (data.Status == HidDeviceData.ReadStatus.Success)
            {
                var button = (int)data.Data[6];
                if (button == 47 /*A*/ || button == 79 /*B*/ || button == 143 /*Y*/ || button == 31 /*X*/)
                {
                    OnBuzzered(1);
                }
            }

            _player1.Read(new ReadCallback(ReadDataPlayer1));
        }

        private void ReadDataPlayer2(HidDeviceData data)
        {
            if (data.Status == HidDeviceData.ReadStatus.Success)
            {
                var button = (int)data.Data[6];
                if (button == 47 /*A*/ || button == 79 /*B*/ || button == 143 /*Y*/ || button == 31 /*X*/)
                {
                    OnBuzzered(2);
                }
            }

            _player2.Read(new ReadCallback(ReadDataPlayer2));
        }
    }
}
