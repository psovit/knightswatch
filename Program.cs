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
            Point final = new Point(1, 0);

            int moveCount = 0;

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
                Console.WriteLine(String.Format(displayText,final.X, final.Y, init.X, init.Y,moveCount));                
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
            Console.WriteLine("Count: " + count + " Current Knight Position: " + nearestPoint.X + ", " + nearestPoint.Y);
            return Method(nearestPoint, end);
        }
       
    }

}
