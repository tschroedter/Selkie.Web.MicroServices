using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Web.MicroServices.Common.Interfaces.Nancy
{
    public interface IInformationFinder <T>
    {
        T Delete(Guid id);

        [CanBeNull]
        T FindById(Guid id);

        IEnumerable <T> List();
        T Save(T colonySettings);
    }
}