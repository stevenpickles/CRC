using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC3ROHCB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC3ROHCB<T> _instance;
        private static object _flag = new object();
        public static CRC3ROHCB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC3ROHCB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC3ROHCB() : base( 3 , 0x3 , 0x7 , 0x00 , true , true  , true )
        {
        }
    }
}
