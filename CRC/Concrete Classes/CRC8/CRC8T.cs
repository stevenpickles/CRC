using System;
using System.Collections;

namespace CRC
{
    public sealed class CRC8T<T> : ACRCTable<T>
        where T : IEnumerable
    {
        private static volatile CRC8T<T> _instance;
        private static object _flag = new object();
        public static CRC8T<T> Instance
        {
            get
            {
                if ( _instance == null )
                {
                    lock ( _flag )
                    {
                        if ( _instance == null )
                        {
                            _instance = new CRC8T<T>();
                        }
                    }
                }
                
                return _instance;
            }
        }

        private CRC8T() : base( 8 , 0x07 , 0x00 , 0x00 , false , false  , true )
        {
        }
    }
}
