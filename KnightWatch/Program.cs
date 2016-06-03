using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightWatch
{
    class Program
    {
        static int count = 0, countLimit = 10000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Knight Watch.");
            RunKnightWatch();
        }

        /// <summary>
        /// Main Method
        /// </summary>
        private static void RunKnightWatch()
        {         
            Console.WriteLine();

            Console.WriteLine("Enter Co-ordinates for the point that you would like the knight to move to.");
            Console.WriteLine();
            Console.WriteLine("Enter x co-ordinate:");
            var x1 = Console.ReadLine();            
            Console.WriteLine("Enter y co-ordinate:");
            var y1 = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("You entered (" + x1 + "," + y1 + ")");
            Console.WriteLine("Initial Knight Position: (0,0)");
            Console.WriteLine();
            Point init = new Point(0, 0);

            try
            {
                Point final = new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));

                int moveCount = 0;

                KnightWatch.KnightMoves.CloseRangeKnightPositions curState = KnightMoves.GetPositionType(final, init);

                if (curState != KnightWatch.KnightMoves.CloseRangeKnightPositions.NowhereClose)
                {
                    moveCount = MoveFromClosestPoint(init, moveCount, curState);
                }

                else
                {
                    moveCount = MoveKnight(init, final);
                }

                if (moveCount == -2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Not possible to get to this point in given limit of : " + countLimit);
                }
                else
                {
                    Console.WriteLine();
                    var displayText = "The Knight hits the dead walker located at ({0},{1}) from initial position of ({2},{3}) in {4} moves.";
                    Console.WriteLine(String.Format(displayText, final.X, final.Y, init.X, init.Y, moveCount));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.InnerException);
            }
            Console.WriteLine();

            Console.WriteLine("Try Again? Press 'y' for 'yes' or any key to exit.");

            var response = Console.ReadLine();
            if (response.ToLower().Equals("y"))
            {
                RunKnightWatch();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="init"></param>
        /// <param name="moveCount"></param>
        /// <param name="curState"></param>
        /// <returns></returns>
        private static int MoveFromClosestPoint(Point init, int moveCount, KnightWatch.KnightMoves.CloseRangeKnightPositions curState)
        {
            if (curState == KnightWatch.KnightMoves.CloseRangeKnightPositions.AtDestination)
            {
                //bingo
            }

            else
            {
                //great now we can get to the point from here
                moveCount = KnightMoves.ClosestPossibleMove(init, curState, ref moveCount);
            }
            return moveCount;
        }

        /// <summary>
        /// Recursive Method to move the Knight
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int MoveKnight(Point begin, Point end)
        {
            if (begin.X == end.X && begin.Y == end.Y)
            {
                return count;
            }
            
            if (count > countLimit)
            {
                return -2; //not possible in given steps
            }

            KnightWatch.KnightMoves.CloseRangeKnightPositions curState;

            var ls = KnightMoves.GetHitPoints(end, begin);//

            var checkDirectHit = ls.Where(p => p.X == end.X && p.Y == end.Y).FirstOrDefault();

            if (checkDirectHit != null)
            {
                //bingo 
                count++;
                Console.WriteLine("Step - " + count + " Knight moves to : (" + end.X + "," + end.Y + ")");
                return count;
            }            

            foreach (var knight in ls)
            {
                curState = KnightMoves.GetPositionType(end, knight);  

                if (curState != KnightWatch.KnightMoves.CloseRangeKnightPositions.NowhereClose)
                {
                    count = MoveFromClosestPoint(knight, count, curState);
                    return count;
                    
                }
            }
            
            //if we reached till here, we had a bad luck finding our point in the for loop i.e. knight is still further away from destination
            var nearestPoint = ls.OrderBy(p => p.DistFromGivenPoint).Take(1).ToList()[0];
            count++;
            Console.WriteLine("Step - " + count + " Knight moves to : (" + nearestPoint.X + "," + nearestPoint.Y + ")");
            return MoveKnight(nearestPoint, end);
        }
       
    }

}
