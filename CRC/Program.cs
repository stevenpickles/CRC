using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC
{
    class Program
    {

        static void Main( string[] args )
        {
            string testString = "123456789";

            Console.WriteLine( "------------------------------------------------------------------------------" );
            Console.WriteLine( "CRC Type                   BBB    BBB Fast       Table  Table Fast     Special" );
            Console.WriteLine( "------------------------------------------------------------------------------" );
            
            PrintCRC( "Example CRC" , 
                      "{0:x8}" ,
                      0x12345678 ,
                      0x12345678 ,
                      0x12345678 ,
                      0x12345678 ,
                      0x12345678 );
            
            PrintCRC( "CRC-3/ROHC" , 
                      "{0:x1}" ,
                      CRC3ROHCB<string>.Instance.GetCRC( testString ) ,
                      CRC3ROHCBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-4/ITU" , 
                      "{0:x1}" ,
                      CRC4ITUB<string>.Instance.GetCRC( testString ) ,
                      CRC4ITUBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-5/EPC" , 
                      "{0:x2}" ,
                      CRC5EPCB<string>.Instance.GetCRC( testString ) ,
                      CRC5EPCBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-5/ITU" , 
                      "{0:x2}" ,
                      CRC5ITUB<string>.Instance.GetCRC( testString ) ,
                      CRC5ITUBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-5/USB" , 
                      "{0:x2}" ,
                      CRC5USBB<string>.Instance.GetCRC( testString ) ,
                      CRC5USBBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-6/DARC" , 
                      "{0:x2}" ,
                      CRC6DARCB<string>.Instance.GetCRC( testString ) ,
                      CRC6DARCBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-6/ITU" , 
                      "{0:x2}" ,
                      CRC6ITUB<string>.Instance.GetCRC( testString ) ,
                      CRC6ITUBF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-8" , 
                      "{0:x2}" ,
                      CRC8B<string>.Instance.GetCRC( testString ) ,
                      CRC8BF<string>.Instance.GetCRC( testString ) ,
                      CRC8T<string>.Instance.GetCRC( testString ) ,
                      CRC8TF<string>.Instance.GetCRC( testString ) );
            
            PrintCRC( "CRC-16/Modbus" , 
                      "{0:x4}" ,
                      CRC16ModbusB<string>.Instance.GetCRC( testString ) ,
                      CRC16ModbusBF<string>.Instance.GetCRC( testString ) ,
                      CRC16ModbusT<string>.Instance.GetCRC( testString ) ,
                      CRC16ModbusTF<string>.Instance.GetCRC( testString ) ,
                      CRC16Modbus<string>.GetCRC( testString ) );

            PrintCRC( "CRC-16/AUG-CCITT" , 
                      "{0:x4}" ,
                      CRC16AUGCCITTB<string>.Instance.GetCRC( testString ) ,
                      CRC16AUGCCITTBF<string>.Instance.GetCRC( testString ) ,
                      CRC16AUGCCITTT<string>.Instance.GetCRC( testString ) ,
                      CRC16AUGCCITTTF<string>.Instance.GetCRC( testString ) );

            PrintCRC( "CRC-32" , 
                      "{0:x8}" ,
                      CRC32B<string>.Instance.GetCRC( testString ) ,
                      CRC32BF<string>.Instance.GetCRC( testString ) ,
                      CRC32T<string>.Instance.GetCRC( testString ) ,
                      CRC32TF<string>.Instance.GetCRC( testString ) );

        }
        
        static void PrintCRC( string description , string format , int bitByBit , int bitByBitFast , int table , int tableFast , int special )
        {
            PrintCRCHelper( description , format , bitByBit , bitByBitFast );
            Console.Write( ( "0x" + String.Format( format , table ).ToUpper() ).PadLeft( 12 ) );
            Console.Write( ( "0x" + String.Format( format , tableFast ).ToUpper() ).PadLeft( 12 ) );
            Console.Write( ( "0x" + String.Format( format , special ).ToUpper() ).PadLeft( 12 ) );
            Console.WriteLine();
        }
        
        static void PrintCRC( string description , string format , int bitByBit , int bitByBitFast , int table , int tableFast )
        {
            PrintCRCHelper( description , format , bitByBit , bitByBitFast );
            Console.Write( ( "0x" + String.Format( format , table ).ToUpper() ).PadLeft( 12 ) );
            Console.Write( ( "0x" + String.Format( format , tableFast ).ToUpper() ).PadLeft( 12 ) );
            Console.Write( ( "N/A" ).PadLeft( 12 ) );
            Console.WriteLine();
        }
        
        static void PrintCRC( string description , string format , int bitByBit , int bitByBitFast )
        {
            PrintCRCHelper( description , format , bitByBit , bitByBitFast );
            Console.Write( ( "N/A" ).PadLeft( 12 ) );
            Console.Write( ( "N/A" ).PadLeft( 12 ) );
            Console.Write( ( "N/A" ).PadLeft( 12 ) );
            Console.WriteLine();
        }
        
        static void PrintCRCHelper( string description , string format , int bitByBit , int bitByBitFast )
        {
            Console.Write( String.Format( description + ":" ).PadRight( 18 ) );
            Console.Write( ( "0x" + String.Format( format , bitByBit ).ToUpper() ).PadLeft( 12 ) );
            Console.Write( ( "0x" + String.Format( format , bitByBitFast ).ToUpper() ).PadLeft( 12 ) );
        }

    }
}
