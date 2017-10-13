using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf;

namespace TopShelfTest
{

    
    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well.",DateTime.Now);
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run( x=> {
                x.Service<TownCrier>(s =>                        //2
                {
                    s.ConstructUsing(name => new TownCrier());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Sample Topshelf Host");        //7
                x.SetDisplayName("Stuff");                       //8
                x.SetServiceName("Stuff");
            });
        }
    }
}
