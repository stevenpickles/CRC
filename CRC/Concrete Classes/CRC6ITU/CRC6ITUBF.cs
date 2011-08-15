using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC6ITUBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC6ITUBF<T> _instance;
        private static object _flag = new object();
        public static CRC6ITUBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC6ITUBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC6ITUBF() : base( 6 , 0x03 , 0x00 , 0x00 , true , true  , true )
        {
        }
    }
}
