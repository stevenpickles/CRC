using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC16TF<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {
        private static volatile CRC16TF<T> _instance;
        private static object _flag = new object();
        public static CRC16TF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16TF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16TF() : base( 16 , 0x8005 , 0x0000 , 0x0000 , true , true  , true )
        {
        }
    }
}
