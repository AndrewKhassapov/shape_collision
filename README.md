# shape_collision project

# Shape collision algorithm

A shape collision algorithm for an infinite 2D board of shapes that returns a Dictionary<int, List<int>>() of all intersections between shapes.
Each shape has a unique ID as an integer.
Entry way point is Program.cs
> Return is in the format:
```cs
[SHAPE ID] => ([SHAPE ID INTERSECTED WITH], ..., [SHAPE ID INTERSECTED WITH])
```

## Installation

No installation necessary.

## Usage

Usage is through the ad hoc World class.
```cs
List<Shape>() // Add Shapes to this list.
Dictionary<int, List<int>>() // Return of all intersections.
```

## Contributing

Additional classes, methods, refactoring and optimization always welcome.

## License

Free to use.
