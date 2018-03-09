namespace Day5Example
{
    public class Circle
    {
        public const double PI = 3.14;

        private uint _radius;
        public uint Radius
        {
            get
            {
                return this._radius;
            }
        }

        public double Area
        {
            get
            {
                return (PI * this._radius * this._radius);
            }
        }

        public double Circumference
        {
            get
            {
                return (2 * PI * this._radius);
            }
        }

        public Circle(uint radius)
        {
            this._radius = radius;
        }
    }
}