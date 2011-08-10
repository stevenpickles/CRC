using System;
using System.Collections;
using System.Collections.Generic;

namespace CRC
{
    public abstract class ACRCTable<T> : ACRC<T>
        where T : IEnumerable
    {

        protected ulong[] _table;
        //public ulong[] Table { get ; private set; }

        private int _theOrder;
        protected override int _order                                                            //  CRC polynomial order, without the leading 1 bit (eg, order of 8 is as follows:  x^8 + x^4 + x^2 + x^1 + 1)
        {
            get
            {
                return _theOrder;
            }
            set
            {
                if ( ( ( value >= 1 ) && ( value <= 32 ) ) && ( value % 8 == 0 ) )
                {
                    _theOrder = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException( "order" , "Value not 8, 16, 24 or 32" );
                }
            }
        }

        public ACRCTable( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct ) : this( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , default( T ) )
        {
        }

        public ACRCTable( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct , T message ) : base( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , message )
        {
            _table = GenerateTable();
        }

        protected virtual ulong[] GenerateTable()
        {
            ulong[] table = new ulong[ 256 ];

            ulong currentBit = 0;
            ulong currentCRC = 0;

            //  Iterate through all of the 256 possible bit combinations, bit-by-bit, and
            //  populate the _table with the generated CRC value.
            for( int byteNumber = 0; byteNumber < 256; byteNumber++ )
            {
                currentCRC = (ulong) byteNumber;                                        //  initial value is the value of the current byte

                if ( _reflectInitial )                                               //  reflect CRC, if necessary (8 bits)
                {
                    currentCRC = Reflect( currentCRC , 8 );
                }

                currentCRC = currentCRC << ( _order - 8 );                          //  

                for( int bitNumber = 0; bitNumber < 8; bitNumber++ )
                {
                    currentBit = currentCRC & _highBit;
                    currentCRC = currentCRC << 1;
                    
                    if ( currentBit > 0 )
                    {
                        currentCRC = currentCRC ^ _polynomial;
                    }
                }

                if ( _reflectInitial )                                               //  reflect CRC, if necessary (_order bits)
                {
                    currentCRC = Reflect( currentCRC , _order );
                }

                currentCRC = currentCRC & _mask;                                    //  mask CRC to set number of bits

                table[ byteNumber ] = currentCRC;                                       //  finally, add to the _table
            }

            return table;
        }
        
        protected override void GenerateCRC()
        {
         	if ( _message == null )
            {
                throw new NullReferenceException( "message must be initialized and with a length greater than 0" );
            }

            _calculatedCRC = _initialNonDirect;

	        if ( _reflectInitial )                                                   //  reflect before calculating CRC, if necessary
            {
                _calculatedCRC = Reflect( _calculatedCRC , _order );
            }

	        if ( ! ( _reflectInitial ) )
            {
                foreach ( var element in _message )
                {
                    byte currentByte = Convert.ToByte( element );
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC << 8;
                    _calculatedCRC = _calculatedCRC | (ulong) currentByte; 
                    _calculatedCRC = _calculatedCRC ^ this._table[ ( temp >> ( _order - 8 ) )  & 0xFF ];
                }
            }
	        else
            {
                foreach ( var element in _message )
                {
                    byte currentByte = Convert.ToByte( element );
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC >> 8;
                    _calculatedCRC = _calculatedCRC | ( ( (ulong) currentByte ) << ( _order - 8 ) );
                    _calculatedCRC = _calculatedCRC ^ this._table[ temp & 0xFF ];
                }
            }

	        if ( ! ( _reflectInitial ) )
            {
                for( int index = 0; index < ( _order >> 3 ); ++index )
                {
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC << 8;
                    _calculatedCRC = _calculatedCRC ^ this._table[ ( temp >> ( _order - 8 ) )  & 0xFF ];
                }
	        }
            else 
            {
                for( int index = 0; index < ( _order >> 3 ); ++index )
                {
                    ulong temp = _calculatedCRC;
                    _calculatedCRC = _calculatedCRC >> 8;
                    _calculatedCRC = _calculatedCRC ^ this._table[ temp & 0xFF ];
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
