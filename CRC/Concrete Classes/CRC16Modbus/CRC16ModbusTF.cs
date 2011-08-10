using System;
using System.Collections;

namespace CRC
{
    public class CRC16ModbusTF<T> : ACRCTableFast<T>
        where T : IEnumerable
    {
        private static volatile CRC16ModbusTF<T> _instance;
        private static object _flag = new object();
        public static CRC16ModbusTF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16ModbusTF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16ModbusTF() : base( 16 , 0x8005 , 0xFFFF , 0x0000 , true , true  , true )
        {
        }
    }
}
