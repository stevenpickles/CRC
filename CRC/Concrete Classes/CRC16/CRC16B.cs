using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC16B<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC16B<T> _instance;
        private static object _flag = new object();
        public static CRC16B<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16B<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16B() : base( 16 , 0x8005 , 0x0000 , 0x0000 , true , true  , true )
        {
        }
    }
}
