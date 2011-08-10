using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC16AUGCCITTB<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC16AUGCCITTB<T> _instance;
        private static object _flag = new object();
        public static CRC16AUGCCITTB<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16AUGCCITTB<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16AUGCCITTB() : base( 16 , 0x1021 , 0x1D0F , 0x0000 , false , false , true )
        {
        }
    }
}
