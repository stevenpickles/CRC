using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC8B<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC8B<T> _instance;
        private static object _flag = new object();
        public static CRC8B<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC8B<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC8B() : base( 8 , 0x07 , 0x00 , 0x00 , false , false  , true )
        {
        }
    }
}
