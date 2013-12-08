using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class RandomTask
    {
        public int Id { get; private set; }
        public TaskProgress Progress { get; private set; }
        public int Index { get; set; }

        public TaskStatus Status
        {
            get
            {
                if (Progress == null)
                    return TaskStatus.Locked;
                return Progress.Finished ? TaskStatus.Finished : TaskStatus.Doing;
            }
        }

        public bool IsEmpty
        {
            get { return Progress == null; }
        }

        public void SetEmpty()
        {
            Id = 0;
            Progress = null;
        }

        public void Accept(int id)
        {
            Id = id;
            Progress = TaskProgress.Create(id);
        }

        public RandomTaskArgs ToArgs()
        {
            return new RandomTaskArgs()
            {
                Id = Id,
                Progress = Progress != null ? Progress.Step : 0,
                Status = Status,
                Index = Index
            };
        }
    }
}
