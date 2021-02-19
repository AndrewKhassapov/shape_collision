using System;
using System.Collections;
					
public class Program
{

	public interface IShape{
		public double positionX{get;}
		public double positionY{get;}
	}

	public class Shape : IShape{

		public double positionX{get; protected set;}
		public double positionY{get; protected set;}
		
		
		public Shape(double posX = 0.0, double posY = 0.0){
			positionX = posX;
			positionY = posY;
		}

	public virtual bool CollidesWith(Shape checkShape){		
			return false;
	}
		
	}

	public sealed class Rectangle : Shape {
		
		public double _width{get; private set;}
		public double _height{get; private set;}

		public Rectangle(double width, double height, double posX = 0.0, double posY = 0.0): base(posX, posY){
			_width = width;
			_height = height;
		}
	}
	
	public sealed class Circle : Shape {
		
		public double _radius{get; private set;}

		public Circle(double radius, double posX = 0.0, double posY = 0.0): base(posX, posY){
			_radius = radius;
		}
	}

	
	
		
	
	
	///<summary>Main program execution</summary>
	public static void Main()
	{
		Console.WriteLine("Hello World");
	}
}
