using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC5EPCBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC5EPCBF<T> _instance;
        private static object _flag = new object();
        public static CRC5EPCBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC5EPCBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC5EPCBF() : base( 5 , 0x09 , 0x09 , 0x00 , false , false , true )
        {
        }
    }
}
