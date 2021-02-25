using System;
using System.Collections.Generic;

namespace Expression_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            string exp = "";
            List<string> infixList = InfixListFromUser("Enter an expression: ", exp);
            List<string> postfixList = PostfixFromInfix(infixList);
            DisplayList("The postfix list is: ", postfixList);
            Console.WriteLine();
            Console.WriteLine($"The value of the expression is {ValueOfPostfixList(postfixList)}.");
        }
        static List<string> InfixListFromUser(string msg, string exp)
        {
            Console.Write(msg);
            exp = Console.ReadLine();
            var infixList = new List<string>();
            string strToken = "";
            for (int i = 0; i <= exp.Length - 1; i++)
            {
                if (int.TryParse(exp[i].ToString(), out int num) || (exp[i] == '.'))
                {
                    strToken += exp[i];
                }
                else if (exp[i] == ' ')
                {
                    continue;
                }
                else
                {
                    if (strToken != "")
                    {
                        infixList.Add(strToken);
                        strToken = "";
                    }
                    infixList.Add(exp[i].ToString());
                }
            }
            if (strToken != "")
            {
                infixList.Add(strToken);
            }
            return infixList;
        }
        static List<string> PostfixFromInfix(List<string> infixList)
        {
            var opStack = new Stack<string>();
            var postfixList = new List<string>();
            foreach (string token in infixList)
            {
                if (double.TryParse(token, out double num))
                {
                    postfixList.Add(token);
                }
                else
                {
                    if (opStack.Count == 0 && token != ")")
                    {
                        opStack.Push(token);
                    }
                    else if (token == ")" && opStack.Count == 0) break;
                    else
                    {
                        while (PrevOpShouldBeExecutedBeforeCurOp(opStack, token) == true)
                        {
                            if (opStack.Peek() == "(")
                            {
                                opStack.Pop();
                            }
                            else
                            {
                                postfixList.Add(opStack.Pop());
                            }
                            if (opStack.Count == 0) break;
                        }
                        opStack.Push(token);
                    }
                }
            }
            while (opStack.Count != 0)
            {
                if (opStack.Peek() != ")")
                {
                    postfixList.Add(opStack.Pop());
                }
                else opStack.Pop();
            }
            return postfixList;
        }
        static void DisplayList(string msg, List<string> postfixlist)
        {
            Console.WriteLine(msg);
            foreach (var token in postfixlist)
            {
                Console.Write(token + " ");
            }
        }
        static bool PrevOpShouldBeExecutedBeforeCurOp(Stack<string> opStack, string curOp)
        {
            var signChecking = new List<string> { "*", "/", "+", "-", ")" };
            var signChecking2 = new List<string> { "*", "/", "+", "-", "^", ")" };
            bool preOpIsMultiplicationOrDivision = ((opStack.Peek() == "*" || opStack.Peek() == "/") && (signChecking.Contains(curOp)));
            bool preOpIsMinusOrPlus = ((opStack.Peek() == "-" || opStack.Peek() == "+") && (curOp == "+" || curOp == "-" || curOp == ")"));
            bool preOpIsPowerFuction = ((opStack.Peek() == "^" && signChecking2.Contains(curOp)));
            bool preOpIsOpeningParenthesis = (opStack.Peek() == "(" && curOp == ")");
            if (preOpIsMultiplicationOrDivision || preOpIsMinusOrPlus || preOpIsPowerFuction || preOpIsOpeningParenthesis) 
            {
                return true;
            }
            else return false ;
        }
        static double ValueOfPostfixList(List<string> postfixList)
        {
            var numStack = new Stack<double>();
            double result = 0;
            foreach (var token in postfixList)
            {
                if (double.TryParse(token, out double num))
                {
                    numStack.Push(num);
                }
                else
                {
                    double num1 = numStack.Pop();
                    double num2 = numStack.Pop();
                    result = OpResult(num1, num2, token);
                    numStack.Push(result);
                }
            }
            return result;
        }
        static double OpResult(double num1, double num2, string op)
        {
            double result =0;
            if (op == "+")
            {
                result = num2 + num1;
            }
            else if (op == "-")
            {
                result = num2 - num1;
            }
            else if (op == "*")
            {
                result = num2 * num1;
            }
            else if (op == "/")
            {
                result = num2 / num1;
            }
            else if (op == "^")
            {
                result = Math.Pow(num2, num1);
            }
            else
            {
                Console.WriteLine("Invalid operator!!!");
            }
            return result;
        }
    }
}


                
                        
                        
                        
                    
                         
                        
                                
                                
                                


                
               
               

            

            
            
        


           

