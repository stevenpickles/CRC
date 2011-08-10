using System;
using System.Collections;

namespace CRC
{
    public abstract class ACRCBitByBitFast<T> : ACRCBitByBit<T>
        where T : IEnumerable
    {

        public ACRCBitByBitFast( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct ) : this( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , default( T ) ) 
        {
        }

        public ACRCBitByBitFast( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct , T message ) : base( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , message )
        {   
        }

        protected override void GenerateCRC()
        {
            if ( _message == null )
            {
                throw new NullReferenceException( "message must be initialized and with a length greater than 0" );
            }

            ulong currentBit = 0;
	        
            _calculatedCRC = _initialDirect;

            foreach ( var element in _message )
            {
		        byte currentByte = Convert.ToByte( element );

		        if ( _reflectInitial )
                {
                    currentByte = (byte) Reflect( currentByte , 8 );
                }

		        for ( ulong bitNumber = 0x80; bitNumber > 0; bitNumber = bitNumber >> 1 ) 
                {

			        currentBit = _calculatedCRC & _highBit;
			        _calculatedCRC = _calculatedCRC << 1;

			        if ( ( currentByte & bitNumber ) > 0 )
                    {
                        currentBit = currentBit ^ _highBit;
                    }
			        
                    if ( currentBit > 0 )
                    {
                        _calculatedCRC = _calculatedCRC ^ _polynomial;
                    }
		        }
	        }	

	        if (_reflectFinal)
            {
                _calculatedCRC = Reflect( _calculatedCRC , _order );
            }
	        
            _calculatedCRC = _calculatedCRC ^ _finalXOR;
	        
            _calculatedCRC = _calculatedCRC & _mask;                        //  trim to correct number of bits
        }

    }
}
