using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC4ITUBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC4ITUBF<T> _instance;
        private static object _flag = new object();
        public static CRC4ITUBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC4ITUBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC4ITUBF() : base( 4 , 0x3 , 0x0 , 0x0 , true , true  , true )
        {
        }
    }
}
