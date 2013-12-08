using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class TaskProgress
    {
        public static TaskProgress Create(int id, bool finished = false, int step = 0)
        {
            return new TaskProgress() { Id = id, Finished = finished, Step = step };
        }

        private TaskProgress()
        {
        }

        public int Id { get; set; }
        public int Step { get; set; }
        public bool Finished { get; set; }

        public void Reset()
        {
            Step = 0;
            Finished = false;
        }

        public TaskArgs ToTaskArgs()
        {
            return new TaskArgs()
            {
                Id = Id,
                Status = Finished ? TaskStatus.Finished : TaskStatus.Doing,
                Progress = Step
            };
        }
    }
}
