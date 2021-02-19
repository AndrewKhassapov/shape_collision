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


			public Shape(int id, double posX = 0.0, double posY = 0.0)
			{
				x = posX;
				y = posY;
			}

			public virtual bool CollidesWith(Shape checkShape)
			{
				return false;
			}

		}

		public sealed class Rectangle : Shape
		{

			public double _width { get; private set; }
			public double _height { get; private set; }
			private double _widthHalf { get { return _width / 2.0; } }
			private double _heightHalf { get { return _height / 2.0; } }

			//Bounds.
			public double _maxX { get { return x + _widthHalf; } }
			public double _minX { get { return x - _widthHalf; } }
			public double _maxY { get { return y + _heightHalf; } }
			public double _minY { get { return y - _heightHalf; } }

			public Rectangle(int id, double width, double height, double posX = 0.0, double posY = 0.0) : base(id, posX, posY)
			{
				_width = width;
				_height = height;
			}

			public override bool CollidesWith(Shape checkShape)
			{
				bool collided = false; //Flag. True upon collision.

				if (checkShape is Rectangle)
				{
					Rectangle collider = (Rectangle)checkShape;

					if (x >= collider.x)
					{
						collided = (_minX <= collider._maxX);
					}
					else
					{
						collided = (_maxX >= collider._minX);
					}

					if (y >= collider.y)
					{
						collided = (_minY <= collider._maxY);
					}
					else
					{
						collided = (_maxY >= collider._minY);
					}
				}

				return collided;
			}
		}

		/*public sealed class Circle : Shape
		{

			public double _radius { get; private set; }
			public double _maxX { get { return x + _radius; } }
			public double _minX { get { return x - _radius; } }
			public double _maxY { get { return y + _radius; } }
			public double _minY { get { return y - _radius; } }

			public Circle(int id, double radius, double posX = 0.0, double posY = 0.0) : base(id, posX, posY)
			{
				_radius = radius;
			}
		}*/

		///<summary>Main program execution</summary>
		public static void Main()
		{
			List<Shape> worldShapes = new List<Shape>();
			worldShapes.Add(new Rectangle(0, 1.0, 1.0, 0.0, 0.0));
			worldShapes.Add(new Rectangle(1, 1.0, 1.0, 0.2, 0.2));
			worldShapes.Add(new Rectangle(2, 1.0, 1.0, 2.0, 3.0));
			worldShapes.Add(new Rectangle(3, 1.0, 1.0, 0.5, 3.0));

			Console.WriteLine("Collision is: {0}", worldShapes[0].CollidesWith(worldShapes[1]));

			Console.WriteLine("Hello World");
		}
	}
}