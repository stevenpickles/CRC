using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC8BF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC8BF<T> _instance;
        private static object _flag = new object();
        public static CRC8BF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC8BF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC8BF() : base( 8 , 0x07 , 0x00 , 0x00 , false , false  , true )
        {
        }
    }
}
