using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC32T<T> : ACRCTable<T>
        where T : IEnumerable
    {
        private static volatile CRC32T<T> _instance;
        private static object _flag = new object();
        public static CRC32T<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC32T<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC32T() : base( 32 , 0x04C11DB7 , 0xFFFFFFFF , 0xFFFFFFFF , true , true  , true )
        {
        }
    }
}
