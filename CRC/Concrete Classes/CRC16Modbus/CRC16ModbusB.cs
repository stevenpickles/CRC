using System;
using System.Collections;

namespace CRC
{
    public class CRC16ModbusB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC16ModbusB<T> _instance;
        private static object _flag = new object();
        public static CRC16ModbusB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16ModbusB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16ModbusB() : base( 16 , 0x8005 , 0xFFFF , 0x0000 , true , true  , true )
        {
        }
    }
}
