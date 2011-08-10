using System;
using System.Collections;

namespace CRC
{
    public class CRC16AUGCCITTTF<T> : ACRCTableFast<T>
        where T : IEnumerable
    {
        private static volatile CRC16AUGCCITTTF<T> _instance;
        private static object _flag = new object();
        public static CRC16AUGCCITTTF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16AUGCCITTTF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16AUGCCITTTF() : base( 16 , 0x1021 , 0x1D0F , 0x0000 , false , false , true )
        {
        }
    }
}
