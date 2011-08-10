using System;
using System.Collections;
using System.Collections.Generic;

namespace CRC
{
    public abstract class ACRCTableFast<T> : ACRCTable<T>
        where T : IEnumerable
    {

        public ACRCTableFast( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct ) : this( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , default( T ) )
        {
        }

        public ACRCTableFast( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct , T message ) : base( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , message )
        {
        }

        
        protected override void GenerateCRC()
        {
         	if ( _message == null )
            {
                throw new NullReferenceException( "message must be initialized and with a length greater than 0" );
            }

            _calculatedCRC = _initialDirect;

	        if ( _reflectInitial )                                                   //  reflect before calculating CRC, if necessary
            {
                _calculatedCRC = Reflect( _calculatedCRC , _order );
            }

	        if ( !_reflectInitial )
            {
                foreach ( var element in _message )
                {
                    byte currentByte = Convert.ToByte( element );
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC << 8;
                    _calculatedCRC = _calculatedCRC ^ _table[ ( ( temp >> ( _order - 8 ) )  & 0xFF ) ^ currentByte ];
                }
            }
	        else
            {
                foreach ( var element in _message )
                {
                    byte currentByte = Convert.ToByte( element );
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC >> 8 ;
                    _calculatedCRC = _calculatedCRC ^ _table[ ( temp & 0xFF ) ^ currentByte ];
                }
            }

	        if ( _reflectInitial ^ _reflectFinal )                               //  reflect after calculating CRC, if necessary
            {
                _calculatedCRC = Reflect( _calculatedCRC , _order );
            }
	        
	        _calculatedCRC = _calculatedCRC ^ _finalXOR;
            
            _calculatedCRC = _calculatedCRC & _mask;                        //  trim to correct number of bits
        }

    }
}
