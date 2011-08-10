using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC32B<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC32B<T> _instance;
        private static object _flag = new object();
        public static CRC32B<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC32B<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC32B() : base( 32 , 0x04C11DB7 , 0xFFFFFFFF , 0xFFFFFFFF , true , true  , true )
        {
        }
    }
}
