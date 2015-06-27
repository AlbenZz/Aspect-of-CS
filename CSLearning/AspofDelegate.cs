using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CSLearning.AspofDelegate
{

    /*class AspofDelegate
    {
     * Context
     *      purpose
     *          1. methods as paratmeters allows code to dynamically decide which method should be used as the condition differs.
     * 
     * skill level:
     *     
     * usage:
     *  1.  definition
     *          as a type like enum, class, struct or union
     *              
     *          [public delegate 'return-type' 'type-name'('parameter-list');]
     *              
     *  2. declaration and later instantiatioin or direct instantiation/initiation
     *          declaration:
     *              as a class variable
     *              as a instance variable
     *          direct instantiation
     *              
     *              ['type-name' 'variable-name' = new 'type-name'('method-name-with-same-signature');]
     *  2.  invocation
     *          anInstanceReferenceOfTheTypeOfDelegate();
     *          anInstanceReferenceOfTheTypeOfDelegate.Invoke();
     *              the differency?
     *              
     * 
    }
    */

    // Basic Delegate 
    public delegate void BasicDelegate();


    public class TestBasicDelegate
    {

        public delegate void TestDelegate();

        public TestBasicDelegate()
        {
            TestDelegate td = AnInsFunc;
            td();
            td.Invoke();
        }
        //
        public static void Main(string[] args)
        {
            


            Console.WriteLine("===================================unicast delegate:");
            // unicast delegate
            BasicDelegate simpleDelegate = new BasicDelegate(new TestBasicDelegate().AnInsFunc); 

            //BasicDelegate simpleDelegate = new BasicDelegate(AClassFunc);

            simpleDelegate();
            if (simpleDelegate == null)
            {
                Console.WriteLine("null");
            }
            simpleDelegate = AClassFunc;
            simpleDelegate();


            Console.WriteLine("===================================Multicast delegate:");
            simpleDelegate += new TestBasicDelegate().AnInsFunc;
            simpleDelegate();
            Console.Read();
        }

        


        // a class function
        public static void AClassFunc()
        {
            Console.WriteLine("I'm a class func..");
            Console.WriteLine("I was called by delegate ...");
        }

        // an instance function
        public void AnInsFunc()
        {
            Console.WriteLine("I'm a instance func..");
            Console.WriteLine("I was called by delegate ...");
        }


    }

    #region Delegate As Parameter

    class DelegateAsParameter
    {
        //The modifier 'static' is not valid for this item	
        //public static delegate void ParameterDelegate(string str);

        // http://stackoverflow.com/questions/6835766/why-can-a-net-delegate-not-be-declared-static
        // summary:
        // 
        public delegate void ParameterDelegate(string str);



        public delegate void LoggerHandler(string log);

        //public LoggerHandler LogHandler;

        public void Process(LoggerHandler logHandler)
        {
            if (logHandler != null)
            {
                logHandler("Process() begin");
            }
            if (logHandler != null)
            {
                logHandler("Process() end");
            }
        }


        public DelegateAsParameter()
        {

        }


        // ===================================
        public static void Main(string[] args)
        {

        }


    }

    class FileLogger
    {
        FileStream fileStream;
        StreamWriter streamWriter;

        // constructor
        public FileLogger(string filename)
        {
            fileStream = new FileStream(filename, FileMode.Append);
            streamWriter = new StreamWriter(fileStream);
        }

        public void Log(string s) 
        {
            streamWriter.WriteLine(s);
        }

        public void Close()
        {
            streamWriter.Close();
            fileStream.Close();
        }
    }


    class TestDelegateAsParameter
    {

        // ===================================
        static void Main(string[] args)
        {
            FileLogger floger = new FileLogger("process.log");

            DelegateAsParameter logHandler = new DelegateAsParameter();

            DelegateAsParameter.LoggerHandler ld = new DelegateAsParameter.LoggerHandler(floger.Log);
            //DelegateAsParameter.LoggerHandler ld = floger.Log;

            logHandler.Process(ld);
            floger.Close();
        }
	
	    
	
    }
    #endregion
}
