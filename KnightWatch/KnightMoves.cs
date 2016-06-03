using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightWatch
{
    public class KnightMoves
    {
        #region Enum: Possible Knights Positions

        /// <summary>
        /// Location of the Knight with respect to the destination
        /// </summary>
        public enum CloseRangeKnightPositions
        {
            FirstQuadDiag, SecondQuadDiag, ThirdQuadDiag, FourthQuadDiag,
            PositiveXAxis, PositiveYAxis, NegativeXAxis, NegativeYAxis,
            NowhereClose, AtDestination
            //Positive2XAxis, Positive2YAxis, Negative2XAxis, Negative2YAxis
        }

        #endregion

        #region Possible Moves for the KNIGHT

        public static void MoveX2InQuad1(Point curPoint)
        {
            curPoint.X = curPoint.X + 2;
            curPoint.Y = curPoint.Y + 1;
        }

        public static void MoveY2InQuad1(Point curPoint)
        {
            curPoint.X = curPoint.X + 1;
            curPoint.Y = curPoint.Y + 2;
        }

        public static void MoveX2InQuad2(Point curPoint)
        {
            curPoint.X = curPoint.X - 2;
            curPoint.Y = curPoint.Y + 1;
        }

        public static void MoveY2InQuad2(Point curPoint)
        {
            curPoint.X = curPoint.X - 1;
            curPoint.Y = curPoint.Y + 2;
        }

        public static void MoveX2InQuad3(Point curPoint)
        {
            curPoint.X = curPoint.X - 2;
            curPoint.Y = curPoint.Y - 1;
        }

        public static void MoveY2InQuad3(Point curPoint)
        {
            curPoint.X = curPoint.X - 1;
            curPoint.Y = curPoint.Y - 2;
        }

        public static void MoveX2InQuad4(Point curPoint)
        {
            curPoint.X = curPoint.X + 2;
            curPoint.Y = curPoint.Y - 1;
        }

        public static void MoveY2InQuad4(Point curPoint)
        {
            curPoint.X = curPoint.X + 1;
            curPoint.Y = curPoint.Y - 2;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The knight can move to 8 different co-ordinates. 
        /// Get possible 8 locations along as well as their distance from the destination point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static List<Point> GetHitPoints(Point hitPoint, Point initPoint)
        {
            var ls = new List<Point>()
            {
                new Point(initPoint.X - 2, initPoint.Y-1, hitPoint),
                new Point(initPoint.X + 2, initPoint.Y-1, hitPoint),
                new Point(initPoint.X - 2, initPoint.Y+1, hitPoint),
                new Point(initPoint.X + 2, initPoint.Y+1, hitPoint),
                                                          
                new Point(initPoint.X - 1, initPoint.Y-2, hitPoint),
                new Point(initPoint.X + 1, initPoint.Y-2, hitPoint),
                new Point(initPoint.X - 1, initPoint.Y+2, hitPoint),
                new Point(initPoint.X + 1, initPoint.Y+2, hitPoint)
            };

            return ls;
        }

        /// <summary>
        /// Get how far the knight is from destination
        /// </summary>
        /// <param name="knightX"></param>
        /// <param name="knightY"></param>
        /// <param name="finalX"></param>
        /// <param name="finalY"></param>
        /// <returns></returns>
        public static CloseRangeKnightPositions GetPositionType
            (Point knight, Point target)
        {
            int deltaX = knight.X - target.X;
            int deltaY = knight.Y - target.Y;

            if (!(Math.Abs(deltaX) <= 1 && Math.Abs(deltaY) <= 1))
            {
                return CloseRangeKnightPositions.NowhereClose;
            }

            if (deltaX == 1 && deltaY == 1)
            {
                return CloseRangeKnightPositions.FirstQuadDiag;
            }

            if (deltaX == -1 && deltaY == 1)
            {
                return CloseRangeKnightPositions.SecondQuadDiag;
            }

            if (deltaX == -1 && deltaY == -1)
            {
                return CloseRangeKnightPositions.ThirdQuadDiag;
            }

            if (deltaX == 1 && deltaY == -1)
            {
                return CloseRangeKnightPositions.FourthQuadDiag;
            }

            if (deltaX == 1 && deltaY == 0)
            {
                return CloseRangeKnightPositions.PositiveXAxis;
            }

            if (deltaX == -1 && deltaY == 0)
            {
                return CloseRangeKnightPositions.NegativeXAxis;
            }

            if (deltaX == 0 && deltaY == 1)
            {
                return CloseRangeKnightPositions.PositiveYAxis;
            }

            if (deltaX == 0 && deltaY == -1)
            {
                return CloseRangeKnightPositions.NegativeYAxis;
            }

            return CloseRangeKnightPositions.NowhereClose;

        }

        /// <summary>
        /// Move the knight from closest bordering box positions from the piece
        /// </summary>
        /// <param name="knightX"></param>
        /// <param name="knightY"></param>
        /// <param name="currPositionType"></param>
        /// <param name="moveCounter"></param>
        /// <returns></returns>
        public static int ClosestPossibleMove(Point knight, CloseRangeKnightPositions currPositionType, ref int moveCounter)
        {
            switch (currPositionType)
            {
                    
                case CloseRangeKnightPositions.FirstQuadDiag:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad4(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad2(knight);

                        break;
                    }
                case CloseRangeKnightPositions.SecondQuadDiag:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad3(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad1(knight);
                        
                        break;
                    }
                case CloseRangeKnightPositions.ThirdQuadDiag:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad4(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad2(knight);

                        break;
                    }
                case CloseRangeKnightPositions.FourthQuadDiag:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad3(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad1(knight);

                        break;
                    }
                case CloseRangeKnightPositions.PositiveXAxis:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad1(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad4(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad3(knight);

                        break;
                    }
                case CloseRangeKnightPositions.PositiveYAxis:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad1(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad2(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad3(knight);

                        break;
                    }
                case CloseRangeKnightPositions.NegativeXAxis:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad3(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad1(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad2(knight);
                        break;
                    }
                case CloseRangeKnightPositions.NegativeYAxis:
                    {
                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveX2InQuad3(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad1(knight);

                        moveCounter++;
                        Console.WriteLine("Step - " + moveCounter + " Knight moves to : (" + knight.X + "," + knight.Y + ")");
                        KnightMoves.MoveY2InQuad4(knight);

                        break;
                    }
                default:
                    break;
            }
            return moveCounter;
        }

        #endregion
    }
}
