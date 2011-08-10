using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC32TF<T> : ACRCTableFast<T>
        where T : IEnumerable
    {
        private static volatile CRC32TF<T> _instance;
        private static object _flag = new object();
        public static CRC32TF<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC32TF<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC32TF() : base( 32 , 0x04C11DB7 , 0xFFFFFFFF , 0xFFFFFFFF , true , true  , true )
        {
        }
    }
}
