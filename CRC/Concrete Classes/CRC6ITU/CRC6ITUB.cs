using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC6ITUB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC6ITUB<T> _instance;
        private static object _flag = new object();
        public static CRC6ITUB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC6ITUB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC6ITUB() : base( 6 , 0x03 , 0x00 , 0x00 , true , true  , true )
        {
        }
    }
}
