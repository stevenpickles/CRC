using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC5EPCB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC5EPCB<T> _instance;
        private static object _flag = new object();
        public static CRC5EPCB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC5EPCB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC5EPCB() : base( 5 , 0x09 , 0x09 , 0x00 , false , false , true )
        {
        }
    }
}
