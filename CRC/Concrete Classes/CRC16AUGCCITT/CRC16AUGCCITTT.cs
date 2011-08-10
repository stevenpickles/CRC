using System;
using System.Collections;

namespace CRC
{
    public class CRC16AUGCCITTT<T> : ACRCTable<T>
        where T : IEnumerable
    {
        private static volatile CRC16AUGCCITTT<T> _instance;
        private static object _flag = new object();
        public static CRC16AUGCCITTT<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC16AUGCCITTT<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC16AUGCCITTT() : base( 16 , 0x1021 , 0x1D0F , 0x0000 , false , false , true )
        {
        }
    }
}
