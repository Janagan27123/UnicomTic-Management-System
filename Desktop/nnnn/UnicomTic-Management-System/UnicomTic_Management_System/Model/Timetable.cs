using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTic_Management_System.Model
{
    public class Timetable
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int RoomId { get; set; }
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }  // இதை string-இல் இருந்து TimeSpan ஆக மாற்று
        public TimeSpan EndTime { get; set; }
    }
}
