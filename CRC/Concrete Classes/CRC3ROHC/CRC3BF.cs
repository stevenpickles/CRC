using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC3ROHCBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC3ROHCBF<T> _instance;
        private static object _flag = new object();
        public static CRC3ROHCBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC3ROHCBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC3ROHCBF() : base( 3 , 0x7 , 0x3 , 0x00 , true , true  , true )
        {
        }
    }
}
