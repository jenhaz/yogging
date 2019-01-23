using System;
using System.Collections.Generic;

namespace Yogging.Domain.Sprints
{
    public interface ISprintRepository
    {
        IEnumerable<Sprint> GetAll();
        Sprint GetById(Guid id);
        void Create(Sprint sprint);
        void Update(Sprint sprint);
        void Delete(Sprint sprint);
    }
}