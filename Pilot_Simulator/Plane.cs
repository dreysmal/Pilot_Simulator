using System.Collections.Generic;

namespace Pilot_Simulator
{
    public delegate void Flight(int x, int y);

    class Plane
    {
        private int speed;

        public int Speed
        {
            get { return speed; }
            set
            {
                if(value >= 0 && value <= 1200)
                speed = value;
                else
                {
                    if (value < 0)
                        speed = 0;
                    else
                    {
                        if (value > 1200)
                            speed = 1200;
                    }
                }
            }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set
            {
                if (value >= 0 && value <= 8000)
                    height = value;
                else
                {
                    if (value < 0)
                        height = 0;
                    else
                    {
                        if (value > 8000)
                            height = 8000;
                    }
                }
            }
        }

        public List<Dispatch> dispatchers = new List<Dispatch>(2);

        //----------------Event Declaration---------------//
                     public event Flight Moving;
        //------------------------------------------------//

        public void SetDispatchersToTheirPositions(Dispatch dispatch)
        {
            dispatchers.Add(dispatch);
        }
        public void KillDispatch(int pos, string name)
        {
            dispatchers[pos - 1].Rename(name);
        }

        public void Shift(Direction direction)
        {
            switch (direction)
            {
                case Direction.SPEEDUP:
                    Speed += 50;
                    break;
                case Direction.SPEEDDOWN:
                    Speed -= 50;
                    break;
                case Direction.SPEEDEXTRAUP:
                    Speed += 150;
                    break;
                case Direction.SPEEDEXTRADOWN:
                    Speed -= 150;
                    break;
                case Direction.HEIGHTUP:
                    if (Speed>=50)
                    Height += 250;
                    break;
                case Direction.HEIGHTDOWN:
                    Height -= 250;
                    break;
                case Direction.HEIGHTEXTRAUP:
                    if (Speed >= 50)
                    Height += 500;
                    break;
                case Direction.HETGHTEXTRADOWN:
                    Height -= 500;
                    break;
                default:
                    break;
            }

            if(Moving != null)
            Moving(Speed, Height);
        }
    }
}
