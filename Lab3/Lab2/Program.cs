using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Database;
using Lab2.Controller;
using Lab2.Model;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CinemaContext dB = new CinemaContext())
            {
                /*MovieDAO rd = new MovieDAO(dB);
                SeanceDAO gd = new SeanceDAO(dB);
                TicketDAO bd = new TicketDAO(dB);
                HallDAO d = new HallDAO(dB);*/
                ControllerClass controller = new ControllerClass(dB);
                controller.Start();
            }
        }
    }
}
