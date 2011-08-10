using System;
using System.Collections;

namespace CRC
{
    public class CRC16ModbusT<T> : ACRCTable<T>
        where T : IEnumerable
    {
        private static volatile CRC16ModbusT<T> _instance;
        private static object _flag = new object();
        public static CRC16ModbusT<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16ModbusT<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16ModbusT() : base( 16 , 0x8005 , 0xFFFF , 0x0000 , true , true  , true )
        {
        }
    }
}
