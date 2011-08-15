using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC5USBB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC5USBB<T> _instance;
        private static object _flag = new object();
        public static CRC5USBB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC5USBB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC5USBB() : base( 5 , 0x05 , 0x1F , 0x1F , true , true  , true )
        {
        }
    }
}
