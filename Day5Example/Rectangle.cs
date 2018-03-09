namespace Day5Example
{
    public class Rectangle
    {
        protected uint _length;
        public uint Length
        {
            get
            {
                return this._length;
            }
        }

        protected uint _width;
        public uint Width
        {
            get
            {
                return this._width;
            }
        }

        public uint Area
        {
            get
            {
                return (this._length * this._width);
            }
        }

        public uint Circumference
        {
            get
            {
                return (2 * this._length) + (2 * this._width);
            }
        }

        /// <summary>
        /// Create a rectangle with equal sides (i.e. a square)
        /// </summary>
        /// <param name="side">length of a side</param>
        public Rectangle(uint side)
        {
            this._width = side;
            this._length = side;
        }

        /// <summary>
        /// Create a rectangle given the length and width
        /// </summary>
        /// <param name="length">length of the rectangle</param>
        /// <param name="width">width of the rectangle</param>
        public Rectangle(uint length, uint width)
        {
            this._width = width;
            this._length = length;
        }
    }
}
