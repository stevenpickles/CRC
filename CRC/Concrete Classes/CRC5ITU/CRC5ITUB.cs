using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC5ITUB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC5ITUB<T> _instance;
        private static object _flag = new object();
        public static CRC5ITUB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC5ITUB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC5ITUB() : base( 5 , 0x15 , 0x00 , 0x00 , true , true  , true )
        {
        }
    }
}
