using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC16AUGCCITTBF<T> : ACRCBitByBitFast<T>
        where T : IEnumerable
    {
        private static volatile CRC16AUGCCITTBF<T> _instance;
        private static object _flag = new object();
        public static CRC16AUGCCITTBF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16AUGCCITTBF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16AUGCCITTBF() : base( 16 , 0x1021 , 0x1D0F , 0x0000 , false , false , true )
        {
        }
    }
}
