using System;
using System.Linq.Expressions;

namespace LamdaExpressionsFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            //Action Examples

            WriteConcatSimple("WriteConcat", "Simple");
            WriteConcatLambda("WriteConcat", "Lamda");
            WriteConcatExp("WriteConcat", "Exp");


            //Func Examples int

            Console.WriteLine();
            Console.WriteLine("IntCalcSimple result: {0}", IntCalcSimple(2));
            Console.WriteLine("IntCalcLambda1 result: {0}", IntCalcLambda1(2));
            Console.WriteLine("IntCalcLambda2 result: {0}", IntCalcLambda2(2));
            Console.WriteLine("IntCalcExp result: {0}", IntCalcExp(2));


            //Func Examples bool

            Console.WriteLine();
            Console.WriteLine("AddAddBool3Simple result: {0}", AddAddBool3Simple(3, 4));
            Console.WriteLine("AddAddBool3Lambda result: {0}", AddAddBool3Lambda(3, 4));
            Console.WriteLine("AddAddBool3Exp result: {0}", AddAddBool3Exp(3, 4));

            Console.WriteLine("AddAddBool3Simple result: {0}", AddAddBool3Simple(1, 1));
            Console.WriteLine("AddAddBool3Lambda result: {0}", AddAddBool3Lambda(1, 1));
            Console.WriteLine("AddAddBool3Exp result: {0}", AddAddBool3Exp(1, 1));

            //Func Max Params
            Console.WriteLine();
            Console.WriteLine("Sum16: {0}", Sum16(1, 2, 3, 4, 5, 6, 7, 8, 9,
                10, 11, 12, 13, 14, 15, 16));

            Console.ReadLine();
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

        static int IntCalcSimple(int value)
        {
            return (value + 5 - 3) * 2;
        }
        static int IntCalcLambda1(int value)
        {
            int result = value;

            Func<int, int> addOper;
            Func<int, int> subOper;
            Func<int, int> mulOper;

            addOper = x => x + result;
            subOper = x => result - x;
            mulOper = x => x * result;

            result = addOper(5);
            result = subOper(3);
            result = mulOper(2);

            return result;
        }

        static int IntCalcLambda2(int intValue)
        {
            Func<int, int> intCalc;
            intCalc = v => (v + 5 - 3) * 2;
            return intCalc(intValue);
        }


        static int IntCalcExp(int intValue)
        {
            ParameterExpression value = Expression.Parameter(typeof(int), "value");
            ParameterExpression result = Expression.Parameter(typeof(int), "result");

            BlockExpression block = Expression.Block(
            new[] { result },
            Expression.Assign(result, value),
            Expression.AddAssign(result, Expression.Constant(5)),
            Expression.SubtractAssign(result, Expression.Constant(3)),
            Expression.MultiplyAssign(result, Expression.Constant(2))
            );

            return Expression.Lambda<Func<int, int>>(block, value).Compile()(intValue);

        }

        static bool AddAddBool3Simple(int value1, int value2)
        {
            return value1 + value2 > 3;
        }

        static bool AddAddBool3Lambda(int value1, int value2)
        {
            Func<int, int, bool> addAddBool;
            addAddBool = (v1, v2) => (v1 + v2) > 3;

            return addAddBool(value1, value2);
        }

        static bool AddAddBool3Exp(int value1, int value2)
        {
            ParameterExpression val1 = Expression.Parameter(typeof(int), "val1");
            ParameterExpression val2 = Expression.Parameter(typeof(int), "val2");
            ParameterExpression result = Expression.Parameter(typeof(bool), "result");

            BlockExpression block = Expression.Block(
            new[] { result },
            Expression.IfThenElse(
            Expression.GreaterThan(Expression.Add(val1, val2), Expression.Constant(3)),
            Expression.Assign(result, Expression.Constant(true)),
            Expression.Assign(result, Expression.Constant(false))),
            result
            );

            return Expression.Lambda<Func<int, int, bool>>(block, val1, val2).Compile()(value1, value2);
        }

        static int Sum16(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16)
        {
            Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> funcMaxParam =
                (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16) =>
                x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10 + x11 + x12 + x13 + x14 + x14 + x16;

            return funcMaxParam(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
        }

    }
}
