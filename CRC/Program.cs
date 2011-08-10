using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC
{
    class Program
    {
        static void Main( string[] args )
        {
            string testString = "123456789";

            Console.WriteLine( "----------------------------------------------------------------------" );
            Console.WriteLine( "CRC Type             BBB         BBB Fast       Table       Table Fast" );
            Console.WriteLine( "----------------------------------------------------------------------" );
            Console.WriteLine( "Example CRC:      0x12345678    0x12345678    0x12345678    0x12345678" );
            Console.WriteLine( "CRC-16 (Modbus):      0x" + String.Format( "{0:x4}" , CRC16ModbusB<string>.Instance.GetCRC( testString ) ).ToUpper() + 
                                             "        0x" + String.Format( "{0:x4}" , CRC16ModbusBF<string>.Instance.GetCRC( testString ) ).ToUpper() +
                                             "        0x" + String.Format( "{0:x4}" , CRC16ModbusT<string>.Instance.GetCRC( testString ) ).ToUpper() +
                                             "        0x" + String.Format( "{0:x4}" , CRC16ModbusTF<string>.Instance.GetCRC( testString ) ).ToUpper() );

        }
    }
}
