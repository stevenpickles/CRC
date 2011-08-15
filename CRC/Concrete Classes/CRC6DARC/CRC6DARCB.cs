using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC6DARCB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC6DARCB<T> _instance;
        private static object _flag = new object();
        public static CRC6DARCB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC6DARCB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC6DARCB() : base( 6 , 0x19 , 0x00 , 0x00 , true , false  , true )
        {
        }
    }
}
