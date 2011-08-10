using System;
using System.Collections;
using System.Collections.Generic;

namespace CRC
{
    public abstract class ACRC<T>
        where T : IEnumerable
    {

        private int _theOrder;
        protected virtual int _order                                                        //  CRC polynomial order, without the leading 1 bit (eg, order of 8 is as follows:  x^8 + x^4 + x^2 + x^1 + 1)
        {
            get
            {
                return _theOrder;
            }
            set
            {
                if ( ( value >= 1 ) && ( value <= 32 ) )
                {
                    _theOrder = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException( "order" , "Value not between 1 and 32" );
                }
            }
        }

        protected ulong _polynomial;
        //public ulong Polynomial { get { return _polynomial; } }                         //  CRC polynomial, without the leading 1 bit
        
        protected ulong _initialValue;
        //public ulong InitialValue { get { return _initialValue; } }                     //  initial value to which the calculatedCRC is set
        
        protected ulong _finalXOR;
        //public ulong FinalXOR { get; private set; }                                     //  final value to which the calculatedCRC is XOR'd
        
        protected bool _reflectInitial;
        //public bool ReflectInitial { get; private set; }                                //  specifies if a data byte is reflected before processing
        
        protected bool _reflectFinal;
        //public bool ReflectFinal { get; private set; }                                  //  specifies if a data byte is reflected after processing
        
        protected bool _direct;
        //public bool Direct { get; private set; }                                        //  direct, when true, means no augmented zero bits
        
        protected ulong _calculatedCRC;
        //public ulong CalculatedCRC { get { return _calculatedCRC; } }                   //  calculated CRC value

        //protected byte[] _message;
        protected T _message;
        //public byte[] Message;                                                          //  data message
        
        private ulong _bit;
        protected ulong _mask;                                      //  bit mask of size of _order
        protected ulong _highBit { get; private set; }                                   //  MSB of bit mask 1, all others 0
        protected ulong _initialDirect;                                                  //  initial value with no augmented zero bits
        protected ulong _initialNonDirect;                                               //  initial value with augmented zero bits
        
        
        public ACRC( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct ) : this( order , polynomial , initialValue , finalXOR , reflectInitial , reflectFinal , direct , default( T ) )
        {
        }

        public ACRC( int order , ulong polynomial , ulong initialValue , ulong finalXOR , bool reflectInitial , bool reflectFinal , bool direct , T message )
        {

            //  Set instance variables
            _order = order;
            _polynomial = polynomial;
            _initialValue = initialValue;
            _finalXOR = finalXOR;
            _reflectInitial = reflectInitial;
            _reflectFinal = reflectFinal;
            _direct = direct;
            _message = message;


            _mask = (ulong) 1 << ( _order - 1 );                                     //  MSB is 1, all other bits 0
            _mask = _mask - 1;                                                  //  MSB is 0, all other bits 1
            _mask = _mask << 1;                                                 //  LSB is 0, all other bits 1
            _mask = _mask | 1;                                                  //  all mask bits are 1


            _highBit = (ulong) 1 << ( _order - 1 );                                  //  MSB is 1, all other bits 0

            if ( _polynomial != ( _polynomial & _mask ) )
            {
                throw new ArgumentOutOfRangeException( "polynomial" , "Invalid polynomial" );
            }

            if ( _initialValue != ( _initialValue & _mask ) )
            {
                throw new ArgumentOutOfRangeException( "initialValue" , "Invalid initial value" );
            }

            if ( _finalXOR != ( _finalXOR & _mask ) )
            {
                throw new ArgumentOutOfRangeException( "finalXOR" , "Invalid final XOR value" );
            }


            //  Generate initial direct and nondirect values
            _calculatedCRC = _initialValue;
            if ( ! ( _direct ) )
            {
                _initialNonDirect = _initialValue;
                
                for( int index = 0; index < _order; index++ )
                {
                    _bit = _calculatedCRC & _highBit;
                    _calculatedCRC = _calculatedCRC << 1;
                    
                    if ( _bit > 0 )
                    {
                        _calculatedCRC = _calculatedCRC ^ _polynomial;
                    }
                }
                
                _calculatedCRC = _calculatedCRC & _mask;
                _initialDirect = _calculatedCRC;
            }
            else
            {
                _initialDirect = _initialValue;
                
                for( int index = 0; index < _order; index++ )
                {
                    _bit = _calculatedCRC & 1;
                    
                    if ( _bit > 0 )
                    {
                        _calculatedCRC = _calculatedCRC ^ _polynomial;
                    }

                    _calculatedCRC = _calculatedCRC >> 1;

                    if ( _bit > 0 )
                    {
                        _calculatedCRC = _calculatedCRC | _highBit;
                    }

                    _initialNonDirect = _calculatedCRC;
                }
            }

        }

        
        protected ulong Reflect( ulong data , int numberOfBits )
        {
            ulong reflectedData = 0;
            ulong setBit = 1;

            //  Start with the MSB of data and traverse bit-by-bit until
            //  all bits have been tested.  For each 1 bit in data, there
            //  will be a corresponding 1 bit in reflectedData the same
            //  distance from the starting position (in reverse order).
            for( ulong testBit = (ulong) 1 << ( numberOfBits - 1 ) ; testBit > 0 ; testBit = testBit >> 1 )
            {
                if ( ( data & testBit ) != 0 )
                {
                    reflectedData = reflectedData | setBit;
                }

                setBit = setBit << 1;
            }

            return reflectedData;
        }



        protected abstract void GenerateCRC();
        
        public virtual int GetCRC( T message )
        {
            _message = message;
            GenerateCRC();
            return ( (int) _calculatedCRC );
        }

    }
}
