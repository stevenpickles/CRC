using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC6DARCBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC6DARCBF<T> _instance;
        private static object _flag = new object();
        public static CRC6DARCBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC6DARCBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC6DARCBF() : base( 6 , 0x19 , 0x00 , 0x00 , true , false  , true )
        {
        }
    }
}
