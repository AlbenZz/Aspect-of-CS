using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLearning
{

    #region Enumeration type

    /**
     * 1. The approved types for an enum are byte, ushort, unit, ulong, sbyte, short, int, long
     * 2. default value
     *      
     * 3. initializers to override default value
     *      ==========!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!==============
     *         
     *          the order of initilizers should be taken care of>>>
     *              enum Case1 { Const1 = 1, Const2, Const3, Const4, Const5 = 3, Const6 }
     *              Const1 = 1, C2 = 2, C3 = 3, C4 = 4, C5 = 3, C6 = 4;
     *                  >> C5 references to C3
     *                   Console.Write(
     *                  >> C6 refermeces to C4
     *                  
     * 3. default value as Class or Instance Variable (Not local)
     *    When you create an enum, select the most logical default value and give it a value of zero. 
     *      That will cause all enums to have that default value if they are not explicitly assigned a value when they are created.
     * 
     * 4. any
     * 
     * 
     */



    enum Day { Sat, Sun, Mon, Tue, Wed, Thr, Fri}


    enum Case1 { Const1 = 1, Const2, Const3, Const4, Const5 = 3, Const6 }
    enum Case2 { Const1, Const2, Const3, Const4, Const5 = 6, Const6 }
    // modify the underlying type of constant
    enum Month : byte { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec}
    class EnumTester
    {
        public enum Test { 
            D, C = 0
        }

        // nested type accessibility in type definition/declaration(in msdn) needed to be considered
        public Test A;
        // ===================================
        static void Main(string[] args)
        {


            // print Case
            Console.WriteLine("Case1: ");
            foreach (int i in Enum.GetValues(typeof(Case1)))
            {
                Console.WriteLine("\t" + Enum.GetName(typeof(Case1), i) + ": " + i);
            }
            Console.WriteLine("\t" + Case1.Const5);

            Console.WriteLine("Case2: ");
            foreach (int  i in Enum.GetValues(typeof(Case2)))
            {
                Console.WriteLine("\t" + Enum.GetName(typeof(Case2), i) + ": " + i);
            }




            Console.WriteLine(Test.C);
            Test c = (Test)1234;
            int a = (int)c;
            Console.WriteLine(a);


            EnumTester t = new EnumTester();
            Console.WriteLine(t.A);


            Console.Read();

        }
	
    }


    #endregion

}
