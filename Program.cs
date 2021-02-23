using System;
using System.Collections.Generic;

namespace shape_collision
{
    public class Program
    {
        public interface IShape
        {
            public int id { get; }
            public double x { get; }
            public double y { get; }
            public bool CollidesWith() { return false; }
        }

        public class Shape : IShape
        {
            public int id { get; protected set; }
            public double x { get; protected set; }
            public double y { get; protected set; }


            public Shape(int iid, double posX = 0.0, double posY = 0.0)
            {
                id = iid;
                x = posX;
                y = posY;
            }

            public virtual bool CollidesWith(Shape checkShape)
            {
                return false;
            }

            /// <summary>
            /// Detects collision between two rectangles.
            /// </summary>
            /// <returns>True for collision.</returns>
            protected bool GetCollision(Rectangle rectOne, Rectangle rectTwo)
            {
                double dX = Math.Abs(rectOne.x - rectTwo.x);
                double dY = Math.Abs(rectOne.y - rectTwo.y);

                if (dX > (rectOne._widthHalf + rectTwo._widthHalf)) { return false; }

                if (dY > (rectOne._heightHalf + rectTwo._heightHalf)) { return false; }

                return true; //Within bounds.
            }

            /// <summary>
            /// Detects collision between two circles.
            /// </summary>
            /// <returns>True for collision.</returns>
            protected bool GetCollision(Circle circleOne, Circle circleTwo)
            {
                double distanceBetweenCircles = Math.Sqrt((circleTwo.x - circleOne.x) * (circleTwo.x - circleOne.x) + (circleTwo.y - circleOne.y) * (circleTwo.y - circleOne.y));
                return (distanceBetweenCircles >= (circleOne._radius + circleTwo._radius));
            }

            /// <summary>
            /// Detects collision between rectangle and circle.
            /// </summary>
            /// <returns>True for collision.</returns>
            protected bool GetCollision(Rectangle rect, Circle circle) {

                double dX = Math.Abs(circle.x - rect.x);
                double dY = Math.Abs(circle.y - rect.y);

                if (dX > (rect._widthHalf + circle._radius)) { return false; } //Out of bounds.
                if (dY > (rect._heightHalf + circle._radius)) { return false; } //Out of bounds.

                if (dX <= (rect._widthHalf)) { return true; } //Always in bounds.
                if (dY <= (rect._heightHalf)) { return true; } //Always in bounds.

                double dCorner = Math.Pow((dX - rect._widthHalf), 2.0) + Math.Pow((dY - rect._heightHalf), 2.0); //Distance at corners.
                return (dCorner <= Math.Pow(circle._radius, 2.0));
            }

        }

        public sealed class Rectangle : Shape
        {
            public double _width { get; private set; }
            public double _height { get; private set; }
            public double _widthHalf { get { return _width / 2.0; } }
            public double _heightHalf { get { return _height / 2.0; } }

            //Bounds.
            public double _maxX { get { return x + _widthHalf; } }
            public double _minX { get { return x - _widthHalf; } }
            public double _maxY { get { return y + _heightHalf; } }
            public double _minY { get { return y - _heightHalf; } }

            public Rectangle(int iid, double width, double height, double posX = 0.0, double posY = 0.0) : base(iid, posX, posY)
            {

                _width = width;
                _height = height;
            }

            public override bool CollidesWith(Shape checkShape)
            {
                bool collided = false; //Flag. True upon collision.

                if (checkShape is Rectangle)
                {
                    collided = GetCollision(this, (Rectangle)checkShape);
                }
                else if (checkShape is Circle) {
                    collided = GetCollision(this, (Circle)checkShape);
                }

                return collided;
            }
        }

        public sealed class Circle : Shape
        {

            public double _radius { get; private set; }

            //Bounds.
            public double _maxX { get { return x + _radius; } }
            public double _minX { get { return x - _radius; } }
            public double _maxY { get { return y + _radius; } }
            public double _minY { get { return y - _radius; } }

            public Circle(int id, double radius, double posX = 0.0, double posY = 0.0) : base(id, posX, posY)
            {
                _radius = radius;
            }

            public override bool CollidesWith(Shape checkShape)
            {
                bool collided = false; //Flag. True upon collision.

                if (checkShape is Circle)
                {
                    collided = GetCollision(this, (Circle)checkShape);
                }
                else if (checkShape is Rectangle) {
                    collided = GetCollision((Rectangle)checkShape, this);
                }

                return collided;
            }
        }

        public sealed class World
        {
            public List<Shape> worldShapes = null;

            public World()
            {
                BuildWorldShapes();
                var toPrint = FindIntersections(worldShapes);

                foreach (int key in toPrint.Keys)
                {
                    Console.Write("{0} => (", key);
                    foreach (int item in toPrint[key])
                    {
                        Console.Write("{0}, ", item);
                    }
                    Console.WriteLine(")");
                }
            }

            public List<Shape> BuildWorldShapes()
            {
                worldShapes = new List<Shape>();
                worldShapes.Add(new Rectangle(0, 2.0, 2.0, 0.0, 0.0));
                worldShapes.Add(new Rectangle(1, 2.0, 2.0, 1.0, 0.0));
                worldShapes.Add(new Rectangle(2, 2.0, 2.0, 2.5, 0.0));
                worldShapes.Add(new Rectangle(3, 1.0, 1.0, 5.0, 3.0));
                worldShapes.Add(new Circle(4, 2.0, 0.0, 0.0));
                worldShapes.Add(new Circle(5, 2.0, 1.0, 1.0));

                return worldShapes; //Output by reference so any changes will affect this List.
            }

            public Dictionary<int, List<int>> FindIntersections(List<Shape> shapes)
            {
                Dictionary<int, List<int>> intersections = new Dictionary<int, List<int>>();
                for (int i = 0; i < worldShapes.Count; i++)
                {
                    intersections.Add(worldShapes[i].id, FindShapesIntersected(worldShapes[i]));
                }
                return intersections;
            }

            public List<int> FindShapesIntersected(Shape shape)
            {
                List<int> shapeIdsIntersected = new List<int>();
                for (int i = 0; i < worldShapes.Count; i++)
                {
                    if (shape.id != worldShapes[i].id)
                    {
                        if (shape.CollidesWith(worldShapes[i]))
                        {
                            Console.WriteLine("Shape{0} collides with Shape{1}", shape.id, worldShapes[i].id);
                            shapeIdsIntersected.Add(worldShapes[i].id);
                        }
                    }
                }

                return shapeIdsIntersected;
            }

        }
        ///<summary>Main program execution</summary>
        public static void Main()
        {
            World myWorld = new World();
        }
    }
}