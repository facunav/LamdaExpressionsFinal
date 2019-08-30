using System;
using System.Linq.Expressions;

namespace LamdaExpressionsFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteConcatSimple("WriteConcat", "Simple");
            WriteConcatLambda("WriteConcat", "Lamda");
            WriteConcatExp("WriteConcat", "Exp");
        }

        static void WriteConcatSimple(string value1, string value2)
        {
            Console.WriteLine(value1 + value2);

        }
        static void WriteConcatLambda(string value1, string value2)
        {
            Action<string, string> concatFunct;
            concatFunct = (v1, v2) => Console.WriteLine(value1 + value2);
            concatFunct(value1, value2);
        }

        static void WriteConcatExp(string value1, string value2)
        {
            ParameterExpression val1 = Expression.Parameter(typeof(string), "val1");
            ParameterExpression val2 = Expression.Parameter(typeof(string), "val2");

            BlockExpression block = Expression.Block(
            Expression.Call(null,
            typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
             Expression.Call(
             typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) }),
             val1, val2))
            );
            Expression.Lambda<Action<String, String>>(block, val1, val2).Compile()(value1, value2);

        }
    }
}
