using System;

namespace Pilot_Simulator
{
    public delegate void Show(string name, int x);
    public delegate void Alert();
    public delegate void Message(string str);

    class Dispatch
    {
        private string name;

        public string Name
        {
            get { return name != ""? name: "No Name"; }
            set { name = value; }
        }

        public int N { get; private set; } = new Random().Next(-200, 200);
        public int Penalty { get; set; }
        public Dispatch(string name)
        {
            Name = name;
        }
        public void Rename(string str)
        {
            Name = str;
            N = new Random().Next(-200, 200);
            Penalty = 0;
        }
        ///////////
        public event Show Dispatch_message;
        public event Alert Dispatch_alert;
        public event Show Dispatch_penalties;
        public event Message Dispatch_excesses;
        ///////////

        public void Recomended_Height(int speed, int height)
        {
            if(speed>=50)
            {
                if (Dispatch_message != null)
                    Dispatch_message(Name, (7 * speed - N));
            }
            Count_Penalties(speed, height);
        }

        public void Count_Penalties(int speed, int height)
        {
            try
            {
                if (speed >= 150 && height == 0)
                    throw new Exception("The plane has crushed!!!");
                if (speed > 0 && height > 0)
                {
                    int dif = Math.Abs(height - (7 * speed - N));
                    if (dif >= 300 && dif <= 600)
                        Penalty += 25;
                    if (dif > 600 && dif <= 1000)
                        Penalty += 50;
                    if (speed > 1000)
                    {
                        Penalty += 100;
                        if (Dispatch_alert != null)
                            Dispatch_alert();
                    }
                    if ((dif > 1000))
                        throw new Exception("The plane has crushed!!!");
                    if (Penalty > 1000)
                        throw new Exception("Not suitable for flights!!!");
                }
            }
            catch (Exception ex)
            {
                if (Dispatch_excesses != null)
                    Dispatch_excesses(ex.Message);
            }
                if (Dispatch_penalties != null)
                    Dispatch_penalties(Name, Penalty);
        }
    }
}
