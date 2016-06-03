using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Point init = new Point(0, 0);
            Console.WriteLine("Welcome to Knight Watch.");
            Console.WriteLine();
            Console.WriteLine("Emter Co-ordinates for the point that you would like the knight to move to.");
            Console.WriteLine("Enter x co-ordinate:");
            var x1 = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter y co-ordinate:");
            var y1 = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine();

            try
            {
                Point final = new Point(Convert.ToInt32(x1), Convert.ToInt32(y1));

                int moveCount = 0;

                Console.WriteLine("Initial Knight Position: (0,0)");
                KnightWatch.KnightMoves.CloseRangeKnightPositions curState = KnightMoves.GetPositionType(final, init);

                if (curState != KnightWatch.KnightMoves.CloseRangeKnightPositions.NowhereClose)
                {
                    moveCount = MoveFromClosestPoint(init, moveCount, curState);
                }

                else
                {
                    moveCount = Method(init, final);
                }

                if (moveCount == -2)
                {
                    Console.WriteLine("Not possible to get to this point in given limit of : " + countLimit);
                }
                else
                {
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
            Console.ReadLine();
        }


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
        

        static int count = 0, countLimit = 10000;

        private static int Method(Point begin, Point end)
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

            var ls = KnightMoves.GetBestHitPoints(end, begin);//

            var checkDirectHit = ls.Where(p => p.X == end.X && p.Y == end.Y).FirstOrDefault();

            if (checkDirectHit != null)
            {
                //bingo 
                return count + 1 ;
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
            Console.WriteLine("Step: " + count + " Current Knight Position: " + nearestPoint.X + ", " + nearestPoint.Y);
            return Method(nearestPoint, end);
        }
       
    }

}
